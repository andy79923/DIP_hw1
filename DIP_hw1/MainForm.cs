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
            _smoothingMethod = MeanSmoothing;
        }

        private void _bnLoadImage_Click(object sender, EventArgs e)
        {
            if (_openFile.ShowDialog() == DialogResult.OK)
            {
                int index = _lbInputImage.FindString(_openFile.FileName);
                if (index == ListBox.NoMatches)
                {
                    _lbInputImage.Items.Add(_openFile.FileName);
                    _inputImages.Add(new Bitmap(_openFile.FileName));
                    index = _lbInputImage.Items.Count - 1;
                }
                _lbInputImage.SetSelected(index, true);
                _openFile.FileName = "";
                _openFile.InitialDirectory = _openFile.FileName.Substring(0, _openFile.FileName.Length - _openFile.SafeFileName.Length);
            }
        }

        private void _lbInputImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pbInputImage.Image = _inputImages[_lbInputImage.SelectedIndex];
            Bitmap inputImage = _inputImages[_lbInputImage.SelectedIndex];
            List<Bitmap> results = new List<Bitmap>();
            results.Add(new Bitmap(_inputImages[_lbInputImage.SelectedIndex]));

            List<string> resultName = new List<string>();
            resultName.Add("Temp Result");
            ShowResult(ref results, ref resultName, true);
            _cbThresholding.Enabled = true;
            _cbThresholding.Checked = false;
            _checkBoxSmoothing.Enabled = true;
            _checkBoxSmoothing.Checked = false;
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

            Bitmap inputImage = _inputImages[_lbInputImage.SelectedIndex];
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
                _lbResult.Items.Clear();
                _resultImages.Clear();
            }
           
            for (int i = 0; i < results.Count; i++)
            {
                _lbResult.Items.Add(names[i]);
                _resultImages.Add(new Bitmap(results[i]));
            }
            
            _lbResult.SetSelected(_lbResult.Items.Count-1, true);

        }

        private void _lbResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pbResult.Image = _resultImages[_lbResult.SelectedIndex];
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
            Bitmap inputImage = _inputImages[_lbInputImage.SelectedIndex];
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
            if (_cbThresholding.Checked == false)
            {
                _tbThresholding.Enabled = false;
                _tbThresholding.Value = 0;
                _textBoxThresholding.Enabled = false;
                _textBoxThresholding.Text = "0";
                _lbResult.SetSelected(0, true);

                return;
            }
            _tbThresholding.Enabled = true;
            _textBoxThresholding.Enabled = true;
            Bitmap inputImage = _resultImages[_lbResult.SelectedIndex];
            Bitmap result;
            TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Origin Image");
            resultName.Add("Gray Level Image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(inputImage);
            results.Add(result);
            ShowResult(ref results, ref resultName, true);
            _tbThresholding.Value = 100;

        }

        private void _tbThresholding_ValueChange(object sender, EventArgs e)
        {
            if (_cbThresholding.Checked == false)
            {
                return;
            }
            _textBoxThresholding.Text = _tbThresholding.Value.ToString();
            Bitmap inputImage = _resultImages[0];
            Bitmap result;
            Thresholding(ref inputImage, out result, _tbThresholding.Value);

            List<string> resultName = new List<string>();
            resultName.Add("Thresholding Image(" + _tbThresholding.Value.ToString() + ")");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(result);
            ShowResult(ref results, ref resultName, false);
        }

        private void _textBoxThresholding_TextChanged(object sender, EventArgs e)
        {
            if (_cbThresholding.Checked == false || _textBoxThresholding.Text == "")
            {
                return;
            }
            if (Convert.ToInt32(_textBoxThresholding.Text) > 255)
            {
                _textBoxThresholding.Text = "255";
            }
            _tbThresholding.Value = Convert.ToInt32(_textBoxThresholding.Text);
        }

        private void _textBoxThresholding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        static public void MeanSmoothing(ref Bitmap image, out Bitmap result, int filterSize)//the image should be a gray level image
        {
            if (filterSize % 2 != 1)
            {
                result = new Bitmap(image);
                return;
            }
            result = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int intensity = 0;
                    for (int j = 0; j < filterSize; j++)//Use replicate to interpolate the pixel when the filter position is out of the boundary of the source image
                    {
                        int wY = y - filterSize / 2 + j;
                        wY = (wY < 0) ? 0 : wY;
                        wY = (wY >= image.Height) ? image.Height - 1 : wY;
                        for (int i = 0; i < filterSize; i++)
                        {
                            int wX = x - filterSize / 2 + i;
                            wX = (wX < 0) ? 0 : wX;
                            wX = (wX >= image.Width) ? image.Width - 1 : wX;
                            intensity += image.GetPixel(wX, wY).R;
                        }
                    }
                    intensity /= (filterSize * filterSize);
                    result.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }
        }

        private void _checkBoxSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBoxSmoothing.Checked == false)
            {
                _lbResult.SetSelected(0, true);
                return;
            }
            _radioButtonMeanSmoothing.Enabled = true;
            _radioButtonMedianSmoothing.Enabled = true;

            Bitmap inputImage = _resultImages[_lbResult.SelectedIndex];
            Bitmap result;
            TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Origin Image");
            resultName.Add("Gray Level Image");
            resultName.Add("Smoothing image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(inputImage);
            results.Add(result);
            inputImage = result;
            _smoothingMethod(ref inputImage, out result, 3);//set filter size is 3;
            results.Add(result);

            ShowResult(ref results, ref resultName, true);
        }

        static public void MedianSmoothing(ref Bitmap image, out Bitmap result, int filterSize)//the image should be a gray level image
        {
            if (filterSize % 2 != 1)
            {
                result = new Bitmap(image);
                return;
            }
            result = new Bitmap(image.Width, image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    List<int> intensity = new List<int>();
                    for (int j = 0; j < filterSize; j++)//Use replicate to interpolate the pixel when the filter position is out of the boundary of the source image
                    {
                        int wY = y - filterSize / 2 + j;
                        wY = (wY < 0) ? 0 : wY;
                        wY = (wY >= image.Height) ? image.Height - 1 : wY;
                        for (int i = 0; i < filterSize; i++)
                        {
                            int wX = x - filterSize / 2 + i;
                            wX = (wX < 0) ? 0 : wX;
                            wX = (wX >= image.Width) ? image.Width - 1 : wX;
                            intensity.Add((image.GetPixel(wX, wY).R));
                        }
                    }
                    intensity.Sort();
                    int median = intensity.Count / 2 + 1;
                    result.SetPixel(x, y, Color.FromArgb(intensity[median], intensity[median], intensity[median]));
                }
            }
        }

        private void _radioButtonMeanSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioButtonMeanSmoothing.Checked == true)
            {
                _smoothingMethod = MeanSmoothing;
                _lbResult.SetSelected(0, true);
                _checkBoxSmoothing_CheckedChanged(sender, e);
            }
        }

        private void _radioButtonMedianSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioButtonMedianSmoothing.Checked == true)
            {
                _smoothingMethod = MedianSmoothing;
                _lbResult.SetSelected(0, true);
                _checkBoxSmoothing_CheckedChanged(sender, e);
            }
        }
    }
}
