namespace EU4_Province_Creator
{
    partial class RandomEffect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RandomEffect));
            this.EffectBox = new System.Windows.Forms.TextBox();
            this.GetModifierButton = new System.Windows.Forms.Button();
            this.CopyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EffectBox
            // 
            this.EffectBox.Location = new System.Drawing.Point(12, 12);
            this.EffectBox.Name = "EffectBox";
            this.EffectBox.ReadOnly = true;
            this.EffectBox.Size = new System.Drawing.Size(305, 20);
            this.EffectBox.TabIndex = 6;
            this.EffectBox.Enter += new System.EventHandler(this.EffectBox_Enter);
            // 
            // GetModifierButton
            // 
            this.GetModifierButton.Location = new System.Drawing.Point(11, 38);
            this.GetModifierButton.Name = "GetModifierButton";
            this.GetModifierButton.Size = new System.Drawing.Size(200, 23);
            this.GetModifierButton.TabIndex = 4;
            this.GetModifierButton.Text = "Random Effect";
            this.GetModifierButton.UseVisualStyleBackColor = true;
            this.GetModifierButton.Click += new System.EventHandler(this.GetModifierButton_Click);
            // 
            // CopyButton
            // 
            this.CopyButton.Location = new System.Drawing.Point(217, 38);
            this.CopyButton.Name = "CopyButton";
            this.CopyButton.Size = new System.Drawing.Size(100, 23);
            this.CopyButton.TabIndex = 7;
            this.CopyButton.Text = "Copy";
            this.CopyButton.UseVisualStyleBackColor = true;
            this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // RandomEffect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 72);
            this.Controls.Add(this.CopyButton);
            this.Controls.Add(this.EffectBox);
            this.Controls.Add(this.GetModifierButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RandomEffect";
            this.Text = "Random Effect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox EffectBox;
        private System.Windows.Forms.Button GetModifierButton;
        private System.Windows.Forms.Button CopyButton;
    }
}