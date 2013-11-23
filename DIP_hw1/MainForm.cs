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
        }

        private void _bnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.InitialDirectory = "C:";
            openfileDialog.Filter = "Bitmap Files (.bmp)|*.bmp|JPEG (.jpg)|*.jpg|PNG (.png)|*.png|All Files|*.*";
            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                int index = _lbInputImage.FindString(openfileDialog.FileName);
                if (index == ListBox.NoMatches)
                {
                    _lbInputImage.Items.Add(openfileDialog.FileName);
                    _inputImages.Add(new Bitmap(openfileDialog.FileName));
                    index = _lbInputImage.Items.Count - 1;
                }
                _lbInputImage.SetSelected(index, true);
            }
        }

        private void _lbInputImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pbInputImage.Image = _inputImages[_lbInputImage.SelectedIndex];
        }

        private void RGBExtraction(ref List<Bitmap> results)
        {
            results.Clear();
            Bitmap inputImage = new Bitmap(_inputImages[_lbInputImage.SelectedIndex]);
            Bitmap RImage = new Bitmap(inputImage.Width, inputImage.Height);
            Bitmap GImage = new Bitmap(inputImage.Width, inputImage.Height);
            Bitmap BImage = new Bitmap(inputImage.Width, inputImage.Height);

            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    Color RGB = inputImage.GetPixel(x, y);
                    RImage.SetPixel(x, y, Color.FromArgb(RGB.R, RGB.R, RGB.R));
                    GImage.SetPixel(x, y, Color.FromArgb(RGB.G, RGB.G, RGB.G));
                    BImage.SetPixel(x, y, Color.FromArgb(RGB.B, RGB.B, RGB.B));
                }
            }
            results.Add(new Bitmap(RImage));
            results.Add(new Bitmap(GImage));
            results.Add(new Bitmap(BImage));
        }

        private void _bnRGBExtraction_Click(object sender, EventArgs e)
        {
            if (_inputImages.Count == 0)
            {
                return;
            }
            _lbResult.Items.Clear();

            RGBExtraction(ref _resultImages);
            _lbResult.Items.Add("Red channel");
            _lbResult.Items.Add("Green channel");
            _lbResult.Items.Add("Blue channel");
            _pbResult.Image = _resultImages[0];

        }

        private void _lbResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pbResult.Image = _resultImages[_lbResult.SelectedIndex];
        }

        static private void TranslateGrayLevel(ref Bitmap image, out Bitmap result)
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
    }
}
