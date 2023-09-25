using System;
using System.Collections.Generic;

namespace EU4_Province_Creator
{
    #region structs and formats

    public struct CountryHistory
    {
        public string tag = "";
        public string fileLocation = "";
        public bool isInMod = false;

        public CountryHistory(string tag, string fileLocation, bool isInMod)
        {
            this.tag = tag;
            this.fileLocation = fileLocation;
            this.isInMod = isInMod;
        }

        public readonly override string ToString()
        {
            return $"Tag: {tag}, file: {fileLocation,33}, is mod: {isInMod}";
        }
    }

    public struct Leader
    {
        public int adm = 0;
        public int dip = 0;
        public int mil = 0;
        public int shock = 0;
        public int fire = 0;
        public int maneuver = 0;
        public int siege = 0;
        public int age = 0;

        public bool regency = false;
        public bool female = false;
        
        public string tag = string.Empty;
        public string countryOfOrigin = string.Empty;
        public string typeOfRuler = string.Empty;
        public string rulerName = string.Empty;
        public string dynasty = string.Empty;
        public string leaderName = string.Empty;
        public string leaderType = string.Empty;
        public string religion = string.Empty;
        public string culture = string.Empty;

        public DateTime dateOfEntry = new(1444, 11, 1);
        public DateTime dateOfBirth = new (1430,11,11);
        public DateTime dateOfDeath = new (1445,11,11);
        public Leader()
        {

        }

        public readonly override string ToString()
        {
            return $"Tag: {tag}, name: {rulerName}, age: {age}, dynasty: {dynasty}, adm: {adm}, dip: {dip}, mil: {mil}, " +
                   $"female = {female}, regency = {regency}, " +
                   $"leader type: {leaderType}, leader name: {leaderName}," +
                   $" shock: {shock}, fire: {fire}, maneuver: {maneuver}, siege: {siege}";
        }
    }

    public struct RGB
    {

        public int r;
        public int g;
        public int b;

        public RGB(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public readonly override string ToString()
        {
            return $"{r};{g};{b};";
        }
        public readonly string ToStringFancy()
        {
            return $"Red: {r,3}; Green: {g,3}; Blue: {b,3};";
        }
    }
    public struct Rgb
    {
        public int r;
        public int g;
        public int b;
        public int prov;

        public Rgb(int prov, int r, int g, int b)
        {
            this.prov = prov;
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public readonly override string ToString()
        {
            return $"{prov};{r};{g};{b};";
        }
        public readonly string ToStringFancy()
        {
            return $"ID: {prov,4}; Red: {r,3}; Green: {g,3}; Blue: {b,3};";
        }
    }
    public struct FolderProfile
    {
        public string name;
        public string modFolder = "";
        public string localizationFolder = "";
        public string vanillaFolder = "";

        public FolderProfile(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return $"Profile name: {name}; Mod folder: {modFolder}; Localization folder: {localizationFolder}; Vanilla folder: {vanillaFolder}";
        }
    }
    public struct Vector2D
    {
        public int x;
        public int y;

        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public readonly override string ToString()
        {
            return $"X/Y: {x}/{y}";
        }
    }
    public struct Preset
    {
        public string cores = "";
        public string profileName = "";
        public string name = "";
        public string capital = "";
        public string provinceId = "";
        public string controller = "";
        public string owner = "";
        public string religion = "";
        public string culture = "";
        public string tradegood = "";
        public string centerOfTrade = "";
        public string minDev = "";
        public string maxDev = "";
        public string baseTax = "";
        public string baseProduction = "";
        public string baseManpower = "";

        public bool capitalAndName = false; 
        public bool city = false; 
        public bool hre = false; 
        public bool fort = false; 
        public bool coal = false; 
        public bool randomDevelopment = false; 
        public bool uncolonized = false;
        public bool coreCreatorOwner = false;

        public bool muslim = false;
        public bool eastern = false;
        public bool western = false;
        public bool centralAfrica = false;
        public bool eastAfrican = false;
        public bool westAfrican = false;
        public bool chinese = false;
        public bool nomadic = false;
        public bool indian = false;
        public bool polynesial = false;
        public bool aboriginal = false;
        public bool highAmerican = false;
        public bool northAmerica = false;
        public bool southAmerica = false;
        public bool mesoamerican = false;
        public bool andean = false;
        public bool ottoman = false;

