using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using File = System.IO.File;

namespace EU4_Province_Creator
{
    internal static class Loading
    {
        /// <summary>
        /// Loads and / or verifies all data use by the application
        /// </summary>
        public static void LoadData()
        {
            LoadSettings();
            LoadFolderProfiles();
            LoadModifiers();
            LoadPresetsFromJson();
            LoadEffects();

        }

        /// <summary>
        /// Deserializes the settings from json and sets the settings of the MAIN WINDOW.
        /// </summary>
        private static void LoadSettings()
        {
            var json = File.ReadAllText(GlobalVars.userDataPath + "\\Settings.json");
            GlobalVars.settings = JsonConvert.DeserializeObject<Settings>(json);

            GlobalVars.main.StartWithNewId.Checked = GlobalVars.settings.startNewProvId;
            GlobalVars.main.LoadProfileOnLaunch.Checked = GlobalVars.settings.loadLastProfile;
            GlobalVars.main.LoadFilesOnProfileSelect.Checked = GlobalVars.settings.loadFilesOnProfileSelect;
            GlobalVars.main.SaveSettingsOnClose.Checked = GlobalVars.settings.saveSettingsOnClose;
            GlobalVars.main.StartWithNewRgb.Checked = GlobalVars.settings.startWithNewRgb;
        }
        public static void LoadFolderProfiles()
        {
            var json = File.ReadAllText(GlobalVars.userDataPath + "\\FolderProfiles.json");
            GlobalVars.folderProfiles = JsonConvert.DeserializeObject<List<FolderProfile>>(json);
        }
        /// <summary>
        /// Loads the file names and directories 
        /// </summary>
        public static void LoadCountryHistoriesNames()
        {
            if (!Directory.Exists(GlobalVars.modFolder + "\\history\\countries"))
                Directory.CreateDirectory(GlobalVars.modFolder + "\\history\\countries");
            var files = Directory.GetFiles(GlobalVars.modFolder + "\\history\\countries");
            if (files.Length > 1)
            {
                foreach (var file in files)
                {
                    var match = Regex.Match(file, @"(([A-Z]{3})\s*-\s*.*)");
                    GlobalVars.countryHistories.Add(
                        new CountryHistory(match.Groups[2].Value, match.Groups[1].Value, true));
                }
                Debug.WriteLine("Finished mod files");
            }
            
            if (!Directory.Exists(GlobalVars.vanillaFolder + "\\history\\countries"))
                return;
            files = Directory.GetFiles(GlobalVars.vanillaFolder + "\\history\\countries");
            if (files.Length <= 1) return;
            {
                foreach (var file in files)
                {
                    var match = Regex.Match(file, @"(([A-Z]{3})\s*-\s*.*)");
                    GlobalVars.countryHistories.Add(
                        new CountryHistory(match.Groups[2].Value, match.Groups[1].Value, false));
                }
                Debug.WriteLine("Finished vanilla files");
                Util.WriteLog(PrintF.PrintListNewLinesS(GlobalVars.countryHistories), "CountryHistories");
            }
        }
        /// <summary>
        /// Collects all possible files and parses them to Tags
        /// </summary>
        public static void LoadCountriesAndTags()
        {
            if (!File.Exists(GlobalVars.modFolder + "\\common\\country_tags\\00_countries.txt"))
                Parsing.ParseTags(File.ReadAllText(GlobalVars.vanillaFolder+ "\\common\\country_tags\\00_countries.txt"));
            if (!Directory.Exists(GlobalVars.modFolder + "\\common\\country_tags"))
                return;
            var files = Directory.GetFiles(GlobalVars.modFolder + "\\common\\country_tags");
            if (files.Length < 1)
                return;
            foreach (var item in files)
            {
                Parsing.ParseTags(File.ReadAllText(item));
            }
        }
        /// <summary>
        /// Loads all modifiers from the \\common\\event_modifiers\\02_test_modifiers.txt file if the file exists
        /// </summary>
        public static void LoadModifiers()
        {
            if (!File.Exists(GlobalVars.vanillaFolder + "\\common\\event_modifiers\\02_test_modifiers.txt"))
            {
                //Debug.WriteLine($"Cant find file: {GlobalVars.vanillaFolder}\\common\\event_modifiers\\02_test_modifiers.txt");
                return;
            }
            //Debug.WriteLine("Loading Modifiers");
            Parsing.ParseModifiers();
        }
        /// <summary>
        /// reads in file from @howard with all effects but also some triggers and scopes inside
        /// TODO sort this out and split it up to categories or different features
        /// </summary>
        private static void LoadEffects()
        {
            if (!File.Exists(Path.Combine(GlobalVars.dataPath, "effects.txt")))
                return;
            Parsing.ParseEffects();
        }
        /// <summary>
        /// Loads all saved presets from .json file and puts them to the GlobalVars list
        /// To load a preset call GlobalVars.presetForm.SetDataFromPreset
        /// </summary>
        private static void LoadPresetsFromJson()
        {
            if (!File.Exists(GlobalVars.userDataPath + "\\UserPresets.json"))
                return;
            var json = File.ReadAllText(GlobalVars.userDataPath + "\\UserPresets.json");
            GlobalVars.presets = JsonConvert.DeserializeObject<List<Preset>>(json);
        }
        
    }
}
