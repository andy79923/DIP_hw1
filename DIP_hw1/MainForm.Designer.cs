﻿using System.Collections.Generic;
using System.Drawing;
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
            this.SuspendLayout();
            // 
            // _bnLoadImage
            // 
            this._bnLoadImage.Location = new System.Drawing.Point(104, 196);
            this._bnLoadImage.Name = "_bnLoadImage";
            this._bnLoadImage.Size = new System.Drawing.Size(75, 23);
            this._bnLoadImage.TabIndex = 0;
            this._bnLoadImage.Text = "Load Image";
            this._bnLoadImage.UseVisualStyleBackColor = true;
            this._bnLoadImage.Click += new System.EventHandler(this._bnLoadImage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this._bnLoadImage);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "MainForm";
            this.Text = "Main menu";
            this.ResumeLayout(false);

        }

        #endregion

        private List<Bitmap> _inputImages; 
        private System.Windows.Forms.Button _bnLoadImage;
    }
}

