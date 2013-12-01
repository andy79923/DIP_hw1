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
            _smoothingMethod = MeanSmoothing;
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
            _buttonGrayLevel.Enabled = true;
            _buttonRGBExtraction.Enabled = true;
            _checkBoxThresholding.Enabled = true;
            _checkBoxThresholding.Checked = false;
            _checkBoxSmoothing.Enabled = true;
            _checkBoxSmoothing.Checked = false;
            _buttonHistogramEqualization.Enabled = true;
            _buttonSobel.Enabled=true;
            _trackBarStretchingHorizontalScale.Enabled = true;
            _textBoxStretchingHorizontalScale.Enabled = true;
            _trackBarStretchingVerticalScale.Enabled = true;
            _trackBarStretchingVerticalScale.Value = 100;
            _trackBarStretchingHorizontalScale.Value = 100;
            _textBoxStretchingVerticalScale.Enabled = true;
            _trackBarRotation.Enabled = true;
            _trackBarRotation.Value = 0;
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

        private void _textBox_KeyPress(object sender, KeyPressEventArgs e)
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
            TranslateGrayLevel(ref inputImage, out result);

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
                _listBoxResult.SetSelected(0, true);
                _checkBoxSmoothing_CheckedChanged(sender, e);
            }
        }

        private void _radioButtonMedianSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioButtonMedianSmoothing.Checked == true)
            {
                _smoothingMethod = MedianSmoothing;
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

        static public void HistogramEqualization(ref Bitmap image, out Bitmap result)//the image should be a gray level image
        {
            result = new Bitmap(image.Width, image.Height);
            double[] histogram = new double[256];
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int intensity = image.GetPixel(x, y).R;
                    histogram[intensity]+=1;
                }
            }
            for (int i = 1; i < 256; i++)
            {
                histogram[i] += histogram[i - 1];
            }
            double ratio = 255 / histogram[255];
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int intensity = Convert.ToInt32(Math.Round(histogram[image.GetPixel(x, y).R] * ratio));
                    result.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }

        }

        private void _buttonHistogramEqualization_Click(object sender, EventArgs e)
        {
            Bitmap inputImage = _resultImages[_listBoxResult.SelectedIndex];
            Bitmap result;
            TranslateGrayLevel(ref inputImage, out result);

            List<string> resultName = new List<string>();
            resultName.Add("Origin Image");
            resultName.Add("Gray Level Image");
            resultName.Add("Histogram Equalization");
            List<Bitmap> results = new List<Bitmap>();
            results.Add(inputImage);
            results.Add(result);
            inputImage = result;

            HistogramEqualization(ref inputImage, out result);
            results.Add(result);

            ShowResult(ref results, ref resultName, true);
        }

        static public void Sobel(ref Bitmap image, out Bitmap result, bool x_order)//the image should be a gray level image
        {
            result = new Bitmap(image.Width, image.Height);
            int[,] filter = (x_order == true) ? new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } } : new int[,] { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    int intensity = 0;
                    for (int j = 0; j < 3; j++)//Use replicate to interpolate the pixel when the filter position is out of the boundary of the source image
                    {
                        int wY = y - 3 / 2 + j;
                        wY = (wY < 0) ? 0 : wY;
                        wY = (wY >= image.Height) ? image.Height - 1 : wY;
                        for (int i = 0; i < 3; i++)
                        {
                            int wX = x - 3 / 2 + i;
                            wX = (wX < 0) ? 0 : wX;
                            wX = (wX >= image.Width) ? image.Width - 1 : wX;
                            intensity += (image.GetPixel(wX, wY).R * filter[i, j]);
                        }
                    }
                    intensity = Math.Abs(intensity);
                    intensity = (intensity > 255) ? 255 : intensity;
                    result.SetPixel(x, y, Color.FromArgb(intensity, intensity, intensity));
                }
            }
        }

        private void _buttonSobel_Click(object sender, EventArgs e)
        {
            _buttonOverlap.Enabled = true;
            Bitmap inputImage = _resultImages[_listBoxResult.SelectedIndex];
            Bitmap result, vertical, horizontal;

            TranslateGrayLevel(ref inputImage, out result);

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

            Sobel(ref inputImage, out vertical, false);
            Sobel(ref inputImage, out horizontal, true);
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

        static public void Stretching(ref Bitmap image, out Bitmap result, double xScale, double yScale)//the image should be a gray level image
        {
            result = new Bitmap(Convert.ToInt32(Convert.ToDouble(image.Width)*xScale), Convert.ToInt32(Convert.ToDouble(image.Height)*yScale));
            double[,] invertibleTransformation = new double[2, 2] { { yScale / (xScale * yScale), 0 }, { 0, xScale / (xScale * yScale) } };

            for (int rY = 0; rY < result.Height; rY++)
            {
                for (int rX = 0; rX < result.Width; rX++)
                {
                    double x = invertibleTransformation[0, 0] * rX + invertibleTransformation[0, 1] * rY;
                    double y = invertibleTransformation[1, 0] * rX + invertibleTransformation[1, 1] * rY;
                    int leftX = (int)x;
                    int rightX = (leftX + 1 >= image.Width) ? leftX : leftX + 1;
                    int topY = (int)y;
                    int bottomY = (topY + 1 >= image.Height) ? topY : topY + 1;
                    double alpha = x - (double)leftX;
                    double beta = y - (double)topY;
                    int intensity = (int)(Math.Round((1 - alpha) * (1 - beta) * (double)image.GetPixel(leftX, topY).R + alpha * (1 - beta) * (double)image.GetPixel(rightX, topY).R
                                    + (1 - alpha) * beta * (double)image.GetPixel(leftX, bottomY).R + alpha * beta * (double)image.GetPixel(rightX, bottomY).R));
                    result.SetPixel(rX, rY, Color.FromArgb(intensity, intensity, intensity));
                }
            }
        }

        private void _trackBarStretching_ValueChange(object sender, EventArgs e)
        {
            Bitmap inputImage = _resultImages[0];
            Bitmap result;
            List<string> resultName = new List<string>();
            List<Bitmap> results = new List<Bitmap>();
            if (_listBoxResult.Items.Count < 2 || _listBoxResult.Items[1].ToString() != "Gray Level Image")
            {
                TranslateGrayLevel(ref inputImage, out result);
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
            Stretching(ref inputImage, out result, (double)_trackBarStretchingHorizontalScale.Value / 100, (double)_trackBarStretchingVerticalScale.Value / 100);

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

        static public void Rotation(ref Bitmap image, out Bitmap result, double angle, PointF center)//the image should be a gray level image
        {
            double[,] transform = new double[2, 2] { { Math.Cos(angle), -Math.Sin(angle) }, { Math.Sin(angle), Math.Cos(angle) } };
            PointF p1 = new PointF(0, 0);
            PointF p2 = new PointF(0, image.Height);
            PointF p3 = new PointF(image.Width, 0);
            PointF p4 = new PointF(image.Width, image.Height);
            PointF np1 = new PointF((float)Math.Round(transform[0, 0] * p1.X + transform[0, 1] * p1.Y), (float)Math.Round(transform[1, 0] * p1.X + transform[1, 1] * p1.Y));
            PointF np2 = new PointF((float)Math.Round(transform[0, 0] * p2.X + transform[0, 1] * p2.Y), (float)Math.Round(transform[1, 0] * p2.X + transform[1, 1] * p2.Y));
            PointF np3 = new PointF((float)Math.Round(transform[0, 0] * p3.X + transform[0, 1] * p3.Y), (float)Math.Round(transform[1, 0] * p3.X + transform[1, 1] * p3.Y));
            PointF np4 = new PointF((float)Math.Round(transform[0, 0] * p4.X + transform[0, 1] * p4.Y), (float)Math.Round(transform[1, 0] * p4.X + transform[1, 1] * p4.Y));

            double minX = np1.X, maxX = np1.X, minY = np1.Y, maxY = np1.Y;
            minX = (np2.X < minX) ? np2.X : minX;
            minX = (np3.X < minX) ? np3.X : minX;
            minX = (np4.X < minX) ? np4.X : minX;
            maxX = (np2.X > maxX) ? np2.X : maxX;
            maxX = (np3.X > maxX) ? np3.X : maxX;
            maxX = (np4.X > maxX) ? np4.X : maxX;
            minY = (np2.Y < minY) ? np2.Y : minY;
            minY = (np3.Y < minY) ? np3.Y : minY;
            minY = (np4.Y < minY) ? np4.Y : minY;
            maxY = (np2.Y > maxY) ? np2.Y : maxY;
            maxY = (np3.Y > maxY) ? np3.Y : maxY;
            maxY = (np4.Y > maxY) ? np4.Y : maxY;


            result = new Bitmap((int)(maxX - minX), (int)(maxY - minY));
            PointF resultTranslate = new PointF((float)(result.Width - 1) / 2, (float)(result.Height - 1) / 2);
            double transformDivision = transform[0, 0] * transform[1, 1] - transform[0, 1] * transform[1, 0];
            double[,] invertableTransform = new double[2, 2] { { transform[1, 1] / transformDivision, -transform[0, 1] / transformDivision }, { -transform[1, 0] / transformDivision, transform[0, 0] / transformDivision } };

            for (int rY = 0; rY < result.Height; rY++)
            {
                for (int rX = 0; rX < result.Width; rX++)
                {
                    double x = invertableTransform[0, 0] * ((double)rX - resultTranslate.X) + invertableTransform[0, 1] * ((double)rY - resultTranslate.Y) + center.X;
                    double y = invertableTransform[1, 0] * ((double)rX - resultTranslate.X) + invertableTransform[1, 1] * ((double)rY - resultTranslate.Y) + center.Y;
                    
                    int leftX = (int)x;
                    int rightX = (leftX + 1 >= image.Width) ? leftX : leftX + 1;
                    int topY = (int)y;
                    int bottomY = (topY + 1 >= image.Height) ? topY : topY + 1;
                    double alpha = x - (double)leftX;
                    double beta = y - (double)topY;
                    leftX = (leftX  == -1) ? leftX + 1 : leftX;
                    rightX = (rightX == image.Width) ? rightX - 1 : rightX;
                    topY = (topY  == -1) ? topY + 1 : topY;
                    bottomY = (bottomY == image.Height) ? bottomY - 1 : bottomY;
                    double leftTopPixel = (leftX < 0 || leftX >= image.Width || topY < 0 || topY >= image.Height) ? 0 : (double)image.GetPixel(leftX, topY).R;
                    double rightTopPixel = (rightX < 0 || rightX >= image.Width || topY < 0 || topY >= image.Height) ? 0 : (double)image.GetPixel(rightX, topY).R;
                    double leftBottomPixel = (leftX < 0 || leftX >= image.Width || bottomY < 0 || bottomY >= image.Height) ? 0 : (double)image.GetPixel(leftX, bottomY).R;
                    double rightBottomPixel = (rightX < 0 || rightX >= image.Width || bottomY < 0 || bottomY >= image.Height) ? 0 : (double)image.GetPixel(rightX, bottomY).R;

                    int intensity = (int)(Math.Round((1 - alpha) * (1 - beta) * leftTopPixel + alpha * (1 - beta) * rightTopPixel + (1 - alpha) * beta * leftBottomPixel + alpha * beta * rightBottomPixel));
                    intensity = (intensity < 0) ? 0 : intensity;
                    intensity = (intensity > 255) ? 255 : intensity;
                    result.SetPixel(rX, rY, Color.FromArgb(intensity, intensity, intensity));
                }
            }
        }

        private void _trackBarRotation_ValueChange(object sender, EventArgs e)
        {
            Bitmap inputImage = _resultImages[0];
            Bitmap result;
            List<string> resultName = new List<string>();
            List<Bitmap> results = new List<Bitmap>();
            if (_listBoxResult.Items.Count < 2 || _listBoxResult.Items[1].ToString() != "Gray Level Image")
            {
                TranslateGrayLevel(ref inputImage, out result);
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
            Rotation(ref inputImage, out result, (double)_trackBarRotation.Value * Math.PI / 180.0, new PointF((float)(inputImage.Width - 1) / 2, (float)(inputImage.Height - 1) / 2));

            resultName.Add("Rotation Image (" + _trackBarRotation.Value.ToString() + ")");
            results.Add(result);

            ShowResult(ref results, ref resultName, false);
        }
    }
}
