namespace EU4_Province_Creator
{
    partial class TagVerifier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TagVerifier));
            this.TagInputBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuggestTag = new System.Windows.Forms.CheckBox();
            this.VerifyButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.NextTagButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TagInputBox
            // 
            this.TagInputBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TagInputBox.Location = new System.Drawing.Point(111, 6);
            this.TagInputBox.Name = "TagInputBox";
            this.TagInputBox.Size = new System.Drawing.Size(90, 20);
            this.TagInputBox.TabIndex = 0;
            this.TagInputBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TagInputBox.TextChanged += new System.EventHandler(this.TagInputBox_TextChanged);
            this.TagInputBox.Enter += new System.EventHandler(this.TagInputBox_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter tag:";
            // 
            // SuggestTag
            // 
            this.SuggestTag.AutoSize = true;
            this.SuggestTag.Location = new System.Drawing.Point(15, 32);
            this.SuggestTag.Name = "SuggestTag";
            this.SuggestTag.Size = new System.Drawing.Size(87, 17);
            this.SuggestTag.TabIndex = 2;
            this.SuggestTag.Text = "Suggest Tag";
            this.SuggestTag.UseVisualStyleBackColor = true;
            // 
            // VerifyButton
            // 
            this.VerifyButton.Location = new System.Drawing.Point(12, 55);
            this.VerifyButton.Name = "VerifyButton";
            this.VerifyButton.Size = new System.Drawing.Size(90, 23);
            this.VerifyButton.TabIndex = 3;
            this.VerifyButton.Text = "Verify";
            this.VerifyButton.UseVisualStyleBackColor = true;
            this.VerifyButton.Click += new System.EventHandler(this.VerifyButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(111, 55);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(90, 23);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.Close_Click);
            // 
            // NextTagButton
            // 
            this.NextTagButton.Location = new System.Drawing.Point(111, 28);
            this.NextTagButton.Name = "NextTagButton";
            this.NextTagButton.Size = new System.Drawing.Size(90, 23);
            this.NextTagButton.TabIndex = 5;
            this.NextTagButton.Text = "Next Tag";
            this.NextTagButton.UseVisualStyleBackColor = true;
            this.NextTagButton.Click += new System.EventHandler(this.NextTagButton_Click);
            // 
            // TagVerifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 86);
            this.Controls.Add(this.NextTagButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.VerifyButton);
            this.Controls.Add(this.SuggestTag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TagInputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TagVerifier";
            this.Text = "Tag Verifier";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TagVerifier_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TagInputBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox SuggestTag;
        private System.Windows.Forms.Button VerifyButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Button NextTagButton;
    }
}