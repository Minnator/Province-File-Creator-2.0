using System;
using System.Drawing;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    public partial class ProvinceColorPicker : Form
    {
        public ProvinceColorPicker()
        {
            InitializeComponent();     
            Parsing.RgbParseFiles();
        }
        private void NewRBGButton_Click(object sender, EventArgs e)
        {
            if (GlobalVars.numOfProvs < 1)
                return;
            var newRGB = Util.GenerateRandomColor();
            rVal.Text = Convert.ToString(newRGB.r);
            gVal.Text = Convert.ToString(newRGB.g);
            bVal.Text = Convert.ToString(newRGB.b);
            ColorEx.BackColor = Color.FromArgb(newRGB.r, newRGB.g, newRGB.b);
            if (FunnyPicture.Image == null) return;
            FunnyPicture.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            FunnyPicture.Refresh();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
