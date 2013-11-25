using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DIP_hw1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            _inputImages = new List<Bitmap>();
            _resultImages = new List<Bitmap>();
            _openFile = new OpenFileDialog();
            _openFile.InitialDirectory = "C:";
            _openFile.Filter = "Bitmap Files (.bmp)|*.bmp|JPEG (.jpg)|*.jpg|PNG (.png)|*.png|All Files|*.*";
        }

        private void _bnLoadImage_Click(object sender, EventArgs e)
        {
            if (_openFile.ShowDialog() == DialogResult.OK)
            {
                int index = _listBoxInput.FindString(_openFile.FileName);
                if (index == ListBox.NoMatches)
                {
                    _listBoxInput.Items.Add(_openFile.FileName);
                    _inputImages.Add(new Bitmap(_openFile.FileName));
                    index = _listBoxInput.Items.Count - 1;
                }
                _listBoxInput.SetSelected(index, true);
                _openFile.FileName = "";
                _openFile.InitialDirectory = _openFile.FileName.Substring(0, _openFile.FileName.Length - _openFile.SafeFileName.Length);
            }
        }

        private void _lbInputImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pictureBoxInput.Image = _inputImages[_listBoxInput.SelectedIndex];
            Bitmap inputImage = _inputImages[_listBoxInput.SelectedIndex];
            List<Bitmap> results = new List<Bitmap>();
            results.Add(new Bitmap(_inputImages[_listBoxInput.SelectedIndex]));

            List<string> resultName = new List<string>();
            resultName.Add("Temp Result");
            ShowResult(ref results, ref resultName, true);
            _checkBoxThresholding.Enabled = true;
            _checkBoxThresholding.Checked = false;
        }

        static public void RGBExtraction(ref Bitmap image, out List<Bitmap> results)
        {
            results = new List<Bitmap>();
            for (int i = 0; i < 3; i++)
            {
                results.Add(new Bitmap(image.Width, image.Height));
            }

            Bitmap RImage = results[0];
            Bitmap GImage = results[1];
            Bitmap BImage = results[2];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = image.GetPixel(x, y);
                    RImage.SetPixel(x, y, Color.FromArgb(RGB.R, RGB.R, RGB.R));
                    GImage.SetPixel(x, y, Color.FromArgb(RGB.G, RGB.G, RGB.G));
                    BImage.SetPixel(x, y, Color.FromArgb(RGB.B, RGB.B, RGB.B));
                }
            }
        }

        private void _bnRGBExtraction_Click(object sender, EventArgs e)
        {
            if (_inputImages.Count == 0)
            {
                return;
            }

            Bitmap inputImage = _inputImages[_listBoxInput.SelectedIndex];
            List<Bitmap> results;
            RGBExtraction(ref inputImage, out results);

            List<string> resultName = new List<string>();
            resultName.Add("Red channel");
            resultName.Add("Green channel");
            resultName.Add("Blue channel");
            ShowResult(ref results, ref resultName, true);
        }

        private void ShowResult(ref List<Bitmap> results, ref List<string> names, bool isClearAllResult)
        {
            if (isClearAllResult == true)
            {
                _listBoxResult.Items.Clear();
                _resultImages.Clear();
            }
           
            for (int i = 0; i < results.Count; i++)
            {
                _listBoxResult.Items.Add(names[i]);
                _resultImages.Add(new Bitmap(results[i]));
            }
            
            _listBoxResult.SetSelected(_listBoxResult.Items.Count-1, true);

        }

        private void _lbResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pictureBoxResult.Image = _resultImages[_listBoxResult.SelectedIndex];
        }

        static public void TranslateGrayLevel(ref Bitmap image, out Bitmap result)
        {
            result = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color RGB = image.GetPixel(x, y);
                    int grayLevel = (RGB.R + RGB.G + RGB.B) / 3;
                    result.SetPixel(x, y, Color.FromArgb(grayLevel, grayLevel, grayLevel));
                }
            }
        }

        private void _bnGrayLevel_Click(object sender, EventArgs e)
        {
            if (_inputImages.Count == 0)
            {
                return;
            }
            Bitmap inputImage = _inputImages[_listBoxInput.SelectedIndex];
            Bitmap result;
            TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Gray Level Image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(result);            
            ShowResult(ref results, ref resultName, true);
        }

        static public void Thresholding(ref Bitmap image, out Bitmap result, int thresholdValue)//the image should be a gray level image
        {
            result = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    byte intensity = image.GetPixel(x, y).R;
                    result.SetPixel(x, y, (intensity >= thresholdValue) ? Color.FromArgb(255, 255, 255) : Color.FromArgb(0, 0, 0));
                }
            }
        }

        private void _cbThresholding_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBoxThresholding.Checked == false)
            {
                _trackBarThresholding.Enabled = false;
                _trackBarThresholding.Value = 0;
                _textBoxThresholding.Enabled = false;
                _textBoxThresholding.Text = "0";
                _listBoxResult.SetSelected(0, true);

                return;
            }
            _trackBarThresholding.Enabled = true;
            _textBoxThresholding.Enabled = true;
            Bitmap inputImage = _resultImages[_listBoxResult.SelectedIndex];
            Bitmap result;
            TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Origin Image");
            resultName.Add("Gray Level Image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(inputImage);
            results.Add(result);
            ShowResult(ref results, ref resultName, true);
            _trackBarThresholding.Value = 100;

        }

        private void _tbThresholding_ValueChange(object sender, EventArgs e)
        {
            if (_checkBoxThresholding.Checked == false)
            {
                return;
            }
            _textBoxThresholding.Text = _trackBarThresholding.Value.ToString();
            Bitmap inputImage = _resultImages[0];
            Bitmap result;
            Thresholding(ref inputImage, out result, _trackBarThresholding.Value);

            List<string> resultName = new List<string>();
            resultName.Add("Thresholding Image(" + _trackBarThresholding.Value.ToString() + ")");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(result);
            ShowResult(ref results, ref resultName, false);
        }

        private void _textBoxThresholding_TextChanged(object sender, EventArgs e)
        {
            if (_checkBoxThresholding.Checked == false || _textBoxThresholding.Text == "")
            {
                return;
            }
            if (Convert.ToInt32(_textBoxThresholding.Text) > 255)
            {
                _textBoxThresholding.Text = "255";
            }
            _trackBarThresholding.Value = Convert.ToInt32(_textBoxThresholding.Text);
        }

        private void _textBoxThresholding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
