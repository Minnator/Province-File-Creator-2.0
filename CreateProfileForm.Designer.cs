namespace EU4_Province_Creator
{
    partial class CreateProfileForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateProfileForm));
            this.label1 = new System.Windows.Forms.Label();
            this.ProfileNameBox = new System.Windows.Forms.TextBox();
            this.SaveProfileButton = new System.Windows.Forms.Button();
            this.ModFolderBox = new System.Windows.Forms.TextBox();
            this.ModFolderLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SelectModFolder = new System.Windows.Forms.Button();
            this.CopyModFolder = new System.Windows.Forms.Button();
            this.CopyVanillaFolder = new System.Windows.Forms.Button();
            this.SelectVanillaFolder = new System.Windows.Forms.Button();
            this.VanillaFolderBox = new System.Windows.Forms.TextBox();
            this.VanillaFolderLabel = new System.Windows.Forms.Label();
            this.CopyLocalisationFolder = new System.Windows.Forms.Button();
            this.SelectLocalisationFolder = new System.Windows.Forms.Button();
            this.LocalisationFolderBox = new System.Windows.Forms.TextBox();
            this.LocalisationFolderLabel = new System.Windows.Forms.Label();
            this.DebugInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create Profile Name:";
            // 
            // ProfileNameBox
            // 
            this.ProfileNameBox.Location = new System.Drawing.Point(123, 10);
            this.ProfileNameBox.Name = "ProfileNameBox";
            this.ProfileNameBox.Size = new System.Drawing.Size(300, 20);
            this.ProfileNameBox.TabIndex = 1;
            this.ProfileNameBox.Text = "Mod 1";
            this.ProfileNameBox.TextChanged += new System.EventHandler(this.ProfileNameBox_TextChanged);
            this.ProfileNameBox.Enter += new System.EventHandler(this.ProfileNameBox_Enter);
            // 
            // SaveProfileButton
            // 
            this.SaveProfileButton.Location = new System.Drawing.Point(699, 129);
            this.SaveProfileButton.Name = "SaveProfileButton";
            this.SaveProfileButton.Size = new System.Drawing.Size(73, 23);
            this.SaveProfileButton.TabIndex = 2;
            this.SaveProfileButton.Text = "Save Profile";
            this.SaveProfileButton.UseVisualStyleBackColor = true;
            this.SaveProfileButton.Click += new System.EventHandler(this.SaveFolderProfile_Click);
            // 
            // ModFolderBox
            // 
            this.ModFolderBox.Location = new System.Drawing.Point(123, 50);
            this.ModFolderBox.Name = "ModFolderBox";
            this.ModFolderBox.Size = new System.Drawing.Size(461, 20);
            this.ModFolderBox.TabIndex = 4;
            this.ModFolderBox.Text = "Enter Mod Folder ";
            this.ModFolderBox.Enter += new System.EventHandler(this.ModFolderBox_Enter);
            // 
            // ModFolderLabel
            // 
            this.ModFolderLabel.AutoSize = true;
            this.ModFolderLabel.Location = new System.Drawing.Point(13, 53);
            this.ModFolderLabel.Name = "ModFolderLabel";
            this.ModFolderLabel.Size = new System.Drawing.Size(63, 13);
            this.ModFolderLabel.TabIndex = 3;
            this.ModFolderLabel.Text = "Mod Folder:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(620, 129);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(73, 23);
            this.CancelButton.TabIndex = 6;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SelectModFolder
            // 
            this.SelectModFolder.Location = new System.Drawing.Point(590, 48);
            this.SelectModFolder.Name = "SelectModFolder";
            this.SelectModFolder.Size = new System.Drawing.Size(80, 23);
            this.SelectModFolder.TabIndex = 7;
            this.SelectModFolder.Text = "Select Folder";
            this.SelectModFolder.UseVisualStyleBackColor = true;
            this.SelectModFolder.Click += new System.EventHandler(this.SelectModFolder_Click);
            // 
            // CopyModFolder
            // 
            this.CopyModFolder.Location = new System.Drawing.Point(676, 48);
            this.CopyModFolder.Name = "CopyModFolder";
            this.CopyModFolder.Size = new System.Drawing.Size(96, 23);
            this.CopyModFolder.TabIndex = 8;
            this.CopyModFolder.Text = "" +
                                      "Current" +
                                      " Folder";
            this.CopyModFolder.UseVisualStyleBackColor = true;
            this.CopyModFolder.Click += new System.EventHandler(this.CopyModFolder_Click);
            // 
            // CopyVanillaFolder
            // 
            this.CopyVanillaFolder.Location = new System.Drawing.Point(676, 74);
            this.CopyVanillaFolder.Name = "CopyVanillaFolder";
            this.CopyVanillaFolder.Size = new System.Drawing.Size(96, 23);
            this.CopyVanillaFolder.TabIndex = 12;
            this.CopyVanillaFolder.Text = "Current Folder";
            this.CopyVanillaFolder.UseVisualStyleBackColor = true;
            this.CopyVanillaFolder.Click += new System.EventHandler(this.CopyVanillaFolder_Click);
            // 
            // SelectVanillaFolder
            // 
            this.SelectVanillaFolder.Location = new System.Drawing.Point(590, 74);
            this.SelectVanillaFolder.Name = "SelectVanillaFolder";
            this.SelectVanillaFolder.Size = new System.Drawing.Size(80, 23);
            this.SelectVanillaFolder.TabIndex = 11;
            this.SelectVanillaFolder.Text = "Select Folder";
            this.SelectVanillaFolder.UseVisualStyleBackColor = true;
            this.SelectVanillaFolder.Click += new System.EventHandler(this.SelectVanillaFolder_Click);
            // 
            // VanillaFolderBox
            // 
            this.VanillaFolderBox.Location = new System.Drawing.Point(123, 76);
            this.VanillaFolderBox.Name = "VanillaFolderBox";
            this.VanillaFolderBox.Size = new System.Drawing.Size(461, 20);
            this.VanillaFolderBox.TabIndex = 10;
            this.VanillaFolderBox.Text = "Enter Vanilla Folder";
            this.VanillaFolderBox.Enter += new System.EventHandler(this.VanillaFolderBox_Enter);
            // 
            // VanillaFolderLabel
            // 
            this.VanillaFolderLabel.AutoSize = true;
            this.VanillaFolderLabel.Location = new System.Drawing.Point(13, 79);
            this.VanillaFolderLabel.Name = "VanillaFolderLabel";
            this.VanillaFolderLabel.Size = new System.Drawing.Size(73, 13);
            this.VanillaFolderLabel.TabIndex = 9;
            this.VanillaFolderLabel.Text = "Vanilla Folder:";
            // 
            // CopyLocalisationFolder
            // 
            this.CopyLocalisationFolder.Location = new System.Drawing.Point(676, 100);
            this.CopyLocalisationFolder.Name = "CopyLocalisationFolder";
            this.CopyLocalisationFolder.Size = new System.Drawing.Size(96, 23);
            this.CopyLocalisationFolder.TabIndex = 16;
            this.CopyLocalisationFolder.Text = "Current Folder";
            this.CopyLocalisationFolder.UseVisualStyleBackColor = true;
            this.CopyLocalisationFolder.Click += new System.EventHandler(this.CopyLocalizationFolder_Click);
            // 
            // SelectLocalisationFolder
            // 
            this.SelectLocalisationFolder.Location = new System.Drawing.Point(590, 100);
            this.SelectLocalisationFolder.Name = "SelectLocalisationFolder";
            this.SelectLocalisationFolder.Size = new System.Drawing.Size(80, 23);
            this.SelectLocalisationFolder.TabIndex = 15;
            this.SelectLocalisationFolder.Text = "Select Folder";
            this.SelectLocalisationFolder.UseVisualStyleBackColor = true;
            this.SelectLocalisationFolder.Click += new System.EventHandler(this.SelectLocalizationFolder_Click);
            // 
            // LocalisationFolderBox
            // 
            this.LocalisationFolderBox.Location = new System.Drawing.Point(123, 102);
            this.LocalisationFolderBox.Name = "LocalisationFolderBox";
            this.LocalisationFolderBox.Size = new System.Drawing.Size(461, 20);
            this.LocalisationFolderBox.TabIndex = 14;
            this.LocalisationFolderBox.Text = "Enter Localisation Folder";
            this.LocalisationFolderBox.Enter += new System.EventHandler(this.LocalizationFolderBox_Enter);
            // 
            // LocalisationFolderLabel
            // 
            this.LocalisationFolderLabel.AutoSize = true;
            this.LocalisationFolderLabel.Location = new System.Drawing.Point(13, 105);
            this.LocalisationFolderLabel.Name = "LocalisationFolderLabel";
            this.LocalisationFolderLabel.Size = new System.Drawing.Size(98, 13);
            this.LocalisationFolderLabel.TabIndex = 13;
            this.LocalisationFolderLabel.Text = "Localisation Folder:";
            // 
            // DebugInfo
            // 
            this.DebugInfo.AutoSize = true;
            this.DebugInfo.Location = new System.Drawing.Point(120, 134);
            this.DebugInfo.Name = "DebugInfo";
            this.DebugInfo.Size = new System.Drawing.Size(100, 13);
            this.DebugInfo.TabIndex = 17;
            this.DebugInfo.Text = "Please enter values";
            // 
            // createProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 161);
            this.Controls.Add(this.DebugInfo);
            this.Controls.Add(this.CopyLocalisationFolder);
            this.Controls.Add(this.SelectLocalisationFolder);
            this.Controls.Add(this.LocalisationFolderBox);
            this.Controls.Add(this.LocalisationFolderLabel);
            this.Controls.Add(this.CopyVanillaFolder);
            this.Controls.Add(this.SelectVanillaFolder);
            this.Controls.Add(this.VanillaFolderBox);
            this.Controls.Add(this.VanillaFolderLabel);
            this.Controls.Add(this.CopyModFolder);
            this.Controls.Add(this.SelectModFolder);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ModFolderBox);
            this.Controls.Add(this.ModFolderLabel);
            this.Controls.Add(this.SaveProfileButton);
            this.Controls.Add(this.ProfileNameBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProfileForm";
            this.Text = "Create Profile";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateProfileForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ProfileNameBox;
        private System.Windows.Forms.Button SaveProfileButton;
        private System.Windows.Forms.TextBox ModFolderBox;
        private System.Windows.Forms.Label ModFolderLabel;
        private new System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button SelectModFolder;
        private System.Windows.Forms.Button CopyModFolder;
        private System.Windows.Forms.Button CopyVanillaFolder;
        private System.Windows.Forms.Button SelectVanillaFolder;
        private System.Windows.Forms.TextBox VanillaFolderBox;
        private System.Windows.Forms.Label VanillaFolderLabel;
        private System.Windows.Forms.Button CopyLocalisationFolder;
        private System.Windows.Forms.Button SelectLocalisationFolder;
        private System.Windows.Forms.TextBox LocalisationFolderBox;
        private System.Windows.Forms.Label LocalisationFolderLabel;
        private System.Windows.Forms.Label DebugInfo;
    }
}