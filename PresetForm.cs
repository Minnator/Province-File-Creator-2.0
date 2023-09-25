using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    public partial class PresetForm : Form
    {
        public PresetForm()
        {
            InitializeComponent();
            UpdatePresetList();
        }

        public void UpdatePresetList()
        {
            PresetBox.Items.Clear();
            if (GlobalVars.presets == null || GlobalVars.presets.Count == 0)
                return;
            var objects = GlobalVars.presets.Select(obj => obj.profileName).ToList();
            if (objects.Any())
                PresetBox.Items.AddRange(objects.ToArray());
        }
        
        private void Delete_Click(object sender, EventArgs e)
        {
            ErrorBox.ForeColor = Color.Red;
            ErrorBox.Clear();
            GlobalVars.presets.RemoveAll(preset => preset.profileName.Equals(PresetBox.Text));
            Util.UpdateAllPresetBoxes();
            PresetBox.Text = "";
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            ErrorBox.Clear();
            if (!ContainsPreset())
            {
                ErrorBox.ForeColor = Color.Red;
                ErrorBox.Text = "There is no preset with such name to modify";
                return;
            }
            GlobalVars.presets.RemoveAll(preset => preset.profileName.Equals(PresetBox.Text));
            GlobalVars.presets.Add(GetData());
            Util.UpdateAllPresetBoxes();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            ErrorBox.Clear();
            if (PresetBox.Text == null || ContainsPreset() || PresetBox.Text.Equals("Enter an unused name") || PresetBox.Text == string.Empty)
            {
                ErrorBox.ForeColor = Color.Red;
                ErrorBox.Text = "Enter an unused name";
                PresetBox.BeginInvoke((MethodInvoker)delegate { PresetBox.SelectAll(); });
                return;
            }
            GlobalVars.presets.Add(GetData());
            Util.UpdateAllPresetBoxes();
        }

        private void PresetForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Saving.SaveUserDataToJson(GlobalVars.presets, "UserPresets.json");
        }

        private bool ContainsPreset()
        {
            return GlobalVars.presets.Any(item => item.profileName.Equals(PresetBox.Text));
        }

        public static void SetDataForSelectedPreset(string profileName)
        {
            var curPreset = GlobalVars.presets.FirstOrDefault(profile => profile.profileName == profileName);

            GlobalVars.main.AboriginalCheckbox.Checked = curPreset.aboriginal;
            GlobalVars.main.AndeanCheckbox.Checked = curPreset.andean;
            GlobalVars.main.WesternCheckbox.Checked = curPreset.western;
            GlobalVars.main.EasternCheckbox.Checked = curPreset.eastern;
            GlobalVars.main.MuslimCheckbox.Checked = curPreset.muslim;
            GlobalVars.main.CentralAfricanCheckbox.Checked = curPreset.centralAfrica;
            GlobalVars.main.EastAfricanCheckbox.Checked = curPreset.eastAfrican;
            GlobalVars.main.WestAfricanCheckbox.Checked = curPreset.westAfrican;
            GlobalVars.main.ChineseCheckbox.Checked = curPreset.chinese;
            GlobalVars.main.NomadicCheckbox.Checked = curPreset.nomadic;
            GlobalVars.main.IndianCheckbox.Checked = curPreset.indian;
            GlobalVars.main.PolynesialCheckbox.Checked = curPreset.polynesial;
            GlobalVars.main.HighAmericanCheckbox.Checked = curPreset.highAmerican;
            GlobalVars.main.NorthAmericanCheckbox.Checked = curPreset.northAmerica;
            GlobalVars.main.SouthAmericanCheckbox.Checked = curPreset.southAmerica;
            GlobalVars.main.MesoamericanCheckbox.Checked = curPreset.mesoamerican;
            GlobalVars.main.BaseManpowerInput.Text = curPreset.baseManpower;
            GlobalVars.main.BaseProductionInput.Text = curPreset.baseProduction;
            GlobalVars.main.BaseTaxInput.Text = curPreset.baseTax;
            GlobalVars.main.CapitalName.Text = curPreset.capital;
            GlobalVars.main.ProvIDInput.Text = curPreset.provinceId;
            GlobalVars.main.ProvNameTextInput.Text = curPreset.name;
            GlobalVars.main.ControllerInput.Text = curPreset.controller;
            GlobalVars.main.OwnerInput.Text = curPreset.owner;
            GlobalVars.main.ReligionList.SelectedItem = curPreset.religion;
            GlobalVars.main.CultureInput.Text = curPreset.culture;
            GlobalVars.main.CenterOfTradeLevelInput.Text = curPreset.centerOfTrade;
            GlobalVars.main.MinDevInput.Text = curPreset.minDev;
            GlobalVars.main.MaxDevInput.Text = curPreset.maxDev;
            GlobalVars.main.IsCityCheckbox.Checked = curPreset.city;
            GlobalVars.main.IsHRECheckbox.Checked = curPreset.hre;
            GlobalVars.main.IsFortCheckbox.Checked = curPreset.fort;
            GlobalVars.main.UseLatentTradeGood.Checked = curPreset.coal;
            GlobalVars.main.SameCCOCheckbox.Checked = curPreset.coreCreatorOwner;
            GlobalVars.main.IsUncolonised.Checked = curPreset.uncolonized;
            GlobalVars.main.CoresTagInput.Text = curPreset.cores;
            GlobalVars.main.UseRandomDev.Checked = curPreset.randomDevelopment;
            GlobalVars.main.ProvNameCapitalName.Checked = curPreset.capitalAndName;
            GlobalVars.main.TradegoodsList.SelectedItem = curPreset.tradegood;
            GlobalVars.main.OttomanCheckbox.Checked = curPreset.ottoman;
        }
        private Preset GetData()
        {
            var gcores = GlobalVars.main.CoresTagInput.Text.Where(
                c => Regex.IsMatch(c.ToString(), @"([A-Za-z\d+]{3})")).ToList();
            return new Preset
            {
                aboriginal = GlobalVars.main.AboriginalCheckbox.Checked,
                andean = GlobalVars.main.AndeanCheckbox.Checked,
                western = GlobalVars.main.WesternCheckbox.Checked,
                eastern = GlobalVars.main.EasternCheckbox.Checked,
                muslim = GlobalVars.main.MuslimCheckbox.Checked,
                centralAfrica = GlobalVars.main.CentralAfricanCheckbox.Checked,
                eastAfrican = GlobalVars.main.EastAfricanCheckbox.Checked,
                westAfrican = GlobalVars.main.WestAfricanCheckbox.Checked,
                chinese = GlobalVars.main.ChineseCheckbox.Checked,
                nomadic = GlobalVars.main.NomadicCheckbox.Checked,
                indian = GlobalVars.main.IndianCheckbox.Checked,
                polynesial = GlobalVars.main.PolynesialCheckbox.Checked,
                highAmerican = GlobalVars.main.HighAmericanCheckbox.Checked,
                northAmerica = GlobalVars.main.NorthAmericanCheckbox.Checked,
                southAmerica = GlobalVars.main.SouthAmericanCheckbox.Checked,
                mesoamerican = GlobalVars.main.MesoamericanCheckbox.Checked,
                baseManpower = GlobalVars.main.BaseManpowerInput.Text,
                baseProduction = GlobalVars.main.BaseProductionInput.Text,
                baseTax = GlobalVars.main.BaseTaxInput.Text,
                capital = GlobalVars.main.CapitalName.Text,
                provinceId = GlobalVars.main.ProvIDInput.Text,
                name = GlobalVars.main.ProvNameTextInput.Text,
                controller = GlobalVars.main.ControllerInput.Text,
                owner = GlobalVars.main.OwnerInput.Text,
                religion = GlobalVars.main.ReligionList.Text,
                culture = GlobalVars.main.CultureInput.Text,
                centerOfTrade = GlobalVars.main.CenterOfTradeLevelInput.Text,
                minDev = GlobalVars.main.MinDevInput.Text,
                maxDev = GlobalVars.main.MaxDevInput.Text,
                city = GlobalVars.main.IsCityCheckbox.Checked,
                hre = GlobalVars.main.IsHRECheckbox.Checked,
                fort = GlobalVars.main.IsFortCheckbox.Checked,
                coal = GlobalVars.main.UseLatentTradeGood.Checked,
                coreCreatorOwner = GlobalVars.main.SameCCOCheckbox.Checked,
                uncolonized = GlobalVars.main.IsUncolonised.Checked,
                profileName = PresetBox.Text,
                cores = GlobalVars.main.CoresTagInput.Text,
                randomDevelopment = GlobalVars.main.UseRandomDev.Checked,
                capitalAndName = GlobalVars.main.ProvNameCapitalName.Checked,
                tradegood = GlobalVars.main.TradegoodsList.Text,
                ottoman = GlobalVars.main.OttomanCheckbox.Checked
            };
        }

        private void PresetBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (PresetBox.Text == null || GlobalVars.presets.All(profile => profile.profileName != PresetBox.Text))
                return;
            SetDataForSelectedPreset(PresetBox.Text);
        }

        private void PresetBox_Enter(object sender, EventArgs e)
        {
            PresetBox.BeginInvoke((MethodInvoker)delegate { PresetBox.SelectAll(); });
        }
    }
}
