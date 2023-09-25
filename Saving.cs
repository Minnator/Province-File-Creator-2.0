using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace EU4_Province_Creator
{
    internal static class Saving
    {
        public static void SaveAllUserData()
        {
            SaveSettings();
            SaverUserProfiles();
        }

        public static void SaveClimate()
        {
            Parsing.ParseImpassable();
            StringBuilder sb = new();
            var cnt = 0;
            sb.Append("impassable = {").Append(Environment.NewLine + "\t");
            foreach (var id in GlobalVars.impassable)
            {
                if (cnt == 20)
                {
                    cnt = 0;
                    sb.Append(id + " ").Append(Environment.NewLine + "\t");
                }
                else
                {
                    sb.Append(id + " ");
                }
                cnt ++;
            }
            sb.Append($"{Environment.NewLine}" + "}");

            string path;
            if (File.Exists(Path.Combine(GlobalVars.modFolder, "map", "climate.txt")))
            {
                path = Path.Combine(GlobalVars.modFolder, "map", "climate.txt");
            }
            else
            {
                path = Path.Combine(GlobalVars.modFolder, "map", "climate.txt");
                File.Create(path);
            }
            if (!File.Exists(path))
                return;

            var updatedClimate =
                Regex.Replace(File.ReadAllText(path), "impassable = \\{[\\s\\S]*?\\}", sb.ToString());
            File.WriteAllText(path, updatedClimate);
        }
        public static void SaveSettings()   
        {
            Debug.WriteLine(GlobalVars.settings);
            SaveUserDataToJson(GlobalVars.settings, "Settings.json");
        }

        /// <summary>
        /// Saves data in the .json format in the \User\[path].json file
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        public static void SaveUserDataToJson(object obj, string path)
        {
            Debug.WriteLine(Path.Combine(GlobalVars.userDataPath, path));
            File.WriteAllText(Path.Combine(GlobalVars.userDataPath, path), JsonConvert.SerializeObject(obj, Formatting.Indented));
        }
        public static void SaverUserProfiles()
        {
            SaveUserDataToJson(GlobalVars.folderProfiles, "FolderProfiles.json");
        }

        /// <summary>
        /// Saves a file in .csv format without BOM-Ending and unix-LF NewLine
        /// Verifies, if the path and content are valid 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void SaveToCsv(string path, string content)
        {
            if (!File.Exists(path) || content == null) 
                return;
            using StreamWriter writer = new(path, true, Encoding.UTF8);
            writer.NewLine = "\n"; // Set the line ending to Unix-style LF
            writer.Write(content);
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// saves any kind of .txt file to the provide path!
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        public static void SaveTextFile(string path, string text)
        {
            if (File.Exists(path) || text == null)
                return;
            Debug.Write($"Reached here with {text} \n\n\nand path: {path}");
            using StreamWriter writer = new(path, false, Encoding.UTF8);
            writer.NewLine = "\n"; // Set the line ending to Unix-style LF
            writer.Write(text);
            writer.Flush();
            writer.Close();
        }
        public static void OverrideTextFileANSI(string path, string text)
        {
            if (!File.Exists(path) || text == null)
                return;
            //Debug.Write($"Reached here with {text} \n\n\nand path: {path}");
            using StreamWriter writer = new(GlobalVars.appPath + "\\TESTS.TXT", false, Encoding.GetEncoding(1252));
            writer.Write(text);
        }

        private static void WriteFileANSI(string path, string text, bool append = false)
        {
            if (text == null)
                return;
            //Debug.Write($"Reached here with {text} \n\n\nand path: {path}");
            using StreamWriter writer = new(path, append, Encoding.GetEncoding(1252));
            writer.Write(text);
        }

        private static void ModifyTextFile(string path, string text)
        {
            if (!File.Exists(path) || text == null)
                return;

            //File.WriteAllText(path, text);
            using StreamWriter writer = new(path, false, Encoding.UTF8);
            writer.WriteLine(text);
            writer.Flush();
            writer.Close();
            
        }

        #region Modifiying of files
        public static void EditDefinition(ref List<Rgb> rGBs, string provName)
        {
            if (!File.Exists(GlobalVars.modFolder + "\\map\\definition.csv"))
                return;
            StringBuilder sb = new();
            sb.Append(Environment.NewLine).Append($"{GlobalVars.main.ProvIDInput.Text};" + GlobalVars.ColorsId[int.Parse(GlobalVars.main.ProvIDInput.Text)].ToString());
            sb.Append(provName).Append(";x");
            Debug.WriteLine(sb.ToString());
            SaveToCsv(GlobalVars.modFolder + "\\map\\definition.csv", sb.ToString());
        }
        public static void EditDefaultMap(string modPath, ref List<Rgb> rGBs)
        {
            Debug.WriteLine($"Editing Default.Map with max provs of {GlobalVars.numOfProvs + 1}");
            modPath = Path.Combine(modPath, "map", "default.map");
            if (!File.Exists(modPath))
                return;
            string[] lines;
            try
            {
                lines = File.ReadAllLines(modPath);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return;
            }
            for (var i = 0; i < lines.Length; i++)
            {
                if (!lines[i].Contains("max_provinces")) continue;
                lines[i] = $"max_provinces = {GlobalVars.numOfProvs + 1}";
                break;
            }

            var text = Util.ConvertArrayToString(lines);
            if (!File.Exists(modPath) || text == null)
                return;
            File.WriteAllText(modPath, text);
            //Debug.WriteLine($"Trying to save as this file {modPath} this value: {Util.ConvertArrayToString(lines)}");
            //ModifyTextFile(modPath, Util.ConvertArrayToString(lines));
        }

        public static void SaveLeadersToHistoryFile()
        {
            //Formatter.FormatAllRulerHistory();
            foreach (var leader in GlobalVars.formattedLeaders)
            {
                var item = Util.GetPathToTagCH(leader.Key);
                if (string.IsNullOrEmpty(item.fileLocation))
                {
                    Debug.WriteLine("Skipped");
                    continue;
                }
                var path = item.isInMod
                    ?  $"{Path.Combine(GlobalVars.modFolder, Path.Combine("history\\countries", item.fileLocation))}"
                    :  $"{Path.Combine(GlobalVars.vanillaFolder, Path.Combine("history\\countries", item.fileLocation))}";
                // Debug.WriteLine($"Reading history file from this path: {path}");
                var allLines = Util.StringToArray(Util.ReadFileWithAnsiEncoding(path));
                var entries = Parsing.GetCountriesHistoryEntries(allLines);
                var fileContent = Util.AddLeaderToCountryHistory(leader, entries.ToArray());
                if (path.Contains(GlobalVars.vanillaFolder)) // if this is not done the vanilla files would be overwritten
                    path = path.Replace(GlobalVars.vanillaFolder, GlobalVars.modFolder);
                WriteFileANSI(path,  fileContent);
                //Util.WriteLog(fileContent, $"{item.fileLocation}");
                OverrideTextFileANSI(path, fileContent);
            }
        }

        #endregion

    }
}
