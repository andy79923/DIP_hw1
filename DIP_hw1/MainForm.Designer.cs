﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace DIP_hw1
{
    delegate void RadioButtonSmoothing(ref Bitmap image, out Bitmap result, int filterSize);
    partial class MainForm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this._buttonLoadImage = new System.Windows.Forms.Button();
            this._pictureBoxInput = new System.Windows.Forms.PictureBox();
            this._listBoxInput = new System.Windows.Forms.ListBox();
            this._buttonRGBExtraction = new System.Windows.Forms.Button();
            this._pictureBoxResult = new System.Windows.Forms.PictureBox();
            this._listBoxResult = new System.Windows.Forms.ListBox();
            this._buttonGrayLevel = new System.Windows.Forms.Button();
            this._checkBoxThresholding = new System.Windows.Forms.CheckBox();
            this._trackBarThresholding = new System.Windows.Forms.TrackBar();
            this._textBoxThresholding = new System.Windows.Forms.TextBox();
            this._checkBoxSmoothing = new System.Windows.Forms.CheckBox();
            this._radioButtonMeanSmoothing = new System.Windows.Forms.RadioButton();
            this._radioButtonMedianSmoothing = new System.Windows.Forms.RadioButton();
            this._textBoxSmoothing = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._buttonHistogramEqualization = new System.Windows.Forms.Button();
            this._buttonSobel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._buttonOverlap = new System.Windows.Forms.Button();
            this._trackBarStretchingHorizontalScale = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this._textBoxStretchingHorizontalScale = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._trackBarStretchingVerticalScale = new System.Windows.Forms.TrackBar();
            this._textBoxStretchingVerticalScale = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this._trackBarRotation = new System.Windows.Forms.TrackBar();
            this._textBoxRotation = new System.Windows.Forms.TextBox();
            this._buttonDeleteFinalResult = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarThresholding)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarStretchingHorizontalScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarStretchingVerticalScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarRotation)).BeginInit();
            this.SuspendLayout();
            // 
            // _buttonLoadImage
            // 
            this._buttonLoadImage.Location = new System.Drawing.Point(35, 434);
            this._buttonLoadImage.Name = "_buttonLoadImage";
            this._buttonLoadImage.Size = new System.Drawing.Size(75, 23);
            this._buttonLoadImage.TabIndex = 0;
            this._buttonLoadImage.Text = "Load Image";
            this._buttonLoadImage.UseVisualStyleBackColor = true;
            this._buttonLoadImage.Click += new System.EventHandler(this._bnLoadImage_Click);
            // 
            // _pictureBoxInput
            // 
            this._pictureBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBoxInput.Location = new System.Drawing.Point(150, 10);
            this._pictureBoxInput.Name = "_pictureBoxInput";
            this._pictureBoxInput.Size = new System.Drawing.Size(512, 512);
            this._pictureBoxInput.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._pictureBoxInput.TabIndex = 1;
            this._pictureBoxInput.TabStop = false;
            // 
            // _listBoxInput
            // 
            this._listBoxInput.FormattingEnabled = true;
            this._listBoxInput.HorizontalScrollbar = true;
            this._listBoxInput.ItemHeight = 12;
            this._listBoxInput.Location = new System.Drawing.Point(10, 10);
            this._listBoxInput.Name = "_listBoxInput";
            this._listBoxInput.Size = new System.Drawing.Size(120, 400);
            this._listBoxInput.TabIndex = 2;
            this._listBoxInput.SelectedIndexChanged += new System.EventHandler(this._lbInputImage_SelectedIndexChanged);
            // 
            // _buttonRGBExtraction
            // 
            this._buttonRGBExtraction.Enabled = false;
            this._buttonRGBExtraction.Location = new System.Drawing.Point(35, 477);
            this._buttonRGBExtraction.Name = "_buttonRGBExtraction";
            this._buttonRGBExtraction.Size = new System.Drawing.Size(95, 23);
            this._buttonRGBExtraction.TabIndex = 3;
            this._buttonRGBExtraction.Text = "RGB Extraction";
            this._buttonRGBExtraction.UseVisualStyleBackColor = true;
            this._buttonRGBExtraction.Click += new System.EventHandler(this._bnRGBExtraction_Click);
            // 
            // _pictureBoxResult
            // 
            this._pictureBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pictureBoxResult.Location = new System.Drawing.Point(680, 10);
            this._pictureBoxResult.Name = "_pictureBoxResult";
            this._pictureBoxResult.Size = new System.Drawing.Size(512, 512);
            this._pictureBoxResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._pictureBoxResult.TabIndex = 3;
            this._pictureBoxResult.TabStop = false;
            // 
            // _listBoxResult
            // 
            this._listBoxResult.FormattingEnabled = true;
            this._listBoxResult.HorizontalScrollbar = true;
            this._listBoxResult.ItemHeight = 12;
            this._listBoxResult.Location = new System.Drawing.Point(1212, 10);
            this._listBoxResult.Name = "_listBoxResult";
            this._listBoxResult.Size = new System.Drawing.Size(120, 400);
            this._listBoxResult.TabIndex = 4;
            this._listBoxResult.SelectedIndexChanged += new System.EventHandler(this._lbResult_SelectedIndexChanged);
            // 
            // _buttonGrayLevel
            // 
            this._buttonGrayLevel.Enabled = false;
            this._buttonGrayLevel.Location = new System.Drawing.Point(35, 528);
            this._buttonGrayLevel.Name = "_buttonGrayLevel";
            this._buttonGrayLevel.Size = new System.Drawing.Size(154, 23);
            this._buttonGrayLevel.TabIndex = 5;
            this._buttonGrayLevel.Text = "Translate to Gray Level Image";
            this._buttonGrayLevel.UseVisualStyleBackColor = true;
            this._buttonGrayLevel.Click += new System.EventHandler(this._bnGrayLevel_Click);
            // 
            // _checkBoxThresholding
            // 
            this._checkBoxThresholding.AutoSize = true;
            this._checkBoxThresholding.Enabled = false;
            this._checkBoxThresholding.Location = new System.Drawing.Point(35, 583);
            this._checkBoxThresholding.Name = "_checkBoxThresholding";
            this._checkBoxThresholding.Size = new System.Drawing.Size(118, 16);
            this._checkBoxThresholding.TabIndex = 6;
            this._checkBoxThresholding.Text = "Apply Thresholding";
            this._checkBoxThresholding.UseVisualStyleBackColor = true;
            this._checkBoxThresholding.CheckedChanged += new System.EventHandler(this._cbThresholding_CheckedChanged);
            // 
            // _trackBarThresholding
            // 
            this._trackBarThresholding.Enabled = false;
            this._trackBarThresholding.Location = new System.Drawing.Point(150, 583);
            this._trackBarThresholding.Maximum = 255;
            this._trackBarThresholding.Name = "_trackBarThresholding";
            this._trackBarThresholding.Size = new System.Drawing.Size(104, 45);
            this._trackBarThresholding.TabIndex = 7;
            this._trackBarThresholding.TickStyle = System.Windows.Forms.TickStyle.None;
            this._trackBarThresholding.ValueChanged += new System.EventHandler(this._tbThresholding_ValueChange);
            // 
            // _textBoxThresholding
            // 
            this._textBoxThresholding.Enabled = false;
            this._textBoxThresholding.Location = new System.Drawing.Point(261, 583);
            this._textBoxThresholding.Name = "_textBoxThresholding";
            this._textBoxThresholding.Size = new System.Drawing.Size(30, 22);
            this._textBoxThresholding.TabIndex = 8;
            this._textBoxThresholding.Text = "0";
            this._textBoxThresholding.TextChanged += new System.EventHandler(this._textBoxThresholding_TextChanged);
            this._textBoxThresholding.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBox_KeyPress);
            // 
            // _checkBoxSmoothing
            // 
            this._checkBoxSmoothing.AutoSize = true;
            this._checkBoxSmoothing.Enabled = false;
            this._checkBoxSmoothing.Location = new System.Drawing.Point(35, 632);
            this._checkBoxSmoothing.Name = "_checkBoxSmoothing";
            this._checkBoxSmoothing.Size = new System.Drawing.Size(107, 16);
            this._checkBoxSmoothing.TabIndex = 9;
            this._checkBoxSmoothing.Text = "Apply Smoothing";
            this._checkBoxSmoothing.UseVisualStyleBackColor = true;
            this._checkBoxSmoothing.CheckedChanged += new System.EventHandler(this._checkBoxSmoothing_CheckedChanged);
            // 
            // _radioButtonMeanSmoothing
            // 
            this._radioButtonMeanSmoothing.AutoSize = true;
            this._radioButtonMeanSmoothing.Checked = true;
            this._radioButtonMeanSmoothing.Enabled = false;
            this._radioButtonMeanSmoothing.Location = new System.Drawing.Point(6, 21);
            this._radioButtonMeanSmoothing.Name = "_radioButtonMeanSmoothing";
            this._radioButtonMeanSmoothing.Size = new System.Drawing.Size(49, 16);
            this._radioButtonMeanSmoothing.TabIndex = 10;
            this._radioButtonMeanSmoothing.TabStop = true;
            this._radioButtonMeanSmoothing.Text = "Mean";
            this._radioButtonMeanSmoothing.UseVisualStyleBackColor = true;
            this._radioButtonMeanSmoothing.CheckedChanged += new System.EventHandler(this._radioButtonMeanSmoothing_CheckedChanged);
            // 
            // _radioButtonMedianSmoothing
            // 
            this._radioButtonMedianSmoothing.AutoSize = true;
            this._radioButtonMedianSmoothing.Enabled = false;
            this._radioButtonMedianSmoothing.Location = new System.Drawing.Point(61, 22);
            this._radioButtonMedianSmoothing.Name = "_radioButtonMedianSmoothing";
            this._radioButtonMedianSmoothing.Size = new System.Drawing.Size(58, 16);
            this._radioButtonMedianSmoothing.TabIndex = 11;
            this._radioButtonMedianSmoothing.TabStop = true;
            this._radioButtonMedianSmoothing.Text = "Median";
            this._radioButtonMedianSmoothing.UseVisualStyleBackColor = true;
            this._radioButtonMedianSmoothing.CheckedChanged += new System.EventHandler(this._radioButtonMedianSmoothing_CheckedChanged);
            // 
            // _textBoxSmoothing
            // 
            this._textBoxSmoothing.Enabled = false;
            this._textBoxSmoothing.Location = new System.Drawing.Point(335, 630);
            this._textBoxSmoothing.Name = "_textBoxSmoothing";
            this._textBoxSmoothing.Size = new System.Drawing.Size(28, 22);
            this._textBoxSmoothing.TabIndex = 12;
            this._textBoxSmoothing.Text = "3";
            this._textBoxSmoothing.TextChanged += new System.EventHandler(this._textBoxSmoothing_TextChanged);
            this._textBoxSmoothing.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(278, 633);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Filter Size";
            // 
            // _buttonHistogramEqualization
            // 
            this._buttonHistogramEqualization.Enabled = false;
            this._buttonHistogramEqualization.Location = new System.Drawing.Point(35, 671);
            this._buttonHistogramEqualization.Name = "_buttonHistogramEqualization";
            this._buttonHistogramEqualization.Size = new System.Drawing.Size(154, 23);
            this._buttonHistogramEqualization.TabIndex = 14;
            this._buttonHistogramEqualization.Text = "Apply Histogram Equalization";
            this._buttonHistogramEqualization.UseVisualStyleBackColor = true;
            this._buttonHistogramEqualization.Click += new System.EventHandler(this._buttonHistogramEqualization_Click);
            // 
            // _buttonSobel
            // 
            this._buttonSobel.Enabled = false;
            this._buttonSobel.Location = new System.Drawing.Point(214, 528);
            this._buttonSobel.Name = "_buttonSobel";
            this._buttonSobel.Size = new System.Drawing.Size(149, 23);
            this._buttonSobel.TabIndex = 15;
            this._buttonSobel.Text = "Apply Sobel Edge Detection";
            this._buttonSobel.UseVisualStyleBackColor = true;
            this._buttonSobel.Click += new System.EventHandler(this._buttonSobel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._radioButtonMeanSmoothing);
            this.groupBox1.Controls.Add(this._radioButtonMedianSmoothing);
            this.groupBox1.Location = new System.Drawing.Point(152, 611);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(120, 54);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Method";
            // 
            // _buttonOverlap
            // 
            this._buttonOverlap.Enabled = false;
            this._buttonOverlap.Location = new System.Drawing.Point(369, 528);
            this._buttonOverlap.Name = "_buttonOverlap";
            this._buttonOverlap.Size = new System.Drawing.Size(184, 23);
            this._buttonOverlap.TabIndex = 16;
            this._buttonOverlap.Text = "Overlap Edge On The Origin Image";
            this._buttonOverlap.UseVisualStyleBackColor = true;
            this._buttonOverlap.Click += new System.EventHandler(this._buttonOverlap_Click);
            // 
            // _trackBarStretchingHorizontalScale
            // 
            this._trackBarStretchingHorizontalScale.Enabled = false;
            this._trackBarStretchingHorizontalScale.Location = new System.Drawing.Point(690, 554);
            this._trackBarStretchingHorizontalScale.Maximum = 200;
            this._trackBarStretchingHorizontalScale.Minimum = 1;
            this._trackBarStretchingHorizontalScale.Name = "_trackBarStretchingHorizontalScale";
            this._trackBarStretchingHorizontalScale.Size = new System.Drawing.Size(104, 45);
            this._trackBarStretchingHorizontalScale.TabIndex = 17;
            this._trackBarStretchingHorizontalScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this._trackBarStretchingHorizontalScale.Value = 100;
            this._trackBarStretchingHorizontalScale.ValueChanged += new System.EventHandler(this._trackBarStretching_ValueChange);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(688, 533);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "Stretching Value of Horizontal Direction";
            // 
            // _textBoxStretchingHorizontalScale
            // 
            this._textBoxStretchingHorizontalScale.Enabled = false;
            this._textBoxStretchingHorizontalScale.Location = new System.Drawing.Point(801, 554);
            this._textBoxStretchingHorizontalScale.Name = "_textBoxStretchingHorizontalScale";
            this._textBoxStretchingHorizontalScale.Size = new System.Drawing.Size(29, 22);
            this._textBoxStretchingHorizontalScale.TabIndex = 19;
            this._textBoxStretchingHorizontalScale.Text = "1";
            this._textBoxStretchingHorizontalScale.TextChanged += new System.EventHandler(this._textBoxStretchingHorizontalScale_TextChanged);
            this._textBoxStretchingHorizontalScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBox_double_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(690, 592);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "Stretching Value of Vertical Direction";
            // 
            // _trackBarStretchingVerticalScale
            // 
            this._trackBarStretchingVerticalScale.Enabled = false;
            this._trackBarStretchingVerticalScale.Location = new System.Drawing.Point(692, 620);
            this._trackBarStretchingVerticalScale.Maximum = 200;
            this._trackBarStretchingVerticalScale.Minimum = 1;
            this._trackBarStretchingVerticalScale.Name = "_trackBarStretchingVerticalScale";
            this._trackBarStretchingVerticalScale.Size = new System.Drawing.Size(104, 45);
            this._trackBarStretchingVerticalScale.TabIndex = 21;
            this._trackBarStretchingVerticalScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this._trackBarStretchingVerticalScale.Value = 100;
            this._trackBarStretchingVerticalScale.ValueChanged += new System.EventHandler(this._trackBarStretching_ValueChange);
            // 
            // _textBoxStretchingVerticalScale
            // 
            this._textBoxStretchingVerticalScale.Enabled = false;
            this._textBoxStretchingVerticalScale.Location = new System.Drawing.Point(803, 620);
            this._textBoxStretchingVerticalScale.Name = "_textBoxStretchingVerticalScale";
            this._textBoxStretchingVerticalScale.Size = new System.Drawing.Size(27, 22);
            this._textBoxStretchingVerticalScale.TabIndex = 22;
            this._textBoxStretchingVerticalScale.Text = "1";
            this._textBoxStretchingVerticalScale.TextChanged += new System.EventHandler(this._textBoxStretchingVerticalScale_TextChanged);
            this._textBoxStretchingVerticalScale.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBox_double_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(921, 533);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 23;
            this.label4.Text = "Rotational Value";
            // 
            // _trackBarRotation
            // 
            this._trackBarRotation.Enabled = false;
            this._trackBarRotation.Location = new System.Drawing.Point(923, 554);
            this._trackBarRotation.Maximum = 360;
            this._trackBarRotation.Name = "_trackBarRotation";
            this._trackBarRotation.Size = new System.Drawing.Size(104, 45);
            this._trackBarRotation.TabIndex = 24;
            this._trackBarRotation.TickStyle = System.Windows.Forms.TickStyle.None;
            this._trackBarRotation.ValueChanged += new System.EventHandler(this._trackBarRotation_ValueChange);
            // 
            // _textBoxRotation
            // 
            this._textBoxRotation.Enabled = false;
            this._textBoxRotation.Location = new System.Drawing.Point(1034, 554);
            this._textBoxRotation.Name = "_textBoxRotation";
            this._textBoxRotation.Size = new System.Drawing.Size(30, 22);
            this._textBoxRotation.TabIndex = 25;
            this._textBoxRotation.Text = "0";
            this._textBoxRotation.TextChanged += new System.EventHandler(this._textBoxTrtation_TextChanged);
            this._textBoxRotation.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._textBox_KeyPress);
            // 
            // _buttonDeleteFinalResult
            // 
            this._buttonDeleteFinalResult.Enabled = false;
            this._buttonDeleteFinalResult.Location = new System.Drawing.Point(1212, 434);
            this._buttonDeleteFinalResult.Name = "_buttonDeleteFinalResult";
            this._buttonDeleteFinalResult.Size = new System.Drawing.Size(120, 23);
            this._buttonDeleteFinalResult.TabIndex = 26;
            this._buttonDeleteFinalResult.Text = "Delete the Final Result";
            this._buttonDeleteFinalResult.UseVisualStyleBackColor = true;
            this._buttonDeleteFinalResult.Click += new System.EventHandler(this._buttonDeleteFinalResult_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1374, 698);
            this.Controls.Add(this._buttonDeleteFinalResult);
            this.Controls.Add(this._textBoxRotation);
            this.Controls.Add(this._trackBarRotation);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._textBoxStretchingVerticalScale);
            this.Controls.Add(this._trackBarStretchingVerticalScale);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._textBoxStretchingHorizontalScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._trackBarStretchingHorizontalScale);
            this.Controls.Add(this._buttonOverlap);
            this.Controls.Add(this._buttonSobel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._buttonHistogramEqualization);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBoxSmoothing);
            this.Controls.Add(this._checkBoxSmoothing);
            this.Controls.Add(this._textBoxThresholding);
            this.Controls.Add(this._trackBarThresholding);
            this.Controls.Add(this._checkBoxThresholding);
            this.Controls.Add(this._buttonGrayLevel);
            this.Controls.Add(this._buttonRGBExtraction);
            this.Controls.Add(this._listBoxResult);
            this.Controls.Add(this._pictureBoxResult);
            this.Controls.Add(this._listBoxInput);
            this.Controls.Add(this._pictureBoxInput);
            this.Controls.Add(this._buttonLoadImage);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "MainForm";
            this.Text = "Main menu";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarThresholding)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarStretchingHorizontalScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarStretchingVerticalScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._trackBarRotation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenFileDialog _openFile;
        private List<Bitmap> _inputImages;
        private List<Bitmap> _resultImages;
        private System.Windows.Forms.Button _buttonLoadImage;
        private System.Windows.Forms.PictureBox _pictureBoxInput;
        private System.Windows.Forms.ListBox _listBoxInput;
        private System.Windows.Forms.Button _buttonRGBExtraction;
        private System.Windows.Forms.PictureBox _pictureBoxResult;
        private System.Windows.Forms.ListBox _listBoxResult;
        private System.Windows.Forms.Button _buttonGrayLevel;
        private CheckBox _checkBoxThresholding;
        private TrackBar _trackBarThresholding;
        private TextBox _textBoxThresholding;
        private CheckBox _checkBoxSmoothing;
        private RadioButton _radioButtonMeanSmoothing;
        private RadioButton _radioButtonMedianSmoothing;
        private RadioButtonSmoothing _smoothingMethod;
        private TextBox _textBoxSmoothing;
        private Label label1;
        private Button _buttonHistogramEqualization;
        private Button _buttonSobel;
        private GroupBox groupBox1;
        private Button _buttonOverlap;
        private TrackBar _trackBarStretchingHorizontalScale;
        private Label label2;
        private TextBox _textBoxStretchingHorizontalScale;
        private Label label3;
        private TrackBar _trackBarStretchingVerticalScale;
        private TextBox _textBoxStretchingVerticalScale;
        private Label label4;
        private TrackBar _trackBarRotation;
        private TextBox _textBoxRotation;
        private Button _buttonDeleteFinalResult;
    }
}

