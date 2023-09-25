using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    public partial class CreateProfileForm : Form
    {
        private FolderProfile folderProfile;

        public CreateProfileForm()
        {
            InitializeComponent();
        }

        public void CreateProfileForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.EnableMainWindowForm();
        }

        private void SaveFolderProfile_Click(object sender, EventArgs e)
        {
            folderProfile.name = ProfileNameBox.Text;
            DebugInfo.Text = "";
            if (!CheckData())
                return;
            if (!CheckForIdenticalNames(GlobalVars.userDataPath + "\\FolderProfiles.json"))
            {
                DebugInfo.Text = "Profile name already in use! Choose a new one!";
                return;
            }
            var fileContent = JsonConvert.SerializeObject(GlobalVars.folderProfiles, Formatting.Indented);
            File.WriteAllText(GlobalVars.userDataPath + "\\FolderProfiles.json", fileContent);
            Util.UpdateAllFolderProfiles();
            Close();
        }

        private bool CheckForIdenticalNames(string path)
        {
            var json = File.ReadAllText(path);
            GlobalVars.folderProfiles = JsonConvert.DeserializeObject<List<FolderProfile>>(json);
            if (GlobalVars.folderProfiles == null || GlobalVars.folderProfiles.Count == 0)
            {
                GlobalVars.folderProfiles = new List<FolderProfile> { folderProfile };
                return true;
            }
            if (GlobalVars.folderProfiles.Any(item => item.name.Equals(folderProfile.name)))
            {
                return false;
            }
            GlobalVars.folderProfiles.Add(folderProfile);
            return true;
        }

        private void ModFolderBox_Enter(object sender, EventArgs e)
        {
            ModFolderBox.BeginInvoke((MethodInvoker)delegate
            {
                ModFolderBox.SelectAll();
            });
        }

        private void VanillaFolderBox_Enter(object sender, EventArgs e)
        {
            VanillaFolderBox.BeginInvoke((MethodInvoker)delegate
            {
                VanillaFolderBox.SelectAll();
            });
        }

        private void LocalizationFolderBox_Enter(object sender, EventArgs e)
        {
            LocalisationFolderBox.BeginInvoke((MethodInvoker)delegate
            {
                LocalisationFolderBox.SelectAll();
            });
        }

        private void ProfileNameBox_Enter(object sender, EventArgs e)
        {
            ProfileNameBox.BeginInvoke((MethodInvoker)delegate
            {
                ProfileNameBox.SelectAll();
            });
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectModFolder_Click(object sender, EventArgs e)
        {
            folderProfile.modFolder = Util.FolderFileDialog();
            ModFolderBox.Text = folderProfile.modFolder;
            CheckData();
        }

        private void SelectVanillaFolder_Click(object sender, EventArgs e)
        {
            folderProfile.vanillaFolder = Util.FolderFileDialog();
            VanillaFolderBox.Text = folderProfile.vanillaFolder;
            CheckData();
        }

        private void SelectLocalizationFolder_Click(object sender, EventArgs e)
        {
            folderProfile.localizationFolder = Util.FolderFileDialog();
            LocalisationFolderBox.Text = folderProfile.localizationFolder;
            CheckData();
        }

        private void CopyModFolder_Click(object sender, EventArgs e)
        {
            folderProfile.modFolder = GlobalVars.modFolder;
            ModFolderBox.Text = GlobalVars.modFolder;
            CheckData();
        }

        private void CopyVanillaFolder_Click(object sender, EventArgs e)
        {
            folderProfile.vanillaFolder = GlobalVars.vanillaFolder;
            VanillaFolderBox.Text = GlobalVars.vanillaFolder;
            CheckData();
        }

        private void CopyLocalizationFolder_Click(object sender, EventArgs e)
        {
            folderProfile.localizationFolder = GlobalVars.localizationsFile;
            LocalisationFolderBox.Text = GlobalVars.localizationsFile;
            CheckData();
        }

        private void ProfileNameBox_TextChanged(object sender, EventArgs e)
        {
            if (!ValidProfileName())
                return;
            DebugInfo.Text = "";
        }

        public bool ValidProfileName()
        {
            if (string.IsNullOrEmpty(ProfileNameBox.Text))
            {
                DebugInfo.Text = "Your profile must have a valid name!";
                return false;
            }

            if (!GlobalVars.folderProfiles.Any(item => item.name.Equals(ProfileNameBox.Text))) return true;
            DebugInfo.Text = "This profile name is already used.";
            return false;
        }

        public bool CheckData()
        {
            DebugInfo.Text = "";
            if (folderProfile.modFolder is null or "")
            {
                DebugInfo.Text += "No Mod folder selected! ";
                return false;
            }
            if (folderProfile.localizationFolder is null or "")
            {
                DebugInfo.Text += "No Localisation folder selected ";
                return false;
            }
            if (folderProfile.vanillaFolder is null or "")
            {
                DebugInfo.Text += "No Vanilla folder selected! ";
                return false;
            }
            if (ValidProfileName())
                return true;
            DebugInfo.Text += "Invalid Profile Name!";
            return false;
        }

        private void ModFolderBox_TextChanged(object sender, EventArgs e)
        {
            folderProfile.modFolder = ModFolderBox.Text;
        }

        private void VanillaFolderBox_TextChanged(object sender, EventArgs e)
        {
            folderProfile.vanillaFolder = VanillaFolderBox.Text;
        }

        private void LocalisationFolderBox_TextChanged(object sender, EventArgs e)
        {
            folderProfile.localizationFolder = LocalisationFolderBox.Text;
        }
    }
}
