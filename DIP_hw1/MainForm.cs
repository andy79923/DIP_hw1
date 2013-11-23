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
    }
}