        public Preset()
        {
        }
        public readonly override string ToString()
        {
            return $"Name: {profileName}";
        }
    }
    public struct Modifier
    {
        public string name;
        public string type;

        public Modifier(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
        public readonly string ToStringFancy()
        {
            return $"Name: {name,43}, type: {type,7}";
        }
        public readonly override string ToString()
        {
            return $"{name}";
        }
    }
    public struct Settings
    {
        public bool startNewProvId = true;
        public bool loadLastProfile = true;
        public bool saveSettingsOnClose = true;
        public bool loadFilesOnProfileSelect = true;
        public bool startWithNewRgb = false;

        public string lastProfile = "";

        public RulerSettings rulerSettings = new();
        public TagVerifierSettings tagVerifierSettings = new();
        public Settings()
        {

        }

        public readonly override string ToString()
        {
            return $"New ID: {startNewProvId}, load profile: {loadLastProfile}, profile: {lastProfile}, save settings: {saveSettingsOnClose}," +
                   $" load on select: {loadFilesOnProfileSelect}, start with new RGB: {startWithNewRgb}, ruler formatter settings: {rulerSettings}" +
                   $", tag verifier: {tagVerifierSettings}";
        }
    }
    public struct RulerSettings
    {
        public bool randomOrdinal = true;
        public bool copyVanillaToMod = true;
        public bool saveToCountryFile = true;
        public bool overrideFileData = false;

        public int min = 0;
        public int max = 4;
        public int fixedOrdinal = 0;
        public RulerSettings() { }

        public readonly override string ToString()
        {
            return $"random ordinal: {randomOrdinal}, min: {min}, max: {max}, fixed ordinal: {fixedOrdinal}, copy to mod: {copyVanillaToMod}," +
                   $" save to file: {saveToCountryFile}, override file data: {overrideFileData}";
        }
    }
    public struct Tag
    {
        public string tag;
        public string countryFile = "";

        public Tag(string tagI)
        {
            tag = tagI;
        }
        public Tag(string tagI, string countryFileI)
        {
            tag = tagI;
            countryFile = countryFileI;
        }
        public readonly override string ToString()
        {
            return $"Tag: {tag}; country file: {countryFile}";
        }
    }
    public struct TagVerifierSettings
    {
        public bool suggestTag = false;
        public TagVerifierSettings() { }
        public override string ToString()
        {
            return $"Suggest tag: {suggestTag}";
        }
    }
    #endregion
   
    public static class GlobalVars
    {
        #region Windows
        public static MainWindow main;
        public static ProvinceColorPicker provinceColorPicker;
        public static TagVerifier tagVerifier;
        public static DeleteProfileForm deleteProfileForm;
        public static RandomModifier randomModiferForm;
        public static RandomEffect randomEffect;
        public static PresetForm presetForm;
        public static WebBrowserForm webBrowserForm;
        public static Features featuresWindow;
        #endregion

        public static int numOfProvs = 0;

        public static string appPath = "";
        public static string dataPath = "";
        public static string userDataPath = "";
        public static string modFolder = "";
        public static string vanillaFolder = "";
        public static string buildProvinceData = "";
        public static string localizationsFile = "";
        public static string curProvHistory = "";

        public static DateTime defaultDate = new (1444, 1, 1);

        public static List<string> provinceFiles;
        public static Dictionary<int, string> provincePaths = new();
        public static List<string> effects;

        public static CreateProfileForm createProfileForm;

        public static List<Rgb> colors = new();
        public static Dictionary<int, RGB> ColorsId = new();

        public static readonly Random GlobalRandom = new();
        
        public static Dictionary<Vector2D, int> rulerChanceTableDic = new();

        public static List<CountryHistory> countryHistories = new();
        public static List<Leader> leaderList = new ();
        public static List<Preset> presets = new();
        public static List<Modifier> modifiers = new();
        public static List<Tag> tags = new (); //TODO read in the tags on startup
        public static Settings settings;
        public static List<KeyValuePair<string, string>> formattedLeaders = new();
        public static Dictionary<string, List<string>> formattedRulers = new();
        public static List<FolderProfile> folderProfiles = new();

        public static List<int> impassable = new ();
    }
}
