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
            label1 = new Label();
            ProfileNameBox = new TextBox();
            SaveProfileButton = new Button();
            ModFolderBox = new TextBox();
            ModFolderLabel = new Label();
            CancelButton = new Button();
            SelectModFolder = new Button();
            CopyModFolder = new Button();
            CopyVanillaFolder = new Button();
            SelectVanillaFolder = new Button();
            VanillaFolderBox = new TextBox();
            VanillaFolderLabel = new Label();
            CopyLocalisationFolder = new Button();
            SelectLocalisationFolder = new Button();
            LocalisationFolderBox = new TextBox();
            LocalisationFolderLabel = new Label();
            DebugInfo = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 15);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(116, 15);
            label1.TabIndex = 0;
            label1.Text = "Create Profile Name:";
            // 
            // ProfileNameBox
            // 
            ProfileNameBox.Location = new Point(144, 12);
            ProfileNameBox.Margin = new Padding(4, 3, 4, 3);
            ProfileNameBox.Name = "ProfileNameBox";
            ProfileNameBox.Size = new Size(349, 23);
            ProfileNameBox.TabIndex = 1;
            ProfileNameBox.Text = "Mod 1";
            ProfileNameBox.TextChanged += ProfileNameBox_TextChanged;
            ProfileNameBox.Enter += ProfileNameBox_Enter;
            // 
            // SaveProfileButton
            // 
            SaveProfileButton.Location = new Point(816, 149);
            SaveProfileButton.Margin = new Padding(4, 3, 4, 3);
            SaveProfileButton.Name = "SaveProfileButton";
            SaveProfileButton.Size = new Size(85, 27);
            SaveProfileButton.TabIndex = 2;
            SaveProfileButton.Text = "Save Profile";
            SaveProfileButton.UseVisualStyleBackColor = true;
            SaveProfileButton.Click += SaveFolderProfile_Click;
            // 
            // ModFolderBox
            // 
            ModFolderBox.Location = new Point(144, 85);
            ModFolderBox.Margin = new Padding(4, 3, 4, 3);
            ModFolderBox.Name = "ModFolderBox";
            ModFolderBox.Size = new Size(537, 23);
            ModFolderBox.TabIndex = 4;
            ModFolderBox.Text = "Enter Mod Folder ";
            ModFolderBox.TextChanged += ModFolderBox_TextChanged;
            ModFolderBox.Enter += ModFolderBox_Enter;
            // 
            // ModFolderLabel
            // 
            ModFolderLabel.AutoSize = true;
            ModFolderLabel.Location = new Point(15, 88);
            ModFolderLabel.Margin = new Padding(4, 0, 4, 0);
            ModFolderLabel.Name = "ModFolderLabel";
            ModFolderLabel.Size = new Size(71, 15);
            ModFolderLabel.TabIndex = 3;
            ModFolderLabel.Text = "Mod Folder:";
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(723, 149);
            CancelButton.Margin = new Padding(4, 3, 4, 3);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(85, 27);
            CancelButton.TabIndex = 6;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // SelectModFolder
            // 
            SelectModFolder.Location = new Point(688, 82);
            SelectModFolder.Margin = new Padding(4, 3, 4, 3);
            SelectModFolder.Name = "SelectModFolder";
            SelectModFolder.Size = new Size(93, 27);
            SelectModFolder.TabIndex = 7;
            SelectModFolder.Text = "Select Folder";
            SelectModFolder.UseVisualStyleBackColor = true;
            SelectModFolder.Click += SelectModFolder_Click;
            // 
            // CopyModFolder
            // 
            CopyModFolder.Location = new Point(789, 82);
            CopyModFolder.Margin = new Padding(4, 3, 4, 3);
            CopyModFolder.Name = "CopyModFolder";
            CopyModFolder.Size = new Size(112, 27);
            CopyModFolder.TabIndex = 8;
            CopyModFolder.Text = "Current Folder";
            CopyModFolder.UseVisualStyleBackColor = true;
            CopyModFolder.Click += CopyModFolder_Click;
            // 
            // CopyVanillaFolder
            // 
            CopyVanillaFolder.Location = new Point(789, 49);
            CopyVanillaFolder.Margin = new Padding(4, 3, 4, 3);
            CopyVanillaFolder.Name = "CopyVanillaFolder";
            CopyVanillaFolder.Size = new Size(112, 27);
            CopyVanillaFolder.TabIndex = 12;
            CopyVanillaFolder.Text = "Current Folder";
            CopyVanillaFolder.UseVisualStyleBackColor = true;
            CopyVanillaFolder.Click += CopyVanillaFolder_Click;
            // 
            // SelectVanillaFolder
            // 
            SelectVanillaFolder.Location = new Point(688, 49);
            SelectVanillaFolder.Margin = new Padding(4, 3, 4, 3);
            SelectVanillaFolder.Name = "SelectVanillaFolder";
            SelectVanillaFolder.Size = new Size(93, 27);
            SelectVanillaFolder.TabIndex = 11;
            SelectVanillaFolder.Text = "Select Folder";
            SelectVanillaFolder.UseVisualStyleBackColor = true;
            SelectVanillaFolder.Click += SelectVanillaFolder_Click;
            // 
            // VanillaFolderBox
            // 
            VanillaFolderBox.Location = new Point(144, 52);
            VanillaFolderBox.Margin = new Padding(4, 3, 4, 3);
            VanillaFolderBox.Name = "VanillaFolderBox";
            VanillaFolderBox.Size = new Size(537, 23);
            VanillaFolderBox.TabIndex = 10;
            VanillaFolderBox.Text = "Enter Vanilla Folder";
            VanillaFolderBox.TextChanged += VanillaFolderBox_TextChanged;
            VanillaFolderBox.Enter += VanillaFolderBox_Enter;
            // 
            // VanillaFolderLabel
            // 
            VanillaFolderLabel.AutoSize = true;
            VanillaFolderLabel.Location = new Point(15, 55);
            VanillaFolderLabel.Margin = new Padding(4, 0, 4, 0);
            VanillaFolderLabel.Name = "VanillaFolderLabel";
            VanillaFolderLabel.Size = new Size(80, 15);
            VanillaFolderLabel.TabIndex = 9;
            VanillaFolderLabel.Text = "Vanilla Folder:";
            // 
            // CopyLocalisationFolder
            // 
            CopyLocalisationFolder.Location = new Point(789, 115);
            CopyLocalisationFolder.Margin = new Padding(4, 3, 4, 3);
            CopyLocalisationFolder.Name = "CopyLocalisationFolder";
            CopyLocalisationFolder.Size = new Size(112, 27);
            CopyLocalisationFolder.TabIndex = 16;
            CopyLocalisationFolder.Text = "Current Folder";
            CopyLocalisationFolder.UseVisualStyleBackColor = true;
            CopyLocalisationFolder.Click += CopyLocalizationFolder_Click;
            // 
            // SelectLocalisationFolder
            // 
            SelectLocalisationFolder.Location = new Point(688, 115);
            SelectLocalisationFolder.Margin = new Padding(4, 3, 4, 3);
            SelectLocalisationFolder.Name = "SelectLocalisationFolder";
            SelectLocalisationFolder.Size = new Size(93, 27);
            SelectLocalisationFolder.TabIndex = 15;
            SelectLocalisationFolder.Text = "Select Folder";
            SelectLocalisationFolder.UseVisualStyleBackColor = true;
            SelectLocalisationFolder.Click += SelectLocalizationFolder_Click;
            // 
            // LocalisationFolderBox
            // 
            LocalisationFolderBox.Location = new Point(144, 118);
            LocalisationFolderBox.Margin = new Padding(4, 3, 4, 3);
            LocalisationFolderBox.Name = "LocalisationFolderBox";
            LocalisationFolderBox.Size = new Size(537, 23);
            LocalisationFolderBox.TabIndex = 14;
            LocalisationFolderBox.Text = "Enter Localisation Folder";
            LocalisationFolderBox.TextChanged += LocalisationFolderBox_TextChanged;
            LocalisationFolderBox.Enter += LocalizationFolderBox_Enter;
            // 
            // LocalisationFolderLabel
            // 
            LocalisationFolderLabel.AutoSize = true;
            LocalisationFolderLabel.Location = new Point(15, 121);
            LocalisationFolderLabel.Margin = new Padding(4, 0, 4, 0);
            LocalisationFolderLabel.Name = "LocalisationFolderLabel";
            LocalisationFolderLabel.Size = new Size(109, 15);
            LocalisationFolderLabel.TabIndex = 13;
            LocalisationFolderLabel.Text = "Localisation Folder:";
            // 
            // DebugInfo
            // 
            DebugInfo.AutoSize = true;
            DebugInfo.Location = new Point(140, 155);
            DebugInfo.Margin = new Padding(4, 0, 4, 0);
            DebugInfo.Name = "DebugInfo";
            DebugInfo.Size = new Size(106, 15);
            DebugInfo.TabIndex = 17;
            DebugInfo.Text = "Please enter values";
            // 
            // CreateProfileForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 186);
            Controls.Add(DebugInfo);
            Controls.Add(CopyLocalisationFolder);
            Controls.Add(SelectLocalisationFolder);
            Controls.Add(LocalisationFolderBox);
            Controls.Add(LocalisationFolderLabel);
            Controls.Add(CopyVanillaFolder);
            Controls.Add(SelectVanillaFolder);
            Controls.Add(VanillaFolderBox);
            Controls.Add(VanillaFolderLabel);
            Controls.Add(CopyModFolder);
            Controls.Add(SelectModFolder);
            Controls.Add(CancelButton);
            Controls.Add(ModFolderBox);
            Controls.Add(ModFolderLabel);
            Controls.Add(SaveProfileButton);
            Controls.Add(ProfileNameBox);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateProfileForm";
            Text = "Create Profile";
            FormClosed += CreateProfileForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox ProfileNameBox;
        private Button SaveProfileButton;
        private TextBox ModFolderBox;
        private Label ModFolderLabel;
        private new Button CancelButton;
        private Button SelectModFolder;
        private Button CopyModFolder;
        private Button CopyVanillaFolder;
        private Button SelectVanillaFolder;
        private TextBox VanillaFolderBox;
        private Label VanillaFolderLabel;
        private Button CopyLocalisationFolder;
        private Button SelectLocalisationFolder;
        private TextBox LocalisationFolderBox;
        private Label LocalisationFolderLabel;
        private Label DebugInfo;
    }
}