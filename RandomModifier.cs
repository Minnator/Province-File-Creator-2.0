using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    public partial class RandomModifier : Form
    {
        private readonly List<Modifier> bMod = new();
        private readonly List<Modifier> iMod = new();
        private readonly List<Modifier> fMod = new();
        private readonly List<Modifier> uMod = new();

        public RandomModifier()
        {
            InitializeComponent();
            CreateSublist();
        }

        private void CreateSublist()
        {
            foreach (var item in GlobalVars.modifiers)
            {
                switch (item.type)
                {
                    case "boolean":
                        bMod.Add(item);
                        break;
                    case "integer":
                        iMod.Add(item);
                        break;
                    case "float":
                        fMod.Add(item);
                        break;
                    default:
                        uMod.Add(item);
                        break;
                }
            }
        }

        private void GetModifierButton_Click(object sender, EventArgs e)
        {
            if (UseCategory.Checked)
            {
                var type = CategoriesBox.SelectedItem;
                switch (type)
                {
                    case "boolean":
                        ModifierBox.Text = bMod[GlobalVars.GlobalRandom.Next(0, bMod.Count - 1)].ToString();
                        break;
                    case "integer":
                        ModifierBox.Text = iMod[GlobalVars.GlobalRandom.Next(0, iMod.Count - 1)].ToString();
                        break;
                    case "float":
                        ModifierBox.Text = fMod[GlobalVars.GlobalRandom.Next(0, fMod.Count - 1)].ToString();
                        break;
                    case "unknown":
                        ModifierBox.Text = uMod[GlobalVars.GlobalRandom.Next(0, uMod.Count - 1)].ToString();
                        break;
                    default:
                        ModifierBox.Text = ModifierBox.Text;
                        break;
                }
            }
            else
            {
                ModifierBox.Text = GlobalVars.modifiers[GlobalVars.GlobalRandom.Next(0, GlobalVars.modifiers.Count - 1)].ToString();
            }
        }
        
        private void ModifierBox_Enter(object sender, EventArgs e)
        {
            ModifierBox.BeginInvoke((MethodInvoker)delegate { ModifierBox.SelectAll(); });
        }
    }
}
