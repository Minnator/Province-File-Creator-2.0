using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace EU4_Province_Creator
{
    public partial class Features : Form
    {
        private string[] names;
        private readonly List<string> formatted = new();
        private string[] fileToModify = new string[3];
        public Features()
        {
            InitializeComponent();
            // Loads the country files and their paths
            Loading.LoadCountriesAndTags();
            // Load tables 
            PopulateRulerChanceTable();
            LoadSettingsRulerFormatter();
            Loading.LoadCountryHistoriesNames();
            //Util.WriteLog(PrintF.DebugPrintListNewLinesS(GlobalVars.tags), "DEBUG");
        }
        #region Ruler Formatter

        private void LoadSettingsRulerFormatter()
        {
            RandomOrdinalNumberBox.Checked = GlobalVars.settings.rulerSettings.randomOrdinal;
            MinOrdinalNum.Text = GlobalVars.settings.rulerSettings.min.ToString();
            MaxOrdinalNum.Text = GlobalVars.settings.rulerSettings.max.ToString();
            FixedOrdinalNum.Text = GlobalVars.settings.rulerSettings.fixedOrdinal.ToString();
            CopyToModBox.Checked = GlobalVars.settings.rulerSettings.copyVanillaToMod;
            RulerOverrideFile.Checked = GlobalVars.settings.rulerSettings.overrideFileData;
            RulerSaveToFile.Checked = GlobalVars.settings.rulerSettings.saveToCountryFile;
        }

        private void PopulateRulerChanceTable()
        {
            // Create and add TextBox controls to the TableLayoutPanel
            for (var row = 1; row < RulerChancesTable.RowCount; row++)
            {
                for (var col = 0; col < RulerChancesTable.ColumnCount; col++)
                {
                    TextBox textBox = new ();
                    RulerChancesTable.Controls.Add(textBox, col, row);
                }
            }
        }
        private void PreviewButtonRuler_Click(object sender, EventArgs e)
        {
            RulerOutputBox.Clear();
            GetTableData();
            FormatRulers();
            RulerOutputBox.Text = PrintF.PrintListNewLinesS(formatted);
        }

        private void CopyButtonRuler_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(RulerOutputBox.Text);
        }

        private void SaveButtonRuler_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CountryTagBox.Text))
                return;
            string path;
            if (!RulerSaveToFile.Checked)
                return;
            if (!GlobalVars.tags.Any(item => item.tag.Equals(CountryTagBox.Text)))
            {
                Debug.WriteLine("TAG NOT FOUND: " + CountryTagBox.Text);
                RulerOutputBox.ForeColor = Color.Red;
                RulerOutputBox.Text = $"The given tag: {CountryTagBox.Text} does not exist. Please create the tag first!";
                RulerOutputBox.ForeColor = Color.Black;
                return;
            }

            path = !CopyToModBox.Checked ? 
                Util.GetVanillaPath(CountryTagBox.Text) : Util.GetModOrVanillaFile(CountryTagBox.Text);
            if (string.IsNullOrEmpty(path))
                return;
            Debug.WriteLine($"Path to country file: {path}");
            GetFileParts(path);
            if (!RulerOverrideFile.Checked)
            {
                fileToModify[1] += RulerOutputBox.Text;
                //TODO APPEND TO FILE IN ANSI
                Saving.OverrideTextFileANSI(path, PrintF.ArrayToString(fileToModify));
                return;
            }
            fileToModify[1] = RulerOutputBox.Text;
            Saving.OverrideTextFileANSI(path, PrintF.ArrayToString(fileToModify));
        }

        private void GetFileParts(string path)
        {
            string file;
            if (path.Equals(""))
                return;
            try
            {
                file = Util.ReadFileWithAnsiEncoding(path);
            }
            catch
            {
                return;
            }
            var matches = Regex.Matches(file, @"([\s\S.]+monarch_names\s*=\s*{)([.\s\S]+)(}\s+leader_names[.\s\S]+)");
            foreach (var item in matches.Cast<Match>())
            {
                fileToModify[0] = "\n" + item.Groups[1].Value;
                fileToModify[1] = item.Groups[2].Value;
                fileToModify[2] = item.Groups[3].Value;
            }
            //Debug.WriteLine("Value of FileToModify: " + PrintF.PrintListNewLinesS(fileToModify.ToList()));

        }
        private void RandomOrdinalNumberBox_CheckedChanged(object sender, EventArgs e)
        {
            MinOrdinalNum.Enabled = RandomOrdinalNumberBox.Checked;
            MaxOrdinalNum.Enabled = RandomOrdinalNumberBox.Checked;
            FixedOrdinalNum.Enabled = !RandomOrdinalNumberBox.Checked;
        }

        private void GetTableData()
        {
            Dictionary<Vector2D, int> tableData = new ();
            for (var row = 1; row < RulerChancesTable.RowCount; row++)
            {
                for (var col = 0; col < RulerChancesTable.ColumnCount; col++)
                {
                    var control = RulerChancesTable.GetControlFromPosition(col, row);

                    if (control is not TextBox textBox) continue;
                    Vector2D position = new (col, row);
                    if (int.TryParse(textBox.Text, out var value))
                        tableData[position] = value;  
                    else
                        tableData[position] = 1;
                }
            }
            GlobalVars.rulerChanceTableDic.Clear();
            GlobalVars.rulerChanceTableDic = tableData;
        }

        private void MinOrdinalNum_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(MinOrdinalNum.Text, out var min)) 
                if (min >= 0)
                    return;
            MinOrdinalNum.Text = "0";
            MinOrdinalNum.SelectionStart = MinOrdinalNum.Text.Length;
            MinOrdinalNum.SelectionLength = 0;
        }

        private void MaxOrdinalNum_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(MaxOrdinalNum.Text, out var max))
                if (max >= 0)
                    return;
            MaxOrdinalNum.Text = "0";
            MaxOrdinalNum.SelectionStart = MaxOrdinalNum.Text.Length;
            MaxOrdinalNum.SelectionLength = 0;
        }

        private void FixedOrdinalNum_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(FixedOrdinalNum.Text, out var fix))
                if (fix >= 0)
                    return;
            FixedOrdinalNum.Text = "0";
            FixedOrdinalNum.SelectionStart = FixedOrdinalNum.Text.Length;
            FixedOrdinalNum.SelectionLength = 0;
        }

        private void CountryTagBox_TextChanged(object sender, EventArgs e)
        {
            if (CountryTagBox.Text.Length <= 3) return;
            CountryTagBox.Text = CountryTagBox.Text.Substring(0, 3);
            CountryTagBox.SelectionStart = 3;
            CountryTagBox.SelectionLength = 0;
        }

        private void RulerInputBox_TextChanged(object sender, EventArgs e)
        {
            names = Regex.Split(RulerInputBox.Text, @"\s+", RegexOptions.None);
        }

        private static int GetChance() 
        {
            //PrintF.PrintDictionarySpacedTitle("Table data", GlobalVars.rulerChanceTableDic);
            foreach (var kvp in GlobalVars.rulerChanceTableDic.Where
                         (kvp => kvp.Key.x == 0 && kvp.Value > 0))
            {
                GlobalVars.rulerChanceTableDic[kvp.Key] = kvp.Value - 1;
                var loc = kvp.Key;
                loc.x = 1;
                return GlobalVars.rulerChanceTableDic[loc];
            }
            return 1;
        }
        private string GetOrdinalNumber()
        {
            if (!RandomOrdinalNumberBox.Checked) 
                return FixedOrdinalNum.Text is null or "" ? "0" : FixedOrdinalNum.Text;
            if (MinOrdinalNum.Text is null or "" || MaxOrdinalNum.Text is null or "")
                return "0";
            return int.Parse(MinOrdinalNum.Text) >  int.Parse(MaxOrdinalNum.Text) ? 
                FixedOrdinalNum.Text : GlobalVars.GlobalRandom.Next(int.Parse(MinOrdinalNum.Text), int.Parse(MaxOrdinalNum.Text)).ToString();
        }
        private void FormatRulers()
        {
            formatted.Clear();
            if (names == null || names.Length == 0)
                return;
            foreach (var name in names)
            {
                formatted.Add($"\t\"{name} #{GetOrdinalNumber()}\" = {GetChance()}");
            }
        }

        #endregion

        #region Leader Formatter
        private void RulerFormatterTab_DragDrop(object sender, DragEventArgs e)
        {

        }

        #region Input verifying
        private void LeaderCountryTagBox_TextChanged(object sender, EventArgs e)
        {
            if (CountryTagBox.Text.Length <= 3) return;
            CountryTagBox.Text = CountryTagBox.Text.Substring(0, 3);
            CountryTagBox.SelectionStart = 3;
            CountryTagBox.SelectionLength = 0;
        }
        
        /*
        private void FireSkillBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(FireSkillBox.Text, out var fire))
                FireSkillBox.Text = "";
            FireSkillBox.Text = fire switch
            {
                > 6 => "6",
                < 0 => "0",
                _ => FireSkillBox.Text
            };
        }

        private void ShockSkillBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(ShockSkillBox.Text, out var shock))
                ShockSkillBox.Text = "";
            ShockSkillBox.Text = shock switch
            {
                > 6 => "6",
                < 0 => "0",
                _ => ShockSkillBox.Text
            };
        }

        private void ManeuverSkillBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(ManeuverSkillBox.Text, out var maneuver))
                ManeuverSkillBox.Text = "";
            ManeuverSkillBox.Text = maneuver switch
            {
                > 6 => "6",
                < 0 => "0",
                _ => ManeuverSkillBox.Text
            };
        }

        private void SiegeSkillBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(SiegeSkillBox.Text, out var siege))
                SiegeSkillBox.Text = "";
            SiegeSkillBox.Text = siege switch
            {
                > 6 => "6",
                < 0 => "0",
                _ => SiegeSkillBox.Text
            };
        }
        */

        #endregion

        #region Random Generation

        public void RandomRulerStats()
        {

        }


        #endregion

        #endregion

        private void LeaderDateBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void LeaderDateBox_Enter(object sender, EventArgs e)
        {
            Util.SelectContent(LeaderDateBox);
        }

        private void CsvFileBox_Enter(object sender, EventArgs e)
        {
            Util.SelectContent(CsvFileBox);
        }

        private void AdmBox_Enter(object sender, EventArgs e)
        {
            Util.SelectContent(AgeLeaderBox);
        }

        private void CsvFileBox_TextChanged(object sender, EventArgs e)
        {
            if (!File.Exists(CsvFileBox.Text))
            {
                LeaderErrorBox.Text = "Cannot find the given .csv file!";
                return;
            }
            SaveLeadersFromCsv(CsvFileBox.Text);
        }

        private void DropFilesHereButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(CsvFileBox.Text))
            {
                LeaderErrorBox.Text = "Cannot find the given .csv file!";
                return;
            }
            SaveLeadersFromCsv(CsvFileBox.Text);
        }

        private void PreviewButtonLeader_Click(object sender, EventArgs e)
        {
            
        }



        public void SaveLeadersFromCsv(string path)
        {
            Parsing.ParseLeaderFromFile(path);
            Formatter.FormatAllRulerHistory();
            LOutputBox.Text = PrintF.RulerToString();
            //Util.WriteLog(PrintF.RulerToString(), "Formatted.txt");
            Saving.SaveLeadersToHistoryFile();
        }

        private void DropFilesHereButton_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length <= 0) 
                return;
            if (!File.Exists(files[0]))
            {
                LeaderErrorBox.Text = "Cannot find the given .csv file!";
                return;
            }
            SaveLeadersFromCsv(files[0]);
        }

        private void DropFilesHereButton_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void ExampleCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        
    }

}
