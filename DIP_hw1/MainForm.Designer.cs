using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace DIP_hw1
{
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
            this._bnLoadImage = new System.Windows.Forms.Button();
            this._pbInputImage = new System.Windows.Forms.PictureBox();
            this._lbInputImage = new System.Windows.Forms.ListBox();
            this._bnRGBExtraction = new System.Windows.Forms.Button();
            this._pbResult = new System.Windows.Forms.PictureBox();
            this._lbResult = new System.Windows.Forms.ListBox();
            this._bnGrayLevel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pbInputImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._pbResult)).BeginInit();
            this.SuspendLayout();
            // 
            // _bnLoadImage
            // 
            this._bnLoadImage.Location = new System.Drawing.Point(35, 434);
            this._bnLoadImage.Name = "_bnLoadImage";
            this._bnLoadImage.Size = new System.Drawing.Size(75, 23);
            this._bnLoadImage.TabIndex = 0;
            this._bnLoadImage.Text = "Load Image";
            this._bnLoadImage.UseVisualStyleBackColor = true;
            this._bnLoadImage.Click += new System.EventHandler(this._bnLoadImage_Click);
            // 
            // _pbInputImage
            // 
            this._pbInputImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pbInputImage.Location = new System.Drawing.Point(150, 10);
            this._pbInputImage.Name = "_pbInputImage";
            this._pbInputImage.Size = new System.Drawing.Size(512, 512);
            this._pbInputImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._pbInputImage.TabIndex = 1;
            this._pbInputImage.TabStop = false;
            // 
            // _lbInputImage
            // 
            this._lbInputImage.FormattingEnabled = true;
            this._lbInputImage.HorizontalScrollbar = true;
            this._lbInputImage.ItemHeight = 12;
            this._lbInputImage.Location = new System.Drawing.Point(10, 10);
            this._lbInputImage.Name = "_lbInputImage";
            this._lbInputImage.Size = new System.Drawing.Size(120, 400);
            this._lbInputImage.TabIndex = 2;
            this._lbInputImage.SelectedIndexChanged += new System.EventHandler(this._lbInputImage_SelectedIndexChanged);
            // 
            // _bnRGBExtraction
            // 
            this._bnRGBExtraction.Location = new System.Drawing.Point(35, 477);
            this._bnRGBExtraction.Name = "_bnRGBExtraction";
            this._bnRGBExtraction.Size = new System.Drawing.Size(95, 23);
            this._bnRGBExtraction.TabIndex = 3;
            this._bnRGBExtraction.Text = "RGB Extraction";
            this._bnRGBExtraction.UseVisualStyleBackColor = true;
            this._bnRGBExtraction.Click += new System.EventHandler(this._bnRGBExtraction_Click);
            // 
            // _pbResult
            // 
            this._pbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._pbResult.Location = new System.Drawing.Point(680, 10);
            this._pbResult.Name = "_pbResult";
            this._pbResult.Size = new System.Drawing.Size(512, 512);
            this._pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this._pbResult.TabIndex = 3;
            this._pbResult.TabStop = false;
            // 
            // _lbResult
            // 
            this._lbResult.FormattingEnabled = true;
            this._lbResult.ItemHeight = 12;
            this._lbResult.Location = new System.Drawing.Point(1212, 10);
            this._lbResult.Name = "_lbResult";
            this._lbResult.Size = new System.Drawing.Size(120, 400);
            this._lbResult.TabIndex = 4;
            this._lbResult.SelectedIndexChanged += new System.EventHandler(this._lbResult_SelectedIndexChanged);
            // 
            // _bnGrayLevel
            // 
            this._bnGrayLevel.Location = new System.Drawing.Point(35, 528);
            this._bnGrayLevel.Name = "_bnGrayLevel";
            this._bnGrayLevel.Size = new System.Drawing.Size(154, 23);
            this._bnGrayLevel.TabIndex = 5;
            this._bnGrayLevel.Text = "Translate to Gray Level Image";
            this._bnGrayLevel.UseVisualStyleBackColor = true;
            this._bnGrayLevel.Click += new System.EventHandler(this._bnGrayLevel_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1374, 698);
            this.Controls.Add(this._bnGrayLevel);
            this.Controls.Add(this._bnRGBExtraction);
            this.Controls.Add(this._lbResult);
            this.Controls.Add(this._pbResult);
            this.Controls.Add(this._lbInputImage);
            this.Controls.Add(this._pbInputImage);
            this.Controls.Add(this._bnLoadImage);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "MainForm";
            this.Text = "Main menu";
            ((System.ComponentModel.ISupportInitialize)(this._pbInputImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._pbResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private OpenFileDialog _openFile;
        private List<Bitmap> _inputImages;
        private List<Bitmap> _resultImages;
        private System.Windows.Forms.Button _bnLoadImage;
        private System.Windows.Forms.PictureBox _pbInputImage;
        private System.Windows.Forms.ListBox _lbInputImage;
        private System.Windows.Forms.Button _bnRGBExtraction;
        private System.Windows.Forms.PictureBox _pbResult;
        private System.Windows.Forms.ListBox _lbResult;
        private System.Windows.Forms.Button _bnGrayLevel;
    }
}

