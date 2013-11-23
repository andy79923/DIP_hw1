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

        static public void RGBExtraction(Bitmap image, out List<Bitmap> results)
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
             RGBExtraction(_inputImages[_lbInputImage.SelectedIndex], out _resultImages);

            List<string> resultName = new List<string>();
            resultName.Add("Red channel");
            resultName.Add("Green channel");
            resultName.Add("Blue channel");
            ShowResult(ref resultName);
        }

        private void ShowResult(ref List<string> names)
        {
            _lbResult.Items.Clear();
            for (int i = 0; i < names.Count; i++)
            {
                _lbResult.Items.Add(names[i]);
            }
            _pbResult.Image = _resultImages[0];
            _lbResult.SetSelected(0, true);

        }

        private void _lbResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            _pbResult.Image = _resultImages[_lbResult.SelectedIndex];
        }
    }
}
