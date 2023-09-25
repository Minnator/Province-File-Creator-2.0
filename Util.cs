using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    internal class Util
    {

        public static string FolderFileDialog()
        {
            using var folderBrowserDialog = new FolderBrowserDialog();
            return folderBrowserDialog.ShowDialog() == DialogResult.OK ? folderBrowserDialog.SelectedPath : "";
        }

        public static string GetModOrVanillaFile(string tag)
        {
            var path = Path.Combine(GlobalVars.modFolder, $"common\\{GetPathFromCountry(tag)}");
            //Debug.WriteLine($"PATH mod: {path}");
            return File.Exists(path) ?
                Path.Combine(path.Replace('/', '\\')) : GetVanillaPath(tag);
        }
        public static string ReadFileWithAnsiEncoding(string filePath)
        {
            if (!File.Exists(filePath))
                return string.Empty;
            // Use Encoding.GetEncoding(1252) for ANSI encoding
            using StreamReader reader = new (filePath, Encoding.GetEncoding(1252));
            return reader.ReadToEnd();
        }
        /// <summary>
        /// Takes a string representing a tag as input
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static string GetVanillaPath(string tag)
        {
            var path = Path.Combine(GlobalVars.vanillaFolder, $"common\\{GetPathFromCountry(tag)}");
            //Debug.WriteLine($"PATH vanilla: {path}");
            return File.Exists(path) ?
                Path.Combine(path.Replace('/', '\\')) : "";
        }
        private static string GetPathFromCountry(string input)
        {
            return GlobalVars.tags.FirstOrDefault(item => item.tag.Equals(input)).countryFile;
        }
        public static bool CheckIfTagFree(Tag input)
        {
            return GlobalVars.tags.All(tag => tag.tag != input.tag);
        }
        private static char GetNextChar(char currentChar)
        {
            var nextChar = (char)(currentChar + 1);
            return nextChar > 'Z' ? 'A' : nextChar;
        }

        public static void SelectContent(TextBox box)
        {
            box.BeginInvoke((MethodInvoker)box.SelectAll);
        }
        public static string IncrementTag(string tag)
        {
            var tagChars = tag.ToCharArray();
            var lastIndex = tag.Length - 1;

            for (var i = lastIndex; i >= 0; i--)
            {
                var currentChar = tagChars[i];
                var nextChar = GetNextChar(currentChar);

                tagChars[i] = nextChar;

                if (currentChar != 'Z')
                    break;
            }

            return new string(tagChars);
        }
        public static bool VerifyTag(Tag input)
        {
            return GlobalVars.tags.Any(tag => tag.tag.Equals(input.tag));
        }
        public static void UpdateAllFolderProfiles()
        {
            // TODO also update the List in Delete Profiles
            GlobalVars.main.UpdateFolderProfiles();
            if (GlobalVars.deleteProfileForm == null || GlobalVars.deleteProfileForm.IsDisposed)
                return;
            GlobalVars.deleteProfileForm.UpdateFolderProfiles();
        }

        public static void UpdateAllPresetBoxes()
        {
            GlobalVars.main.UpdatePresets();
            if (GlobalVars.presetForm == null || GlobalVars.presetForm.IsDisposed)
                return;
            GlobalVars.presetForm.UpdatePresetList();
        }
        
        public static Rgb GenerateRandomColor()
        {
            while (true)
            {
                var r = GlobalVars.GlobalRandom.Next(256);
                var g = GlobalVars.GlobalRandom.Next(256);
                var b = GlobalVars.GlobalRandom.Next(256);
                var newColor = new Rgb(GlobalVars.numOfProvs + 1, r, g, b);

                if (!GlobalVars.colors.Contains(newColor))
                {
                    return newColor;
                }
            }
        }
        public static void GetSettings()
        {
            GlobalVars.settings.lastProfile = GlobalVars.main.FolderProfileBox.SelectedItem?.ToString();
            GlobalVars.settings.loadFilesOnProfileSelect = GlobalVars.main.LoadFilesOnProfileSelect.Checked;
            GlobalVars.settings.loadLastProfile = GlobalVars.main.LoadProfileOnLaunch.Checked;
            GlobalVars.settings.startNewProvId = GlobalVars.main.StartWithNewId.Checked;
            GlobalVars.settings.saveSettingsOnClose = GlobalVars.main.SaveSettingsOnClose.Checked;
            GlobalVars.settings.startWithNewRgb = GlobalVars.main.StartWithNewRgb.Checked;
        }

        public static void ErrorPopUp(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public static string ConvertArrayToString(Rgb[] array)
        {
            StringBuilder sb = new ();
            foreach (var item in array)
            {
                sb.Append(item.ToStringFancy()).Append(Environment.NewLine);
            }
            return sb.ToString();
        }
        public static string ConvertArrayToString(IEnumerable<string> array)
        {
            //WriteLog(PrintF.ArrayToString(array), "TextInput.txt");

            StringBuilder sb = new ();
            foreach (var item in array)
            {
                if (item == null)
                    continue;
                sb.Append(item).Append(Environment.NewLine);
            }
            return sb.ToString();
        }
        public static string FindFileWithName(string name)
        {
            Debug.WriteLine($"trying to find file: {name}");
            if (GlobalVars.provinceFiles == null) return null;
            if (GlobalVars.provinceFiles.Count == 0) return null;
            foreach (var file in GlobalVars.provinceFiles.Where(file => file.Contains($"\\{name} -") || file.Contains($"\\{name}-")))
            {
                return file;
            }
            return "";
        }
        public static void WriteLog(string text, string filename)
        {
            var streamWriter = new StreamWriter(GlobalVars.appPath + $"{filename}.txt");
            streamWriter.WriteLine(text);
            streamWriter.Close();
        }
        /// <summary>
        /// Reads all text as UTF-8 and returns it as a string
        /// </summary>
        /// <param name="path"></param>
        public static string ReadAllTextCsv(string path)
        {
            if (!File.Exists(path)) return string.Empty;
            var lines = File.ReadAllLines(path);
            //Debug.WriteLine(PrintF.ArrayToString(lines));

            var newLines = new string[lines.Length - 1];
            Array.Copy(lines, 1, newLines, 0, lines.Length - 1);
            return ConvertArrayToString(newLines);
        }

        /// <summary>
        /// Generates a random number between 0 and 6 (inclusive) with a distribution that centers
        /// around 3 using gaussian distribution
        /// </summary>
        /// <returns></returns>
        public static int GenerateRandomIntGaussian()
        {
            // Generate a random number with a normal distribution centered around 3
            var u1 = 1.0 - GlobalVars.GlobalRandom.NextDouble(); // Uniform random number between 0 and 1
            var u2 = 1.0 - GlobalVars.GlobalRandom.NextDouble(); // Uniform random number between 0 and 1
            var normalRandom = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2);

            // Scale and shift the normalRandom to fit the desired range (0 to 6)
            var randomNumber = Math.Max(0, Math.Min(6, (int)(normalRandom * 1.5 + 3.5)));

            return randomNumber;
        }
        /// <summary>
        /// distributes x pips randomly across the pics
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] DistributePipsRandomly(int value)
        {
            var numbers = new int[4];
            for (var i = 0; i < value; i++)
            {
                var index = GlobalVars.GlobalRandom.Next(numbers.Length);
                if (numbers[index] >= 6)
                    numbers[index + 1] += 1;
                else
                    numbers[index] += 1;
            }
            return numbers;
        }

        public static string[] StringToArray(string input)
        {
            var matches = Regex.Matches(input, @"(.*)\s");
            return matches.Cast<Match>().Select(match => match.Value).ToArray();
        }
        /// <summary>
        /// Returns the path to the countries history
        /// </summary>
        /// <returns></returns>
        public static CountryHistory GetPathToTagCH(string inputTag)
        {
            inputTag = inputTag.ToUpper();
            return GlobalVars.countryHistories.FirstOrDefault(ch => ch.tag == inputTag);
        }
        /// <summary>
        /// Generalize entries by inheritance for future features
        /// </summary>
        /// <returns></returns>
        public static string AddEntryToCountryHistory()
        {
            return string.Empty;
        }

        public static string AddLeaderToCountryHistory(KeyValuePair<string, string> leader, string[] entries)
        {
            var index = 0;
            var dMatch = Regex.Match(leader.Value, @"(\d{1,4}).(\d{1,2}).(\d{1,2})");
            var lDate = new DateTime(int.Parse(dMatch.Groups[1].Value), int.Parse(dMatch.Groups[2].Value),
                int.Parse(dMatch.Groups[3].Value));
            //Debug.WriteLine($"Leader Date: {lDate}");
            foreach (var item in entries)
            {
                try
                {
                    dMatch = Regex.Match(item, @"(\d{1,4}).(\d{1,2}).(\d{1,2})");
                    var curDate = new DateTime(int.Parse(dMatch.Groups[1].Value), int.Parse(dMatch.Groups[2].Value),
                        int.Parse(dMatch.Groups[3].Value));
                    //Debug.WriteLine($"Entry date: {curDate}");

                    if (lDate < curDate)
                    {
                        entries[index-1] = $"{entries[index-1]}{Environment.NewLine}{leader.Value}";

                        //Debug.WriteLine($"FOUND DATE CLOSE TO ENTRY!\n{entries[index]}");
                        break;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"No Date Time in {item}");
                }
                index++;
                if ( index == entries.Length )
                    entries[index - 1] = $"{entries[index - 1]}{Environment.NewLine}{leader.Value}";
            }
            return PrintF.ArrayToString(entries);
        }

    }
}
