﻿using System;
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
            _smoothingMethod = ImageProcessing.MeanSmoothing;
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
                    _buttonGrayLevel.Enabled = true;
                    _buttonRGBExtraction.Enabled = true;
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
            
            _checkBoxThresholding.Enabled = false;
            _checkBoxThresholding.Checked = false;
            _trackBarThresholding.Enabled = false;
            _trackBarThresholding.Value = 0;
            _textBoxThresholding.Enabled = false;
            _textBoxThresholding.Text = "0";

            _checkBoxSmoothing.Enabled = true;
            _checkBoxSmoothing.Checked = false;
            _radioButtonMeanSmoothing.Enabled = false;
            _radioButtonMeanSmoothing.Checked = true;
            _radioButtonMedianSmoothing.Enabled = false;
            _textBoxSmoothing.Enabled = false;
            _textBoxSmoothing.Text = "3";

            _buttonHistogramEqualization.Enabled = false;
            _buttonSobel.Enabled = false;
            _buttonOverlap.Enabled = false;

            _trackBarStretchingHorizontalScale.Enabled = true;
            _trackBarStretchingHorizontalScale.Value = 100;
            _textBoxStretchingHorizontalScale.Enabled = false;
            _textBoxStretchingHorizontalScale.Text = "1";

            _trackBarStretchingVerticalScale.Enabled = false;
            _trackBarStretchingVerticalScale.Value = 100;
            _textBoxStretchingVerticalScale.Enabled = false;
            _textBoxStretchingVerticalScale.Text = "1";
            
            _trackBarRotation.Enabled = false;
            _trackBarRotation.Value = 0;
            _textBoxRotation.Enabled = false;
            _textBoxRotation.Text = "0";

        }

        private void _bnRGBExtraction_Click(object sender, EventArgs e)
        {
            Bitmap inputImage = _inputImages[_listBoxInput.SelectedIndex];
            List<Bitmap> results;
            ImageProcessing.RGBExtraction(ref inputImage, out results);

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

        private void _bnGrayLevel_Click(object sender, EventArgs e)
        {
            Bitmap inputImage = _inputImages[_listBoxInput.SelectedIndex];
            Bitmap result;
            ImageProcessing.TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Gray Level Image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(result);            
            ShowResult(ref results, ref resultName, true);
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
            ImageProcessing.TranslateGrayLevel(ref inputImage, out result);

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
            ImageProcessing.Thresholding(ref inputImage, out result, _trackBarThresholding.Value);

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

        private void _textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void _checkBoxSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (_checkBoxSmoothing.Checked == false)
            {
                _listBoxResult.SetSelected(0, true);
                _radioButtonMeanSmoothing.Enabled = false;
                _radioButtonMeanSmoothing.Checked = true;
                _radioButtonMedianSmoothing.Enabled = false;
                _textBoxSmoothing.Enabled = false;
                _textBoxSmoothing.Text = "3";
                return;
            }
            _radioButtonMeanSmoothing.Enabled = true;
            _radioButtonMedianSmoothing.Enabled = true;
            _textBoxSmoothing.Enabled = true;

            Bitmap inputImage = _resultImages[_listBoxResult.SelectedIndex];
            Bitmap result;
            ImageProcessing.TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Origin Image");
            resultName.Add("Gray Level Image");
            resultName.Add("Smoothing image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(inputImage);
            results.Add(result);
            inputImage = result;

            _smoothingMethod(ref inputImage, out result, (_textBoxSmoothing.Text == "1") ? 3 : Convert.ToInt32(_textBoxSmoothing.Text));//if filter size is 1, replace the value with 3
            results.Add(result);

            ShowResult(ref results, ref resultName, true);
        }

        private void _radioButtonMeanSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioButtonMeanSmoothing.Checked == true)
            {
                _smoothingMethod = ImageProcessing.MeanSmoothing;
                _listBoxResult.SetSelected(0, true);
                _checkBoxSmoothing_CheckedChanged(sender, e);
            }
        }

        private void _radioButtonMedianSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioButtonMedianSmoothing.Checked == true)
            {
                _smoothingMethod = ImageProcessing.MedianSmoothing;
                _listBoxResult.SetSelected(0, true);
                _checkBoxSmoothing_CheckedChanged(sender, e);
            }
        }

        private void _textBoxSmoothing_TextChanged(object sender, EventArgs e)
        {
            if (_textBoxSmoothing.Text != "")
            {
                _listBoxResult.SetSelected(0, true);
                _checkBoxSmoothing_CheckedChanged(sender, e);
            }
            
        }

        private void _buttonHistogramEqualization_Click(object sender, EventArgs e)
        {
            Bitmap inputImage = _resultImages[_listBoxResult.SelectedIndex];
            Bitmap result;
            ImageProcessing.TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Origin Image");
            resultName.Add("Gray Level Image");
            resultName.Add("Histogram Equalization");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(inputImage);
            results.Add(result);
            inputImage = result;

            ImageProcessing.HistogramEqualization(ref inputImage, out result);
            results.Add(result);

            ShowResult(ref results, ref resultName, true);
        }

        private void _buttonSobel_Click(object sender, EventArgs e)
        {
            _buttonOverlap.Enabled = true;
            Bitmap inputImage = _resultImages[_listBoxResult.SelectedIndex];
            Bitmap result, vertical, horizontal;

            ImageProcessing.TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Origin Image");
            resultName.Add("Gray Level Image");
            resultName.Add("Vertical Edge");
            resultName.Add("Horizontal Edge");
            resultName.Add("Combined Image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(inputImage);
            results.Add(result);
            inputImage = result;

            ImageProcessing.Sobel(ref inputImage, out vertical, false);
            ImageProcessing.Sobel(ref inputImage, out horizontal, true);
            result = new Bitmap(inputImage.Width, inputImage.Height);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    int intensity = vertical.GetPixel(x, y).R + horizontal.GetPixel(x, y).R;
                    intensity = (intensity > 255) ? 255 : intensity;
                    result.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }

            results.Add(vertical);
            results.Add(horizontal);
            results.Add(result);

            ShowResult(ref results, ref resultName, true);
        }

        private void _buttonOverlap_Click(object sender, EventArgs e)
        {
            _buttonOverlap.Enabled = false;
            Bitmap inputImage = _inputImages[_listBoxInput.SelectedIndex];
            Bitmap edge = _resultImages[_listBoxResult.SelectedIndex];
            Bitmap result = new Bitmap(inputImage);
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    if (edge.GetPixel(x, y).R == 255)
                    {
                        result.SetPixel(x, y, Color.FromArgb(0, 255, 0));
                    }
                }
            }

            List<string> resultName = new List<string>();
            resultName.Add("Overlap Image");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(result);

            ShowResult(ref results, ref resultName, false);

        }

        private void _trackBarStretching_ValueChange(object sender, EventArgs e)
        {
            Bitmap inputImage = _resultImages[0];
            Bitmap result;
            List<string> resultName = new List<string>();
            List<Bitmap> results = new List<Bitmap>();
            if (_listBoxResult.Items.Count < 2 || _listBoxResult.Items[1].ToString() != "Gray Level Image")
            {
                ImageProcessing.TranslateGrayLevel(ref inputImage, out result);
                results.Add(inputImage);
                results.Add(result);
                inputImage = result;
                resultName.Add("Origin Image");
                resultName.Add("Gray Level Image");
                ShowResult(ref results, ref resultName, true);
                results.Clear();
                resultName.Clear();

            }
            inputImage = _resultImages[1];
            ImageProcessing.Stretching(ref inputImage, out result, (double)_trackBarStretchingHorizontalScale.Value / 100, (double)_trackBarStretchingVerticalScale.Value / 100);

            resultName.Add("Stretching Image (x = " + ((double)_trackBarStretchingHorizontalScale.Value / 100).ToString() + ", y =" + ((double)_trackBarStretchingVerticalScale.Value / 100).ToString() + ")");
            results.Add(result);

            _textBoxStretchingHorizontalScale.Text = ((double)_trackBarStretchingHorizontalScale.Value / 100).ToString();
            _textBoxStretchingVerticalScale.Text = ((double)_trackBarStretchingVerticalScale.Value / 100).ToString();

            ShowResult(ref results, ref resultName, false);

        }

        private void _textBoxStretchingHorizontalScale_TextChanged(object sender, EventArgs e)
        {
            if (_textBoxStretchingHorizontalScale.Text == "" || Convert.ToDouble(_textBoxStretchingHorizontalScale.Text) == 0)
            {
                return;
            }
            if (Convert.ToDouble(_textBoxStretchingHorizontalScale.Text) > 2)
            {
                _textBoxStretchingHorizontalScale.Text = "2.0";
            }
            _trackBarStretchingHorizontalScale.Value = (int)(Convert.ToDouble((_textBoxStretchingHorizontalScale.Text)) * 100);
        }

        private void _textBox_double_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (e.KeyChar != 8 && (text.Text.Length >= 4))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '.' && text.Text.Length != 1)
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar != 8 && e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void _textBoxStretchingVerticalScale_TextChanged(object sender, EventArgs e)
        {
            if (_textBoxStretchingVerticalScale.Text == "" || Convert.ToDouble(_textBoxStretchingVerticalScale.Text) == 0)
            {
                return;
            }
            if (Convert.ToDouble(_textBoxStretchingVerticalScale.Text) > 2)
            {
                _textBoxStretchingVerticalScale.Text = "2.0";
            }
            _trackBarStretchingVerticalScale.Value = (int)(Convert.ToDouble((_textBoxStretchingVerticalScale.Text)) * 100);
        }

        private void _trackBarRotation_ValueChange(object sender, EventArgs e)
        {
            Bitmap inputImage = _resultImages[0];
            Bitmap result;
            List<string> resultName = new List<string>();
            List<Bitmap> results = new List<Bitmap>();
            if (_listBoxResult.Items.Count < 2 || _listBoxResult.Items[1].ToString() != "Gray Level Image")
            {
                ImageProcessing.TranslateGrayLevel(ref inputImage, out result);
                results.Add(inputImage);
                results.Add(result);
                inputImage = result;
                resultName.Add("Origin Image");
                resultName.Add("Gray Level Image");
                ShowResult(ref results, ref resultName, true);
                results.Clear();
                resultName.Clear();

            }
            inputImage = _resultImages[1];
            ImageProcessing.Rotation(ref inputImage, out result, (double)_trackBarRotation.Value * Math.PI / 180.0, new PointF((float)(inputImage.Width - 1) / 2, (float)(inputImage.Height - 1) / 2));

            resultName.Add("Rotation Image (" + _trackBarRotation.Value.ToString() + ")");
            results.Add(result);

            _textBoxRotation.Text = _trackBarRotation.Value.ToString();
            ShowResult(ref results, ref resultName, false);
        }

        private void _textBoxTrtation_TextChanged(object sender, EventArgs e)
        {
            if (_textBoxRotation.Text == "")
            {
                return;
            }
            if (Convert.ToInt32(_textBoxRotation.Text) > 360)
            {
                _textBoxRotation.Text = "360";
            }
            _trackBarRotation.Value = Convert.ToInt32(_textBoxRotation.Text);
        }
    }
}
