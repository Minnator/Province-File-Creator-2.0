namespace EU4_Province_Creator
{
    partial class ProvinceColorPicker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProvinceColorPicker));
            this.NewRBGButton = new System.Windows.Forms.Button();
            this.ColorEx = new System.Windows.Forms.Button();
            this.bVal = new System.Windows.Forms.TextBox();
            this.B_Value = new System.Windows.Forms.Label();
            this.gVal = new System.Windows.Forms.TextBox();
            this.G_Value = new System.Windows.Forms.Label();
            this.rVal = new System.Windows.Forms.TextBox();
            this.R_Value = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.FunnyPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.FunnyPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // NewRBGButton
            // 
            this.NewRBGButton.Location = new System.Drawing.Point(12, 12);
            this.NewRBGButton.Name = "NewRBGButton";
            this.NewRBGButton.Size = new System.Drawing.Size(105, 25);
            this.NewRBGButton.TabIndex = 100;
            this.NewRBGButton.Text = "New Rgb Value";
            this.NewRBGButton.UseVisualStyleBackColor = true;
            this.NewRBGButton.Click += new System.EventHandler(this.NewRBGButton_Click);
            // 
            // ColorEx
            // 
            this.ColorEx.Enabled = false;
            this.ColorEx.Location = new System.Drawing.Point(123, 12);
            this.ColorEx.Name = "ColorEx";
            this.ColorEx.Size = new System.Drawing.Size(105, 25);
            this.ColorEx.TabIndex = 107;
            this.ColorEx.UseVisualStyleBackColor = true;
            // 
            // bVal
            // 
            this.bVal.Location = new System.Drawing.Point(184, 43);
            this.bVal.Name = "bVal";
            this.bVal.ReadOnly = true;
            this.bVal.Size = new System.Drawing.Size(40, 20);
            this.bVal.TabIndex = 106;
            this.bVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // B_Value
            // 
            this.B_Value.AutoSize = true;
            this.B_Value.Location = new System.Drawing.Point(164, 46);
            this.B_Value.Name = "B_Value";
            this.B_Value.Size = new System.Drawing.Size(14, 13);
            this.B_Value.TabIndex = 105;
            this.B_Value.Text = "B";
            // 
            // gVal
            // 
            this.gVal.Location = new System.Drawing.Point(111, 43);
            this.gVal.Name = "gVal";
            this.gVal.ReadOnly = true;
            this.gVal.Size = new System.Drawing.Size(40, 20);
            this.gVal.TabIndex = 104;
            this.gVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // G_Value
            // 
            this.G_Value.AutoSize = true;
            this.G_Value.Location = new System.Drawing.Point(89, 46);
            this.G_Value.Name = "G_Value";
            this.G_Value.Size = new System.Drawing.Size(15, 13);
            this.G_Value.TabIndex = 103;
            this.G_Value.Text = "G";
            // 
            // rVal
            // 
            this.rVal.Location = new System.Drawing.Point(32, 43);
            this.rVal.Name = "rVal";
            this.rVal.ReadOnly = true;
            this.rVal.Size = new System.Drawing.Size(40, 20);
            this.rVal.TabIndex = 102;
            this.rVal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // R_Value
            // 
            this.R_Value.AutoSize = true;
            this.R_Value.Location = new System.Drawing.Point(12, 46);
            this.R_Value.Name = "R_Value";
            this.R_Value.Size = new System.Drawing.Size(15, 13);
            this.R_Value.TabIndex = 101;
            this.R_Value.Text = "R";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(152, 70);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 108;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // FunnyPicture
            // 
            this.FunnyPicture.Image = ((System.Drawing.Image)(resources.GetObject("FunnyPicture.Image")));
            this.FunnyPicture.Location = new System.Drawing.Point(15, 71);
            this.FunnyPicture.Name = "FunnyPicture";
            this.FunnyPicture.Size = new System.Drawing.Size(25, 25);
            this.FunnyPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FunnyPicture.TabIndex = 109;
            this.FunnyPicture.TabStop = false;
            // 
            // ProvinceColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(237, 102);
            this.Controls.Add(this.FunnyPicture);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NewRBGButton);
            this.Controls.Add(this.ColorEx);
            this.Controls.Add(this.bVal);
            this.Controls.Add(this.R_Value);
            this.Controls.Add(this.B_Value);
            this.Controls.Add(this.rVal);
            this.Controls.Add(this.gVal);
            this.Controls.Add(this.G_Value);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProvinceColorPicker";
            this.Text = "Province Color Picker";
            ((System.ComponentModel.ISupportInitialize)(this.FunnyPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NewRBGButton;
        private System.Windows.Forms.Button ColorEx;
        private System.Windows.Forms.TextBox bVal;
        private System.Windows.Forms.Label B_Value;
        private System.Windows.Forms.TextBox gVal;
        private System.Windows.Forms.Label G_Value;
        private System.Windows.Forms.TextBox rVal;
        private System.Windows.Forms.Label R_Value;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox FunnyPicture;
    }
}