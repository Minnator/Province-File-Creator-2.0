namespace EU4_Province_Creator
{
    partial class RandomModifier
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
            this.GetModifierButton = new System.Windows.Forms.Button();
            this.CategoriesBox = new System.Windows.Forms.ComboBox();
            this.ModifierBox = new System.Windows.Forms.TextBox();
            this.UseCategory = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // GetModifierButton
            // 
            this.GetModifierButton.Location = new System.Drawing.Point(11, 38);
            this.GetModifierButton.Name = "GetModifierButton";
            this.GetModifierButton.Size = new System.Drawing.Size(93, 23);
            this.GetModifierButton.TabIndex = 0;
            this.GetModifierButton.Text = "Get Modifier";
            this.GetModifierButton.UseVisualStyleBackColor = true;
            this.GetModifierButton.Click += new System.EventHandler(this.GetModifierButton_Click);
            // 
            // CategoriesBox
            // 
            this.CategoriesBox.AutoCompleteCustomSource.AddRange(new string[] {
            "boolean",
            "integer",
            "float",
            "unknown"});
            this.CategoriesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CategoriesBox.FormattingEnabled = true;
            this.CategoriesBox.Items.AddRange(new object[] {
            "boolean",
            "integer",
            "float",
            "unknown"});
            this.CategoriesBox.Location = new System.Drawing.Point(205, 38);
            this.CategoriesBox.Name = "CategoriesBox";
            this.CategoriesBox.Size = new System.Drawing.Size(112, 21);
            this.CategoriesBox.TabIndex = 1;
            // 
            // ModifierBox
            // 
            this.ModifierBox.Location = new System.Drawing.Point(12, 12);
            this.ModifierBox.Name = "ModifierBox";
            this.ModifierBox.Size = new System.Drawing.Size(305, 20);
            this.ModifierBox.TabIndex = 2;
            this.ModifierBox.Enter += new System.EventHandler(this.ModifierBox_Enter);
            // 
            // UseCategory
            // 
            this.UseCategory.AutoSize = true;
            this.UseCategory.Location = new System.Drawing.Point(110, 42);
            this.UseCategory.Name = "UseCategory";
            this.UseCategory.Size = new System.Drawing.Size(89, 17);
            this.UseCategory.TabIndex = 3;
            this.UseCategory.Text = "Use category";
            this.UseCategory.UseVisualStyleBackColor = true;
            // 
            // RandomModifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 72);
            this.Controls.Add(this.UseCategory);
            this.Controls.Add(this.ModifierBox);
            this.Controls.Add(this.CategoriesBox);
            this.Controls.Add(this.GetModifierButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RandomModifier";
            this.Text = "RandomModifier";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetModifierButton;
        private System.Windows.Forms.ComboBox CategoriesBox;
        private System.Windows.Forms.TextBox ModifierBox;
        private System.Windows.Forms.CheckBox UseCategory;
    }
}