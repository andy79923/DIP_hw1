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
        }

        private void _bnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.InitialDirectory = "C:";
            openfileDialog.Filter = "All Files|*.*|Bitmap Files (.bmp)|*.bmp";
            if (openfileDialog.ShowDialog() == DialogResult.OK)
            {
                _inputImages.Add(new Bitmap(openfileDialog.FileName));
            }
        }
    }
}
