using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    internal static class Parsing
    {
        /// <summary>
        /// get all paths to province files mapped by their province ID
        /// </summary>
        public static void GetALlProvincePaths()
        {
            Dictionary<int, string> temp = new();
            var provModPath = Path.Combine(GlobalVars.modFolder, "history", "provinces");
            var provVanillaPath = Path.Combine(GlobalVars.vanillaFolder, "history", "provinces");


            if (Directory.Exists(provModPath))
            {
                var fileNames = Directory.GetFiles(provModPath)
                    .Select(Path.GetFileName)
                    .ToArray();
                Util.WriteLog(PrintF.ArrayToString(fileNames), "debug");
                foreach (var file in fileNames)
                {
                    var match = Regex.Match(file.ToString(), @"(\d+)\s*-");
                    Debug.WriteLine($"MATCH: {match.Groups[1].Value } from {file}");
                    if (!match.Success) continue;
                    if (temp.ContainsKey(int.Parse(match.Groups[1].Value)))
                        continue;
                    temp.Add(int.Parse(match.Groups[1].Value), Path.Combine(provModPath, file));
                    Debug.WriteLine($"AddedL \t{int.Parse(match.Groups[1].Value)} -- {Path.Combine(provModPath, file)}");
                }
            }
            if (Directory.Exists(provVanillaPath))
            {
                var fileNames = Directory.GetFiles(provVanillaPath)
                    .Select(Path.GetFileName)
                    .ToArray();
                foreach (var file in fileNames)
                {
                    var match = Regex.Match(file, @"(\d+)\s*-");
                    if (!match.Success) continue;
                    if (temp.ContainsKey(int.Parse(match.Groups[1].Value)))
                        continue;
                    temp.Add(int.Parse(match.Groups[1].Value), Path.Combine(provVanillaPath, file));
                }
            }

            GlobalVars.provincePaths = temp;

            Util.WriteLog(PrintF.PrintDictionarySpacedTitle("Id to Province Path: ", temp), "IdToProvincePath");
        }
        /// <summary>
        /// Verifies if the files exist and prioritizes the mod file
        /// </summary>
        public static void RgbParseFiles()
        {
            if (GlobalVars.modFolder.EndsWith("\\mod"))
                return;
            try
            {
                if (File.Exists(GlobalVars.modFolder + "\\map\\definition.csv"))
                    ParseRgbColors(GlobalVars.modFolder + "\\map\\definition.csv");
                else
                    ParseRgbColors(GlobalVars.vanillaFolder + "\\map\\definition.csv");
            }
            catch (DirectoryNotFoundException exception)
            {
                Debug.WriteLine(exception.Message);
            }
        }
        /// <summary>
        /// Reads in all colors from the /map/definition.csv file and saves them globally
        /// </summary>
        /// <param name="filePath"></param>
        private static void ParseRgbColors(string filePath)
        {
            

            List<Rgb> rgbs = new();
            var lines = File.ReadAllLines(filePath);
            Debug.WriteLine($"Reading Definitions from this path: {filePath}");
            if (lines.Length == 0)
                return;

            var matches = Regex.Matches(Util.ConvertArrayToString(lines.ToArray()),
                @"\s*(?:(\d+);(\d+);(\d+);(\d+);).*");
            rgbs.AddRange(matches.Cast<Match>().Select(item => new Rgb(int.Parse(item.Groups[1].Value), 
                int.Parse(item.Groups[2].Value), int.Parse(item.Groups[3].Value), int.Parse(item.Groups[4].Value))));
            GlobalVars.colors = rgbs;
            File.WriteAllText(GlobalVars.appPath + "RGB_values.txt", Util.ConvertArrayToString(GlobalVars.colors.ToArray()));

            // Parsing list to hashmap
            Dictionary<int, RGB> temp = new ();
            foreach (var item in matches.Cast<Match>())
            {
                temp.Add(int.Parse(item.Groups[1].Value), new RGB(int.Parse(item.Groups[2].Value), int.Parse(item.Groups[3].Value), int.Parse(item.Groups[4].Value)));
            }
            GlobalVars.ColorsId = temp;
            Util.WriteLog(PrintF.PrintDictionarySpacedTitle("ProvinceIDs: ", temp), "ColorsToIdsHashed");
        }

        /// <summary>
        /// Only input common/country_tags/*.txt files!
        /// Parses the tags and country file paths from the given string and saves them globally
        /// </summary>
        /// <param name="fileContent"></param>
        public static void ParseTags(string fileContent)
        {
            var matches = Regex.Matches(fileContent, @"([A-Z]{3})\s+=\s+""(.*)""");
            foreach (var item in matches.Cast<Match>())
            {
                GlobalVars.tags.Add(new Tag(item.Groups[1].Value, item.Groups[2].Value));
            }
            Util.WriteLog(PrintF.DebugPrintListNewLinesS(GlobalVars.tags), "ParsedCoutries");
        }
        /// <summary>
        /// Parses all modifiers from the \\common\\event_modifiers\\02_test_modifiers.txt file
        /// </summary>
        public static void ParseModifiers()
        {
            var content = File.ReadAllText(GlobalVars.vanillaFolder + "\\common\\event_modifiers\\02_test_modifiers.txt");
            var matches = Regex.Matches(content, @"#\s{1,}(.*)\s=\s(.*)");
            foreach (var item in matches.Cast<Match>())
            {
                if (item.Groups[2].Value.Equals("yes"))
                    GlobalVars.modifiers.Add(new Modifier(item.Groups[1].Value, "boolean"));
                else if (item.Groups[2].Value.Contains("."))
                    GlobalVars.modifiers.Add(new Modifier(item.Groups[1].Value, "float"));
                else if (int.TryParse(item.Groups[2].Value, out _))
                    GlobalVars.modifiers.Add(new Modifier(item.Groups[1].Value, "integer"));
                else
                    GlobalVars.modifiers.Add(new Modifier(item.Groups[1].Value, "unknown"));
            }

            Util.WriteLog(PrintF.WriteListNewLines(GlobalVars.modifiers), "temp");
        }
        /// <summary>
        /// Parses all effects / triggers / scopes from the effects.txt file to data
        /// Could be expanded later on that's why there is this extra method
        /// </summary>
        public static void ParseEffects()
        {
            GlobalVars.effects = File.ReadAllLines(Path.Combine(GlobalVars.dataPath, "effects.txt")).ToList();
        }
        /// <summary>
        /// Parses Leaders from a given .csv file
        /// </summary>
        public static void ParseLeaderFromFile(string path)
        {
            var fileContent = Util.ReadAllTextCsv(path);
            if (fileContent == string.Empty)
            {
                Debug.WriteLine("Empty .csv");
                return;
            }
            GlobalVars.featuresWindow.LOutputBox.Text = "";
            GlobalVars.featuresWindow.LOutputBox.Text = fileContent;
            var matches = Regex.Matches(fileContent, @"(?<Tag>[A-Z]{3}|),(?<Doe>\d{1,4}.\d{1,2}.\d{1,2}|),(?<LType>[a-z]*|),(?<LName>.*?|),(?<Dynasty>.*?|),(?<Adm>\d{1,}|),(?<Dip>\d{1,}|),(?<Mil>\d{1,}|),(?<Birth>\d{1,4}.\d{1,2}.\d{1,2}|),(?<Age>\d{1,}|),(?<Death>\d{1,4}.\d{1,2}.\d{1,2}|),(?<Religion>.*?|),(?<Culture>.*?|),(?<regency>.*?|),(?<female>.*?|),(?<CoO>[A-Z]{3}|),(?<GType>[a-z]*|),(?<GName>.*?|),(?<Fire>\d{1,}|),(?<Shock>\d{1,}|),(?<Maneuver>\d{1,}|),(?<Siege>\d{1,}|)");
            
            Debug.WriteLine($"Reached Parsing with {matches.Count}");
            GlobalVars.featuresWindow.LeaderErrorBox.Text = $"Reached Parsing with {matches.Count}";


            foreach (var item in matches.Cast<Match>())
            {
                Debug.Write($"ITEM: {item.Value}");
                Leader leader = new();
                //TODO prevent nulls due to empty strings in ints
                leader.adm = int.Parse(item.Groups["Adm"].Value);
                leader.dip = int.Parse(item.Groups["Dip"].Value);
                leader.mil = int.Parse(item.Groups["Mil"].Value);
                leader.age = int.Parse(item.Groups["Age"].Value);
                leader.regency = GetBoolFromString(item.Groups["regency"].Value);
                leader.female = GetBoolFromString(item.Groups["female"].Value);
                leader.dynasty = item.Groups["Dynasty"].Value;
                leader.rulerName = item.Groups["LName"].Value;
                leader.tag = item.Groups["Tag"].Value;
                leader.countryOfOrigin = item.Groups["CoO"].Value;
                leader.typeOfRuler = item.Groups["LType"].Value;
                leader.religion = item.Groups["Religion"].Value;
                leader.culture = item.Groups["Culture"].Value;
                leader.dateOfEntry = GetValidTimeDateTime(item.Groups["Doe"].Value);
                if (GlobalVars.featuresWindow.UseAgeFromDate.Checked)
                {
                    leader.dateOfBirth = new DateTime(GlobalVars.defaultDate.Year - leader.age, 
                        GlobalVars.GlobalRandom.Next(1, 10), GlobalVars.GlobalRandom.Next(1, 31));
                }
                else
                    GetValidTimeDateTime(item.Groups["Birth"].Value);
                leader.dateOfDeath = item.Groups["Death"].Value.Equals(string.Empty) 
                    ? GlobalVars.defaultDate.AddYears(1) 
                    : GetValidTimeDateTime(item.Groups["Death"].Value);

                if (!item.Groups["GType"].Value.Equals(string.Empty))
                {
                    //Debug.WriteLine($"Fire Value: {item.Groups["Fire"].Value}");
                    leader.leaderName = item.Groups["GName"].Value;
                    leader.leaderType = item.Groups["GType"].Value;
                    leader.fire = int.Parse(item.Groups["Fire"].Value);
                    leader.shock = int.Parse(item.Groups["Shock"].Value);
                    leader.maneuver = int.Parse(item.Groups["Maneuver"].Value);
                    leader.siege = int.Parse(item.Groups["Siege"].Value);
                }
                
                Debug.WriteLine($"Leader: {leader}");

                GlobalVars.leaderList.Add(leader);

            }
        }
        /// <summary>
        /// Return the date that has been input or returns 1444.11.11 as default value. Does NOT return null
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static DateTime GetValidTimeDateTime(string input)
        {
            if (string.IsNullOrEmpty(input))
                return new DateTime(1444, 11, 11);

            const string format = "yyyy.M.d";
            if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out var result))
                return result;
            return new DateTime(1444, 11, 11);
        }
        /// <summary>
        /// returns the value of a bool that has been input and defaults to false.
        /// </summary>
        /// <returns></returns>
        private static bool GetBoolFromString(string input)
        {
            return !string.IsNullOrEmpty(input) && input.ToLower().Equals("yes");
        }

        public static List<string> GetCountriesHistoryEntries(IEnumerable<string> lines)
        {
            List<string> historyEntries = new();
            List<string> llines = new ();
            foreach (var line in lines)
            {
                if (!Regex.IsMatch(line, @"(\d{1,4}.\d{1,2}.\d{1,2}\s*=\s*{\s*)"))
                {
                    llines.Add(line);
                    continue;
                }
                historyEntries.Add(PrintF.ArrayToString(llines.ToArray()));
                llines.Clear();
                llines.Add(line);
            }
            historyEntries.Add(PrintF.ArrayToString(llines.ToArray()));
            Util.WriteLog(PrintF.HistoryToCheck(historyEntries.ToArray()), "history");
            return historyEntries;
        }

        public static void ParseImpassable()
        {
            GlobalVars.impassable.Clear();
            string path;
            path = Path.Combine(File.Exists(Path.Combine(GlobalVars.modFolder, "map", "climate.txt"))
                ? Path.Combine(GlobalVars.modFolder, "map", "climate.txt")
                : Path.Combine(GlobalVars.vanillaFolder, "map", "climate.txt"));
            if (!File.Exists(path))
                return;

            var impassableSection = Regex.Match(File.ReadAllText(path), "impassable = \\{[\\s\\S]*?\\}");
            if (!impassableSection.Success) 
                return;

            var ids = Regex.Matches(impassableSection.Value, "\\b\\d+\\b");
            foreach (var id in ids)
            {
                GlobalVars.impassable.Add(int.Parse(id.ToString()!));
            }
            GlobalVars.impassable.Add(int.Parse(GlobalVars.main.ProvIDInput.Text));
        }

        public static void GetMaxProvinces()
        {
            var path = Path.Combine(GlobalVars.modFolder, "map", "default.map");
            if (!File.Exists(path))
                return;
            try
            {
                var lines = File.ReadAllLines(path);
                Util.WriteLog(Util.ConvertArrayToString(lines), "colorsDebug");
                foreach (var line in lines)
                {
                    var match = Regex.Match(line, @"(?:\bmax_provinces\s+=\s+\b)(\d+)");
                    if (!match.Success) continue;
                    if (!int.TryParse(match.Groups[1].Value, out var maxProvinces)) continue;
                    //Util.ErrorPopUp("Number of Provinces", maxProvinces.ToString());
                    GlobalVars.numOfProvs = maxProvinces - 1;
                    Debug.WriteLine($"Max provinces according to default.map: {maxProvinces}");
                    return;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
