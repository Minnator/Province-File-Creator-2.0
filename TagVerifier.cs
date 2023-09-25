using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    public partial class TagVerifier : Form
    {
        public TagVerifier()
        {
            InitializeComponent();
            SuggestTag.Checked = GlobalVars.settings.tagVerifierSettings.suggestTag;
            Loading.LoadCountriesAndTags();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void VerifyButton_Click(object sender, EventArgs e)
        {
            if (Util.CheckIfTagFree(new Tag(TagInputBox.Text)))
            {
                TagInputBox.ForeColor = Color.Green;
                return;
            }

            if (!SuggestTag.Checked)
            {
                TagInputBox.ForeColor = Color.Red;
                return;
            }
            var newTag = new Tag(TagInputBox.Text);
            do
            {
                newTag = new Tag(Util.IncrementTag(newTag.tag));
            } while (!Util.CheckIfTagFree(newTag));
            TagInputBox.Text = newTag.tag;
            TagInputBox.ForeColor = Color.Green;
        }

        private void TagInputBox_Enter(object sender, EventArgs e)
        {
            TagInputBox.BeginInvoke((MethodInvoker)delegate
            {
                TagInputBox.SelectAll();
            });
        }

        private void TagInputBox_TextChanged(object sender, EventArgs e)
        {
            TagInputBox.ForeColor = Color.Black;
            if (TagInputBox.Text.Length <= 3) return;
            TagInputBox.Text = TagInputBox.Text.Substring(0, 3);
            TagInputBox.SelectionStart = 3;
            TagInputBox.SelectionLength = 0;
        }

        private void NextTagButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TagInputBox.Text))
                TagInputBox.Text = "AAA";

            var newTag = new Tag(TagInputBox.Text);
            do
            {
                newTag = new Tag(Util.IncrementTag(newTag.tag));
            } while (!Util.CheckIfTagFree(newTag));
            TagInputBox.Text = newTag.tag;
            TagInputBox.ForeColor = Color.Green;
        }

        private void TagVerifier_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalVars.settings.tagVerifierSettings.suggestTag = SuggestTag.Checked;
        }
    }
}
