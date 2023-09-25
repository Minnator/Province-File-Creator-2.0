using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    public partial class DeleteProfileForm : Form
    {
        public DeleteProfileForm()
        {
            InitializeComponent();
            UpdateFolderProfiles();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            GlobalVars.folderProfiles.RemoveAll(profile => profile.name == FolderProfileBox.Text);
            Saving.SaverUserProfiles();
            Util.UpdateAllFolderProfiles();
        }

        private void FolderProfileBox_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        public void UpdateFolderProfiles()
        {
            FolderProfileBox.Items.Clear();
            var objects = Array.ConvertAll(FPM.GetFolderProfileNames(), item => (object)item);
            PrintF.PrintListNewLines(objects.ToList());
            if (objects.Any())
                FolderProfileBox.Items.AddRange(objects);
        }
    }
}
