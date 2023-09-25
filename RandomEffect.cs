using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    public partial class RandomEffect : Form
    {
        public RandomEffect()
        {
            InitializeComponent();
            
        }

        private void GetModifierButton_Click(object sender, EventArgs e)
        {
            if (GlobalVars.effects == null || GlobalVars.effects.Count < 1) 
                return;
            EffectBox.Text = GlobalVars.effects[GlobalVars.GlobalRandom.Next(0, GlobalVars.effects.Count)];
        }

        private void EffectBox_Enter(object sender, EventArgs e)
        {
            EffectBox.BeginInvoke((MethodInvoker)delegate { EffectBox.SelectAll(); });
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(EffectBox.Text);
        }
    }
}
