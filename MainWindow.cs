using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Markdig;
using ToolTip = System.Windows.Forms.ToolTip;

namespace EU4_Province_Creator;

public partial class MainWindow : Form
{
    private bool addedNewRgb;
    private Rgb newRrg;

    public MainWindow()
    {
        InitializeComponent();
        GenerateToolTips();

        GlobalVars.main = this;

        GlobalVars.appPath = Application.ExecutablePath.Replace("Province File Creator 2.0.exe", "");
        GlobalVars.dataPath = Path.Combine(GlobalVars.appPath, "Data");
        GlobalVars.userDataPath = Path.Combine(GlobalVars.appPath, "User");
        GlobalVars.modFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                               "\\Paradox Interactive\\Europa Universalis IV\\mod\\";
        GlobalVars.localizationsFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                                       "\\Paradox Interactive\\Europa Universalis IV\\mod\\";
        GlobalVars.localizationsFile = NewLocFileNameInput.Text;

        ModFolderPathLabel.Text = GlobalVars.modFolder;
        LocalisationPathLabel.Text = GlobalVars.localizationsFile;
        VanillaFolderPathLabel.Text = GlobalVars.vanillaFolder;

        DebugAndTest();
        Loading.LoadData();
        Util.UpdateAllFolderProfiles();
        Util.UpdateAllPresetBoxes();
        if (StartWithNewRgb.Checked)
            GenerateColor();

        GenerateColor();
    }
    public void UpdateFolderProfiles()
    {
        FolderProfileBox.Items.Clear();
        var objects = Array.ConvertAll(FPM.GetFolderProfileNames(), item => (object)item);
        //PrintF.PrintListNewLines(objects.ToList());
        if (objects.Any())
            FolderProfileBox.Items.AddRange(objects);
        if (LoadProfileOnLaunch.Checked && FolderProfileBox.Items.Contains(GlobalVars.settings.lastProfile))
            FolderProfileBox.SelectedItem = GlobalVars.settings.lastProfile;
        //TODO load files on profile select
    }
    public void UpdatePresets()
    {
        PresetBox.Items.Clear();
        if (GlobalVars.presets == null || GlobalVars.presets.Count == 0)
            return;
        var objects = GlobalVars.presets.Select(obj => obj.profileName).ToList();
        if (objects.Any())
            PresetBox.Items.AddRange(objects.ToArray());
    }
    private void GenerateToolTips()
    {
        var rbgTooltip = new ToolTip();
        rbgTooltip.SetToolTip(NewRBGButton,
            "In theory this can take an infinite amount of time, \nbut it is quite unlikely in this case");
        var definitionsTt = new ToolTip();
        definitionsTt.SetToolTip(editDefinitions,
            "Puts the new province ID as well as name, and the Rgb values into the Definitions.csv File");
        var defaultMapTt = new ToolTip();
        defaultMapTt.SetToolTip(editDefaultMapLabel, "Edits the Max. Provinces in the Default.map file.");
        var capitalNameTt = new ToolTip();
        capitalNameTt.SetToolTip(ProvNameCapitalName,
            "If checked, the capital name will always match the province name and vise versa.");
        var locFileNameTt = new ToolTip();
        locFileNameTt.SetToolTip(NewLocFileNameInput, "The name of the localization file to be used");
    }
    private void ToErrorLog(string message)
    {
        ErrorLog.ForeColor = Color.Red;
        ErrorLog.Text += message;
    }
    private void DebugAndTest()
    {
        ProvNameTextInput.Text = "";
        ProvIDInput.Text = "";
        CoresTagInput.Text = "";
        ControllerInput.Text = "";
        OwnerInput.Text = "";
        ReligionList.Text = "";
        CultureInput.Text = "";
        UseRandomDev.Checked = true;
        MinDevInput.Text = "6";
        MaxDevInput.Text = "11";
        BaseTaxInput.Text = "1";
        BaseProductionInput.Text = "1";
        BaseManpowerInput.Text = "1";
        TradegoodsList.Text = "";
        CenterOfTradeLevelInput.Text = "0";
        IsFortCheckbox.Checked = false;
    }
    /// <summary>
    /// Generating or Appending to the Localization GlobalVars.provinceFiles of the game and giving feedback on wrong inputs 
    /// </summary>
    private void ProvinceLocalization()
    {
        GlobalVars.localizationsFile = LocalisationPathLabel.Text;
        var path = Path.Combine(GlobalVars.main.LocalisationPathLabel.Text, GlobalVars.main.NewLocFileNameInput.Text + ".yml");
        if (File.Exists(path))
            try
            {
                using var fileStream = new FileStream(path, FileMode.Append, FileAccess.Write);
                using var writer = new StreamWriter(fileStream, new UTF8Encoding(true));
                writer.Write("\n PROV" + ProvIDInput.Text + ":0 \"" + ProvNameTextInput.Text + "\"\n" +
                             " PROV_ADJ" + ProvIDInput.Text + ":0 \"" + ProvNameTextInput.Text + "\"");
            }
            catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
            {
                Console.WriteLine("Error: File not Found: " + e.Message + "\n" + e.StackTrace);
                ToErrorLog("Error: File not Found!");
            }
            catch (Exception e) when (e is PathTooLongException)
            {
                Console.WriteLine("Error: Path to long: " + e.Message + "\n" + e.StackTrace);
                ToErrorLog("Error: Path to long!");
            }
            catch (Exception e) when (e is UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Unauthorized Access: " + e.Message + "\n" + e.StackTrace);
                ToErrorLog("Error: Unauthorized Access!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
        else
            try
            {
                using var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                using var writer = new StreamWriter(fileStream, new UTF8Encoding(true));
                writer.Write("l_english:\n" + " PROV" + ProvIDInput.Text + ":0 \"" + ProvNameTextInput.Text +
                             "\"\n" + " PROV_ADJ" + ProvIDInput.Text + ":0 \"" + ProvNameTextInput.Text + "\"");
            }
            catch (Exception e) when (e is FileNotFoundException or DirectoryNotFoundException)
            {
                Console.WriteLine("Error: File not Found: " + e.Message + "\n" + e.StackTrace);
                ToErrorLog("Error: File not Found!");
            }
            catch (Exception e) when (e is PathTooLongException)
            {
                Console.WriteLine("Error: Path to long: " + e.Message + "\n" + e.StackTrace);
                ToErrorLog("Error: Path to long!");
            }
            catch (Exception e) when (e is UnauthorizedAccessException)
            {
                Console.WriteLine("Error: Unauthorized Access: " + e.Message + "\n" + e.StackTrace);
                ToErrorLog("Error: Unauthorized Access!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
    }
    private void FolderSelectionButton_Click(object sender, EventArgs e)
    {
        GlobalVars.modFolder = Util.FolderFileDialog();
        ModFolderPathLabel.Text = GlobalVars.modFolder;
    }

    private void SelectLocalizationFolder_Click(object sender, EventArgs e)
    {
        GlobalVars.localizationsFile = Util.FolderFileDialog();
        LocalisationPathLabel.Text = GlobalVars.localizationsFile;
    }
    private void OutputPreview_TextChanged(object sender, EventArgs e)
    {
        GlobalVars.buildProvinceData = OutputPreview.Text;
    }

    private void PreviewButton_Click(object sender, EventArgs e)
    {
        ErrorLog.Text = "";
        BuildProvinceData();
    }


    // Builds all province data into a single string to display and save
    private void BuildProvinceData()
    {

        if (IsWasteland.Checked)
        {
            GlobalVars.buildProvinceData = "";
            GlobalVars.buildProvinceData += ProvNameGen() + "\r\n";
            GlobalVars.buildProvinceData += DiscoveredByTechGroup() + "\r\n";

            OutputPreview.Text = GlobalVars.buildProvinceData;
            return;
        }
        GlobalVars.buildProvinceData = "";
        GlobalVars.buildProvinceData += ProvNameGen() + "\r\n";
        GlobalVars.buildProvinceData += ControllerGen() + "\r\n";
        GlobalVars.buildProvinceData += OwnerGen() + "\r\n";
        GlobalVars.buildProvinceData += CoreGen();
        GlobalVars.buildProvinceData += CultureGen() + "\r\n";
        GlobalVars.buildProvinceData += ReligionGen() + "\r\n";
        GlobalVars.buildProvinceData += HreGen() + "\r\n";
        GlobalVars.buildProvinceData += CityGen() + "\r\n";
        GlobalVars.buildProvinceData += CapitalGen() + "\r\n";
        GlobalVars.buildProvinceData += UserDefinedTradeGood() + "\r\n";
        if (UseRandomDev.Checked)
            GlobalVars.buildProvinceData += UseRandomDevelopment() + "\r\n";
        else
            GlobalVars.buildProvinceData += UseInputDefinedDevelopment() + "\r\n";
        GlobalVars.buildProvinceData += HasFort() + "\r\n";
        if (UseLatentTradeGood.Checked)
            GlobalVars.buildProvinceData += HasLatentTradeGood() + "\r\n";
        GlobalVars.buildProvinceData += CenterOfTradeLevel() + "\r\n";
        GlobalVars.buildProvinceData += DiscoveredByTechGroup() + "\r\n";

        OutputPreview.Text = GlobalVars.buildProvinceData;
    }

    private int ValidateProvince_ID()
    {
        try
        {
            return int.Parse(ProvIDInput.Text);
        }
        catch (Exception)
        {
            ToErrorLog("No Valid province id\n");
            return 9999;
        }
    }

    /// <summary>
    /// Collects all data from the inputs, build the province file from them, saves the file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CreateFileButton_Click(object sender, EventArgs e)
    {
        if (GlobalVars.colors.Contains(newRrg))
            GenerateColor();

        if (OverruleBox.Checked)
        {
            var id = int.Parse(ProvIDInput.Text);
            GlobalVars.numOfProvs += 1;
            GlobalVars.colors.Add(newRrg);
            GlobalVars.ColorsId.Add(id, new RGB(newRrg.r, newRrg.g, newRrg.b));
            if (EDITDEFINITION.Checked)
                Saving.EditDefinition(ref GlobalVars.colors, ProvNameTextInput.Text);
            if (EDITDEFAULTMAP.Checked && id >= GlobalVars.numOfProvs)
                Saving.EditDefaultMap(GlobalVars.modFolder, ref GlobalVars.colors);
            addedNewRgb = false;
            Debug.WriteLine("Edited Files");
            var path = $"{GlobalVars.modFolder}\\history\\provinces\\{ProvIDInput.Text} - {ProvNameTextInput.Text}.txt";
            Debug.WriteLine("Trying to create this province file: " + path);
            Saving.SaveTextFile(path, GlobalVars.buildProvinceData);
            ProvinceLocalization();
        }
        else
        {
            //Debug.WriteLine($"PROV ID RETURN: {VerifyProvinceId()}");
            if (USERGBBOX.Checked && VerifyProvinceId() == 1)
            {
                if (addedNewRgb == false)
                {
                    ErrorLog.Text = "Please generate a new Color before creating the file!";
                    return;
                }
                GlobalVars.colors.Add(newRrg);
                if (!GlobalVars.ColorsId.ContainsKey(int.Parse(ProvIDInput.Text)))
                {
                    GlobalVars.ColorsId.Add(int.Parse(ProvIDInput.Text), new RGB(newRrg.r, newRrg.g, newRrg.b));
                }
                else
                {
                    Debug.WriteLine("THis Province Id is already defined");
                }
                GlobalVars.numOfProvs += 1;
                if (EDITDEFINITION.Checked)
                    Saving.EditDefinition(ref GlobalVars.colors, ProvNameTextInput.Text);
                if (EDITDEFAULTMAP.Checked)
                    Saving.EditDefaultMap(GlobalVars.modFolder, ref GlobalVars.colors);
                addedNewRgb = false;
                Debug.WriteLine("Edited Files");
                ProvinceLocalization();
                var path1 = $"{GlobalVars.modFolder}\\history\\provinces\\{ProvIDInput.Text} - {ProvNameTextInput.Text}.txt";
                Debug.WriteLine("Trying to create this province file: " + path1);
                Saving.SaveTextFile(path1, GlobalVars.buildProvinceData);
            }
            var path = $"{GlobalVars.modFolder}\\history\\provinces\\{ProvIDInput.Text} - {ProvNameTextInput.Text}.txt";
            Debug.WriteLine("Trying to create this province file: " + path);
            Saving.SaveTextFile(path, GlobalVars.buildProvinceData);
            //File.WriteAllText(
            //GlobalVars.modFolder + "\\history\\provinces\\" + ValidateProvince_ID() + " - " + ProvNameTextInput.Text +
            //".txt", GlobalVars.buildProvinceData);
            CurrentMaxProvs.Text = $"Current number of provinces: {GlobalVars.numOfProvs}";
        }

        if (IsWasteland.Checked)
        {
            Saving.SaveClimate();
        }

        try
        {
            if (ValidateProvince_ID() == 0 || ValidateProvince_ID() >= 9999)
                ProvIDInput.Text = 9999.ToString();
            else if (ValidateProvince_ID() != 9999)
                ProvIDInput.Text = (int.Parse(ProvIDInput.Text) + 1).ToString();
        }
        catch (Exception)
        {
            ToErrorLog("No Valid province id\n");
        }
        
        GenerateColor();
    }

    // Forcing SameCCO
    private void SameCCOCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        if (SameCCOCheckbox.Checked && CoresTagInput.Text.Length == 3 &&
            SameCCOCheckbox.Checked && ControllerInput.Text.Length == 3 &&
            SameCCOCheckbox.Checked && OwnerInput.Text.Length == 3)
        {
            ControllerInput.Text = CoresTagInput.Text;
            OwnerInput.Text = CoresTagInput.Text;
        }
        else
        {
            ToErrorLog("Only enter one Tag per category.\r\n");
        }
    }

    private void CoresTagInput_TextChanged(object sender, EventArgs e)
    {
        switch (SameCCOCheckbox.Checked)
        {
            case true when CoresTagInput.Text.Length == 3:
                ControllerInput.Text = CoresTagInput.Text;
                OwnerInput.Text = CoresTagInput.Text;
                break;
            case true when CoresTagInput.Text.Length >= 3:
                ToErrorLog("To many Tags have cores to use CCO.\r\n");
                break;
        }
    }

    // Ensuring that Same CCO works
    private void ControllerInput_TextChanged(object sender, EventArgs e)
    {
        switch (SameCCOCheckbox.Checked)
        {
            case true when ControllerInput.Text.Length == 3:
                CoresTagInput.Text = ControllerInput.Text;
                OwnerInput.Text = ControllerInput.Text;
                break;
            case true when ControllerInput.Text.Length == 3:
                ToErrorLog("Only one TAG can have control\r\n");
                break;
        }
    }

    // Ensuring that Same CCO works
    private void OwnerInput_TextChanged(object sender, EventArgs e)
    {
        switch (SameCCOCheckbox.Checked)
        {
            case true when OwnerInput.Text.Length == 3:
                ControllerInput.Text = OwnerInput.Text;
                CoresTagInput.Text = OwnerInput.Text;
                break;
            case true when OwnerInput.Text.Length == 3:
                ToErrorLog("Only one Nation can be in crontroll of a province\r\n");
                break;
        }
    }

    private void IsUnColonised_CheckedChanged(object sender, EventArgs e)
    {
        if (IsUncolonised.Checked)
        {
            CoresTagInput.Enabled = false;
            CoresTagInput.Text = "-";
            ControllerInput.Enabled = false;
            ControllerInput.Text = "-";
            OwnerInput.Enabled = false;
            OwnerInput.Text = "-";
        }
        else
        {
            CoresTagInput.Enabled = true;
            CoresTagInput.Text = "";
            ControllerInput.Enabled = true;
            ControllerInput.Text = "";
            OwnerInput.Enabled = true;
            OwnerInput.Text = "";
        }
    }

    // Sets Values for European countries
    private void EuropeanPresetButton_Click(object sender, EventArgs e)
    {
        ProvNameTextInput.Text = "";
        ProvIDInput.Text = "0";
        CoresTagInput.Text = "";
        ControllerInput.Text = "";
        OwnerInput.Text = "";
        ReligionList.SelectedIndex = 0;
        CultureInput.Text = "";
        IsHRECheckbox.Checked = false;
        IsCityCheckbox.Checked = true;
        UseRandomDev.Checked = true;
        MinDevInput.Text = "7";
        MaxDevInput.Text = "20";
        BaseTaxInput.Text = "4";
        BaseProductionInput.Text = "6";
        BaseManpowerInput.Text = "6";
        TradegoodsList.Text = "grain";
        UseLatentTradeGood.Checked = false;
        CenterOfTradeLevelInput.Text = "0";
        IsFortCheckbox.Checked = false;
        WesternCheckbox.Checked = true;
        EasternCheckbox.Checked = true;
        EastAfricanCheckbox.Checked = false;
        MuslimCheckbox.Checked = true;
        AnatolianCheckbox.Checked = true;
        HighAmericanCheckbox.Checked = false;
        IndianCheckbox.Checked = false;
        ChineseCheckbox.Checked = false;
        NomadicCheckbox.Checked = false;
        MesoamericanCheckbox.Checked = false;
        CentralAfricanCheckbox.Checked = false;
        WestAfricanCheckbox.Checked = false;
        AndeanCheckbox.Checked = false;
        NorthAmericanCheckbox.Checked = false;
        SouthAmericanCheckbox.Checked = false;
        AboriginalCheckbox.Checked = false;
        PolynesialCheckbox.Checked = false;
        SameCCOCheckbox.Checked = false;
        IsUncolonised.Checked = false;
    }

    private void ResetValuesButton_Click(object sender, EventArgs e)
    {
        ProvNameTextInput.Text = "";
        ProvIDInput.Text = "0";
        CoresTagInput.Text = "0";
        ControllerInput.Text = "0";
        OwnerInput.Text = "0";
        ReligionList.SelectedIndex = 0;
        CultureInput.Text = "0";
        IsHRECheckbox.Checked = false;
        IsCityCheckbox.Checked = false;
        UseRandomDev.Checked = true;
        MinDevInput.Text = "5";
        MaxDevInput.Text = "12";
        BaseTaxInput.Text = "1";
        BaseProductionInput.Text = "1";
        BaseManpowerInput.Text = "1";
        TradegoodsList.Text = "grain";
        UseLatentTradeGood.Checked = false;
        CenterOfTradeLevelInput.Text = "0";
        IsFortCheckbox.Checked = false;
        WesternCheckbox.Checked = false;
        EasternCheckbox.Checked = false;
        EastAfricanCheckbox.Checked = false;
        MuslimCheckbox.Checked = false;
        AnatolianCheckbox.Checked = false;
        HighAmericanCheckbox.Checked = false;
        IndianCheckbox.Checked = false;
        ChineseCheckbox.Checked = false;
        NomadicCheckbox.Checked = false;
        MesoamericanCheckbox.Checked = false;
        CentralAfricanCheckbox.Checked = false;
        WestAfricanCheckbox.Checked = false;
        AndeanCheckbox.Checked = false;
        NorthAmericanCheckbox.Checked = false;
        SouthAmericanCheckbox.Checked = false;
        AboriginalCheckbox.Checked = false;
        PolynesialCheckbox.Checked = false;
        SameCCOCheckbox.Checked = false;
        IsUncolonised.Checked = false;
    }

    private void NewLocFileNameInput_TextChanged(object sender, EventArgs e)
    {
        GlobalVars.localizationsFile = NewLocFileNameInput.Text;
    }
    private void NewRBGButton_click(object sender, EventArgs e)
    {
        GenerateColor();
    }

    private void GenerateColor()
    {
        if (GlobalVars.numOfProvs < 1)
            return;
        addedNewRgb = true;
        newRrg = Util.GenerateRandomColor();
        rVal.Text = Convert.ToString(newRrg.r);
        gVal.Text = Convert.ToString(newRrg.g);
        bVal.Text = Convert.ToString(newRrg.b);
        SetExColorButton(newRrg);
    }

    private void CurrentFolderPath_TextChanged(object sender, EventArgs e)
    {
        Parsing.GetMaxProvinces();
        //folder_path = ModFolderPathLabel.Text;
        GlobalVars.modFolder = ModFolderPathLabel.Text;
        //Debug.WriteLine($"folder path: {GlobalVars.modFolder}"); //TODO
        Parsing.RgbParseFiles();
        CurrentMaxProvs.Text = $"Current number of provinces: {GlobalVars.numOfProvs}";
        if (StartWithNewId.Checked)
            ProvIDInput.Text = (GlobalVars.numOfProvs + 1).ToString();
    }

    private void CurrentLocalizationPath_TextChanged(object sender, EventArgs e)
    {
        GlobalVars.localizationsFile = LocalisationPathLabel.Text;
    }
    private void UserRgbBox_CheckedChanged(object sender, EventArgs e)
    {
        if (USERGBBOX.Checked)
        {
            EDITDEFINITION.Enabled = true;
            EDITDEFAULTMAP.Enabled = true;
            NewRBGButton.Enabled = true;
        }
        else
        {
            EDITDEFAULTMAP.Enabled = false;
            EDITDEFINITION.Enabled = false;
            NewRBGButton.Enabled = false;
        }
    }

    private void SetExColorButton(Rgb rGB)
    {
        //Debug.WriteLine($"Color: {rGB}");
        ColorEx.BackColor = Color.FromArgb(rGB.r, rGB.g, rGB.b);
        rVal.Text = rGB.r.ToString();
        gVal.Text = rGB.g.ToString();
        bVal.Text = rGB.b.ToString();
    }
    private void SetExColorButton(RGB rGB)
    {
        //Debug.WriteLine($"Color: {rGB}");
        ColorEx.BackColor = Color.FromArgb(rGB.r, rGB.g, rGB.b);
        rVal.Text = rGB.r.ToString();
        gVal.Text = rGB.g.ToString();
        bVal.Text = rGB.b.ToString();
    }

    private void SetValuesNull()
    {
        ProvNameTextInput.Text = "";
        CoresTagInput.Text = "";
        OwnerInput.Text = "";
        ControllerInput.Text = "";
        CultureInput.Text = "";
        ReligionList.SelectedIndex = 0;
        BaseTaxInput.Text = "1";
        BaseProductionInput.Text = "1";
        BaseManpowerInput.Text = "1";
        TradegoodsList.Text = "";
        CapitalName.Text = "";
        OutputPreview.Text = "";
    }

    private void ProvIDInput_TextChanged(object sender, EventArgs e)
    {
        LoadProvinceFile();
    }

    private void LoadProvinceFile()
    {
        if (ProvIDInput.Text.Equals(""))
        {
            ErrorLog.Text = "No Valid Province ID";
            SetValuesNull();
        }
        int provId;
        try
        {
            provId = int.Parse(ProvIDInput.Text);
            if (provId > GlobalVars.numOfProvs)
            {
                ErrorLog.Text = $"ProvinceID {provId}, exceeds the max {GlobalVars.numOfProvs}";
                //Debug.WriteLine($"Got here {provId}");
                SetValuesNull();
            }
        }
        catch (Exception)
        {
            ErrorLog.Text = "Exception, No Valid Province ID";
            return;
        }
        Debug.WriteLine($"---------------------");//\nParsed Province ID: {provId}
        if (provId > GlobalVars.numOfProvs || provId <= 0)
        {
            ErrorLog.Text = $"ProvinceID {provId}, exceeds the max {GlobalVars.numOfProvs}";
            SetExColorButton(new RGB(255, 255, 255));
            OutputPreview.Text = "";
            return;
        }
        try
        {
            Rgb provRgb = new(-1, -1, -1, -1);
            foreach (var rgb in GlobalVars.colors.Where(rgb => rgb.prov == provId))
            {
                provRgb = rgb;
                ErrorLog.Text = ($"Found Province ID: {provId} + color: {provRgb}");
            }

            if (provRgb.prov == -1)
            {
                SetExColorButton(new RGB(255, 255, 255));
                OutputPreview.Text = "";
                return;
            }

            Debug.WriteLine($"Colors Count {GlobalVars.colors.Count} - Color ID: {provRgb.prov} - Color: {provRgb.ToString()}");
            SetExColorButton(GlobalVars.ColorsId[provId]);
            //var provFile = File.ReadAllLines(Util.FindFileWithName(provId.ToString()));
            var provPath = GlobalVars.provincePaths[provId];
            ErrorLog.Text = ($"PROVINCEPATH: {provPath}");
            var provFile = File.ReadAllLines(provPath);
            //Debug.WriteLine($"File Content: {Util.ConvertArrayToString(provFile)}");
            var splitProv = PPF.RemoveHistory(provFile);
            var noHistory = splitProv[0];
            //Debug.WriteLine($"file data: {noHistory}");
            GlobalVars.curProvHistory = splitProv.Length > 1 ? splitProv[1] : "";
            //Debug.WriteLine(noHistory);
            ProvNameTextInput.Text = PPF.GetFirstLineOfArray(PPF.ConvertTextToArray(noHistory));
            CoresTagInput.Text = PPF.ParsePatternString(noHistory, "add_core\\s+=\\s+([a-zA-Z]{3})");
            OwnerInput.Text = PPF.ParsePatternString(noHistory, "owner\\s+=\\s+([a-zA-Z]{3})");
            ControllerInput.Text = PPF.ParsePatternString(noHistory, "controller\\s+=\\s+([a-zA-Z]{3})");
            CultureInput.Text = PPF.ParsePatternString(noHistory, "culture\\s+=\\s+([a-zA-Z_]+)");
            ReligionList.Text = PPF.ParsePatternString(noHistory, "religion\\s+=\\s+([a-zA-Z_]+)");
            BaseTaxInput.Text = PPF.ParsePatternString(noHistory, "base_tax\\s+=\\s+(\\d+)");
            BaseProductionInput.Text = PPF.ParsePatternString(noHistory, "base_production\\s+=\\s+(\\d+)");
            BaseManpowerInput.Text = PPF.ParsePatternString(noHistory, "base_manpower\\s+=\\s+(\\d+)");
            TradegoodsList.Text = PPF.ParsePatternString(noHistory, "trade_goods\\s+=\\s+([a-zA-Z_]+)");
            CapitalName.Text = PPF.ParsePatternString(noHistory, "capital\\s+=\\s+([a-zA-Z]+)");
            OutputPreview.Text = noHistory + GlobalVars.curProvHistory;
        }
        catch (Exception)
        {
            SetExColorButton(new RGB(255, 255, 255));
            OutputPreview.Text = "";
        }

        VerifyProvinceId();
    }

    public int VerifyProvinceId()
    {
        var provID = 0;
        var curMaxProv = 0;
        try
        {
            provID = int.Parse(ProvIDInput.Text);
            curMaxProv = GlobalVars.numOfProvs;
        }
        catch (Exception)
        {
            ErrorLog.Text = "Invalid ProvinceID or Files are not read in.";
        }

        if (provID <= curMaxProv + 1) return provID == curMaxProv + 1 ? 1 : 0;
        ErrorLog.Text = $"ProvinceID must be smaller or equal as {curMaxProv + 1}";
        return -1;

    }

    public string[] GetAllFileNames(string folderPath)
    {
        try
        {
            var fileNames = Directory.GetFiles(folderPath)
                .Select(Path.GetFileName)
                .ToArray();
            return fileNames;
        }
        catch (Exception)
        {
        }

        return new[] { "error" };
    }
    //GlobalVars.modFolder + "\\history\\provinces"
    private void ReadInFiles()
    {

        Parsing.GetALlProvincePaths();
        //string debugFile = "C:\\Users\\david\\source\\repos\\EU4 Province Creator\\EU4 Province Creator\\temp.txt";
        GlobalVars.provinceFiles = GetAllFileNames(Path.Combine(GlobalVars.modFolder, "history", "provinces")).ToList();
        for (var i = 0; i < GlobalVars.provinceFiles.Count; i++)
            GlobalVars.provinceFiles[i] = $"{Path.Combine(GlobalVars.modFolder, "history", "provinces")}\\history\\provinces\\" + GlobalVars.provinceFiles[i];
        var files2 = GetAllFileNames(Path.Combine(GlobalVars.vanillaFolder, "history", "provinces")).ToList();
        for (var i = 0; i < files2.Count; i++)
            files2[i] = $"{Path.Combine(GlobalVars.vanillaFolder, "history", "provinces")}" + files2[i];
        GlobalVars.provinceFiles.AddRange(files2);
        //File.WriteAllText(debugFile, ConvertArrayToString(GlobalVars.provinceFiles.ToArray()));

        if (!Directory.Exists(Path.Combine(GlobalVars.modFolder, "history")))
            return;
        //ErrorLog.Text = "Can't find \"map\" folder";
        if (!Directory.Exists(GlobalVars.modFolder + "\\history\\provinces"))
            return;
        //ErrorLog.Text = "Can't find shit \"definition.csv\" file";
        Parsing.GetMaxProvinces();
        Parsing.RgbParseFiles();
        Loading.LoadModifiers();
        Util.WriteLog(Formatter.ListToString(GlobalVars.provinceFiles), "provinceFiles");


        ErrorLog.ForeColor = Color.Green;
        ErrorLog.Text = "Loaded Files Correctly!";
        ErrorLog.ForeColor = Color.Black;


        //Debug.WriteLine($"Max Provs: {GlobalVars.numOfProvs}");
        CurrentMaxProvs.Text = $"Current number of provinces: {GlobalVars.numOfProvs}";
        if (StartWithNewId.Checked)
            ProvIDInput.Text = (GlobalVars.numOfProvs + 1).ToString();
        //if (StartWithNewId.Checked)
        //ProvIDInput.Text = (GlobalVars.colors.Count + 1).ToString();

        GenerateColor();
    }

    private void ReloadFilesButton_Click(object sender, EventArgs e)
    {
        ReadInFiles();

        CurrentMaxProvs.Text = $"Current number of provinces: {GlobalVars.numOfProvs}";
    }

    private void TextBox1_TextChanged(object sender, EventArgs e)
    {
        GlobalVars.vanillaFolder = VanillaFolderPathLabel.Text;
    }

    private void TextBox1_Enter(object sender, EventArgs e)
    {
        VanillaFolderPathLabel.BeginInvoke((MethodInvoker)delegate { VanillaFolderPathLabel.SelectAll(); });
    }

    private void CurrentFolderPath_Enter(object sender, EventArgs e)
    {
        ModFolderPathLabel.BeginInvoke((MethodInvoker)delegate { ModFolderPathLabel.SelectAll(); });
    }

    private void CurrentLocalizationPath_Enter(object sender, EventArgs e)
    {
        LocalisationPathLabel.BeginInvoke((MethodInvoker)delegate { LocalisationPathLabel.SelectAll(); });
    }
    private void VanillaFolder_Click(object sender, EventArgs e)
    {
        using var folderBrowserDialog = new FolderBrowserDialog();
        if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
        GlobalVars.vanillaFolder = folderBrowserDialog.SelectedPath;
        VanillaFolderPathLabel.Text = GlobalVars.vanillaFolder;
    }

    private void ProvNameTextInput_TextChanged(object sender, EventArgs e)
    {
        if (ProvNameCapitalName.Checked)
            CapitalName.Text = ProvNameTextInput.Text;
    }

    private void CapitalName_TextChanged(object sender, EventArgs e)
    {
        if (ProvNameCapitalName.Checked)
            ProvNameTextInput.Text = CapitalName.Text;
    }
    private void FolderProfileBox_SelectedValueChanged(object sender, EventArgs e)
    {
        Loading.LoadFolderProfiles();
        if (GlobalVars.folderProfiles == null || GlobalVars.folderProfiles.Count == 0)
            return;
        if (FolderProfileBox.SelectedItem == null && GlobalVars.settings.loadLastProfile)
        {
            ToErrorLog("No profiles found!");
            return;
        }

        var selectedValue = FolderProfileBox.SelectedItem.ToString();
        var profile = FPM.GetProfile(selectedValue, GlobalVars.folderProfiles);
        if (profile == null)
        {
            ToErrorLog("Invalid profile name --> profile was null");
            return;
        }

        ModFolderPathLabel.Text = profile?.modFolder;
        LocalisationPathLabel.Text = profile?.localizationFolder;
        VanillaFolderPathLabel.Text = profile?.vanillaFolder;

        // Load Files
        if (GlobalVars.settings.loadFilesOnProfileSelect)
            ReadInFiles();
    }
    private void ProvinceColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (GlobalVars.provinceColorPicker == null || GlobalVars.provinceColorPicker.IsDisposed)
        {
            GlobalVars.provinceColorPicker = new ProvinceColorPicker();
            GlobalVars.provinceColorPicker.Show();
        }
        else
        {
            GlobalVars.provinceColorPicker.BringToFront();
        }
    }

    private void TagVerifierToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (GlobalVars.modFolder == null || GlobalVars.vanillaFolder == null)
        {
            Util.ErrorPopUp("No folders found", "The mod and vanilla folder need to be selected and valid for this feature to work!");
            return;
        }

        if (!Directory.Exists(GlobalVars.vanillaFolder + "\\common\\country_tags")) //!Directory.Exists(GlobalVars.modFolder + "\\common\\country_tags") ||
        {
            Util.ErrorPopUp("No folders found", "The mod and vanilla folder need to contain the <country_tags> folders!\n" +
                                                "(For now I am to lazy to implement the automatic generation of those :))");
            return;
        }

        if (GlobalVars.tagVerifier == null || GlobalVars.tagVerifier.IsDisposed)
        {
            GlobalVars.tagVerifier = new TagVerifier();
            GlobalVars.tagVerifier.Show();
        }
        else
        {
            GlobalVars.tagVerifier.BringToFront();
        }
    }
    private void ProvIDInput_Enter(object sender, EventArgs e)
    {
        ProvIDInput.BeginInvoke((MethodInvoker)delegate { ProvIDInput.SelectAll(); });
    }

    private void CreateProfileToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (GlobalVars.createProfileForm == null || GlobalVars.createProfileForm.IsDisposed)
        {
            GlobalVars.createProfileForm = new CreateProfileForm();
            GlobalVars.createProfileForm.Show();
        }
        else
        {
            GlobalVars.createProfileForm.BringToFront();
        }
    }
    #region Generation of Province File

    public string ProvNameGen()
    {
        return "# " + ValidateProvince_ID() + " - " + ProvNameTextInput.Text;
    }

    public string CoreGen()
    {
        var temp = "";
        var tagFinder = new Regex("[A-Z]{3}");
        var parseTags = new Regex("(\\s?[A-Z]{3},?\\s?)*");

        if (parseTags.IsMatch(CoresTagInput.Text))
            temp = tagFinder.Matches(CoresTagInput.Text).Cast<Match>().
                Aggregate(temp, (current, tag) => current + ("add_core = " + tag.Value + "\r\n"));
        else switch (CoresTagInput.Text)
            {
                case "":
                case "-":
                    temp = "# add_core = ";
                    break;
            }
        return temp;
    }

    public string ControllerGen()
    {
        return ControllerInput.Text switch
        {
            "" or "-" => "# controller = ",
            _ => "controller = " + ControllerInput.Text,
        };
    }
    public string OwnerGen()
    {
        return OwnerInput.Text switch
        {
            "" or "-" => "# owner = ",
            _ => "owner = " + OwnerInput.Text,
        };
    }

    // Generating the culture from the user input
    public string CultureGen()
    {
        if (CultureInput.Text.Equals(""))
            return "# culture = ";
        return "culture = " + CultureInput.Text;
    }

    // Generating the capital from the user input
    public string CapitalGen()
    {
        return CapitalName.Text.Length > 0 ? $"capital = \"{CapitalName.Text}\"" : "";
    }

    // Generating the Religion from the religion list
    public string ReligionGen()
    {
        if (ReligionList.SelectedItem == null || ReligionList.SelectedItem.ToString().Equals("") ||
            ReligionList.SelectedItem.ToString().Equals("---"))
            return "# religion = ";
        return "religion = " + ReligionList.Text;
    }

    public string HreGen()
    {
        return IsHRECheckbox.Checked ? "hre = yes" : "# hre = yes";
    }

    public string CityGen()
    {
        return IsCityCheckbox.Checked ? "is_city = yes" : "";
    }

    //Tell the Random Development generator which numbers to use to what range and then call the generator. I have no Ideas What I did here to be honest
    public string UseRandomDevelopment()
    {
        var baseTax = 0;
        var baseProduction = 0;
        var baseManpower = 0;
        var totalDev = 3;
        if (MinDevInput.Equals("") || MaxDevInput.Equals(""))
            ToErrorLog("ERROR: Invalid Random Dev Input!\n");
        else
            try
            {
                totalDev = GlobalVars.GlobalRandom.Next(int.Parse(MinDevInput.Text), int.Parse(MaxDevInput.Text));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
                ToErrorLog("ERROR: Invalid Random Dev Input!\n");
            }

        if (totalDev < 3)
            totalDev = 3;
        var remainder = totalDev % 3;

        switch (remainder)
        {
            case 2:
                {
                    var tempRand = GlobalVars.GlobalRandom.Next(1, 3);
                    switch (tempRand)
                    {
                        case 1:
                            baseTax = CalculateDevCategories(0, totalDev, baseTax, baseProduction);
                            baseProduction = CalculateDevCategories(1, totalDev, baseTax, baseProduction);
                            baseManpower = CalculateDevCategories(2, totalDev, baseTax, baseProduction);
                            break;
                        case 2:
                            baseTax = CalculateDevCategories(0, totalDev, baseTax, baseProduction);
                            baseProduction = CalculateDevCategories(1, totalDev, baseTax, baseProduction);
                            baseManpower = CalculateDevCategories(2, totalDev, baseTax, baseProduction);
                            break;
                        default:
                            baseTax = CalculateDevCategories(0, totalDev, baseTax, baseProduction);
                            baseProduction = CalculateDevCategories(1, totalDev, baseTax, baseProduction);
                            baseManpower = CalculateDevCategories(2, totalDev, baseTax, baseProduction);
                            break;
                    }

                    break;
                }
            case 1 when GlobalVars.GlobalRandom.Next(1, 2) == 1:
                baseTax = CalculateDevCategories(0, totalDev, baseTax, baseProduction);
                baseProduction = CalculateDevCategories(1, totalDev, baseTax, baseProduction);
                baseManpower = CalculateDevCategories(2, totalDev, baseTax, baseProduction);
                break;
            case 1:
                baseTax = CalculateDevCategories(0, totalDev, baseTax, baseProduction);
                baseProduction = CalculateDevCategories(1, totalDev, baseTax, baseProduction);
                baseManpower = CalculateDevCategories(2, totalDev, baseTax, baseProduction);
                break;
            case 0 when totalDev > 3:
                baseTax = CalculateDevCategories(0, totalDev, baseTax, baseProduction);
                baseProduction = CalculateDevCategories(1, totalDev, baseTax, baseProduction);
                baseManpower = CalculateDevCategories(2, totalDev, baseTax, baseProduction);
                break;
            case 0 when true:
                baseTax = 1;
                baseProduction = 1;
                baseManpower = 1;
                break;
        }

        var outStr = "base_tax = " + baseProduction + "\r\nbase_production = " + baseManpower + "\r\nbase_manpower = " +
                     baseTax;

        return $"# Total development: {totalDev}\r\n" + outStr;
    }

    // Part of the Random dev Generator which actually generates the numbers via a equation for distributing and some semi hardcoded rules for the low development ranges --> prevents negative and null values
    public int CalculateDevCategories(int dev_category, int totalDev, int baseTax, int baseProduction)
    {
        int temp;
        switch (dev_category)
        {
            case 0:
                temp = totalDev switch
                {
                    6 => 3,
                    5 => 2,
                    4 => 1,
                    3 => 1,
                    _ => GlobalVars.GlobalRandom.Next(totalDev / 3, totalDev - totalDev / 5 * 3) + 1,
                };
                break;

            case 1:
                temp = totalDev switch
                {
                    6 => 2,
                    5 => 2,
                    4 => 1,
                    3 => 1,
                    _ => GlobalVars.GlobalRandom.Next((totalDev - baseTax) / 5 * 2,
                                                (totalDev - baseTax) / 5 * 2) + 1,
                };
                break;

            case 2:
                temp = totalDev - baseTax - baseProduction;
                break;
            default:
                return 1;
        }

        return temp;
    }

    public string UseInputDefinedDevelopment()
    {
        try
        {
            if (int.Parse(BaseTaxInput.Text) >= 1 && int.Parse(BaseProductionInput.Text) >= 1 &&
                int.Parse(BaseManpowerInput.Text) >= 1)
            {
                var totalDev = int.Parse(BaseTaxInput.Text) + int.Parse(BaseProductionInput.Text) +
                                int.Parse(BaseManpowerInput.Text);
                return $"#Total development: {totalDev}\r\nbase_tax = " + int.Parse(BaseTaxInput.Text) +
                       "\r\nbase_production = " + int.Parse(BaseProductionInput.Text) + "\r\nbase_manpower = " +
                       int.Parse(BaseManpowerInput.Text);
            }
        }
        catch (Exception e) when (e is FormatException)
        {
            ToErrorLog("Invalid Entry in Development");
        }

        return "#Total development: 3\r\nbase_tax = 1\r\nbase_production = 1\r\nbase_manpower = 1";
    }

    public string UserDefinedTradeGood()
    {
        if (TradegoodsList.SelectedItem == null || TradegoodsList.SelectedItem.ToString().Equals("") ||
            TradegoodsList.SelectedItem.ToString().Equals("---"))
            return "# trade_goods = ";
        return "trade_goods = " + TradegoodsList.Text;
    }
    // Generating Coal String
    public string HasLatentTradeGood()
    {
        return UseLatentTradeGood.Checked ? "latent_trade_goods = { coal }" : "";
    }

    // Generating CoT String
    public string CenterOfTradeLevel()
    {
        return int.Parse(CenterOfTradeLevelInput.Text) switch
        {
            3 => "center_of_trade = 3",
            2 => "center_of_trade = 2",
            1 => "center_of_trade = 1",
            _ => "# center_of_trade = "
        };
    }

    // Generating Fort String
    public string HasFort()
    {
        return IsFortCheckbox.Checked ? "fort_15th = yes" : "# fort_15th = yes";
    }

    // Generating Discovery String
    public string DiscoveredByTechGroup()
    {
        var temp = "";
        if (WesternCheckbox.Checked)
            temp += "discovered_by = ottoman\r\n";
        if (OttomanCheckbox.Checked)
            temp += "discovered_by = western\r\n";
        if (EasternCheckbox.Checked)
            temp += "discovered_by = eastern\r\n";
        if (MuslimCheckbox.Checked)
            temp += "discovered_by = muslim\r\n";
        if (AnatolianCheckbox.Checked)
            temp += "discovered_by = ottoman\r\n";
        if (HighAmericanCheckbox.Checked)
            temp += "discovered_by = high_american\r\n";
        if (IndianCheckbox.Checked)
            temp += "discovered_by = indian\r\n";
        if (ChineseCheckbox.Checked)
            temp += "discovered_by = chinese\r\n";
        if (NomadicCheckbox.Checked)
            temp += "discovered_by = nomad_group\r\n";
        if (MesoamericanCheckbox.Checked)
            temp += "discovered_by = mesoamerican\r\n";
        if (CentralAfricanCheckbox.Checked)
            temp += "discovered_by = central_african\r\n";
        if (EastAfricanCheckbox.Checked)
            temp += "discovered_by = east_african\r\n";
        if (WestAfricanCheckbox.Checked)
            temp += "discovered_by = sub_saharan\r\n";
        if (AndeanCheckbox.Checked)
            temp += "discovered_by = andean\r\n";
        if (NorthAmericanCheckbox.Checked)
            temp += "discovered_by = north_american\r\n";
        if (SouthAmericanCheckbox.Checked)
            temp += "discovered_by = south_american\r\n";
        if (AboriginalCheckbox.Checked)
            temp += "discovered_by = aboriginal_tech\r\n";
        if (PolynesialCheckbox.Checked)
            temp += "discovered_by = polynesian_tech\r\n";
        return temp;
    }

    #endregion

    #region All Set Methods for the GUI

    //TODO Discoveries

    #endregion

    private void MainWindowForm_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (SaveSettingsOnClose.Checked)
        {
            Util.GetSettings();
            Saving.SaveSettings();
        }
        Saving.SaverUserProfiles();
    }

    private void LoadFilesOnProfileSelect_CheckedChanged(object sender, EventArgs e)
    {
        if (StartWithNewId.Checked && !LoadFilesOnProfileSelect.Checked)
            StartWithNewId.Checked = false;
    }

    private void SearchBar_Click(object sender, EventArgs e)
    {
        if (SearchBar.Text != "Enter text here...")
        {
            SearchBar.ForeColor = SystemColors.WindowText;
            VanillaFolderPathLabel.BeginInvoke((MethodInvoker)delegate { SearchBar.SelectAll(); });
            return;
        }
        SearchBar.Text = string.Empty;
        SearchBar.ForeColor = SystemColors.WindowText;
    }

    private void SearchBar_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchBar.Text)) return;
        SearchBar.ForeColor = SystemColors.ScrollBar;
    }

    private void SearchBar_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) //TODO finish search bar
        {
            // Enter key was pressed
            // Perform search or any desired action
            // ...
        }
    }

    private void RandomEffect_Click(object sender, EventArgs e)
    {
        if (GlobalVars.randomEffect == null || GlobalVars.randomEffect.IsDisposed)
        {
            GlobalVars.randomEffect = new RandomEffect();
            GlobalVars.randomEffect.Show();
        }
        else
            GlobalVars.randomEffect.BringToFront();
    }

    private void FeatureWindow_Click(object sender, EventArgs e)
    {
        if (GlobalVars.featuresWindow == null || GlobalVars.featuresWindow.IsDisposed)
        {
            GlobalVars.featuresWindow = new Features();
            GlobalVars.featuresWindow.Show();
        }
        else
            GlobalVars.featuresWindow.BringToFront();
    }

    private void ReadmeHelp_Click(object sender, EventArgs e)
    {
        if (GlobalVars.webBrowserForm == null || GlobalVars.webBrowserForm.IsDisposed)
        {
            GlobalVars.webBrowserForm = new WebBrowserForm();
            GlobalVars.webBrowserForm.Show();
        }
        else
            GlobalVars.webBrowserForm.BringToFront();
        ConvertAndDisplayInWebBrowser(Path.Combine(GlobalVars.dataPath, "README.md"));
    }
    private static void ConvertAndDisplayInWebBrowser(string markdownFilePath)
    {
        // Read the Markdown content from the file
        var markdownContent = File.ReadAllText(markdownFilePath);

        // Convert Markdown to HTML using Markdig
        var htmlContent = Markdown.ToHtml(markdownContent);

        // Display the HTML content in the WebBrowser control
        GlobalVars.webBrowserForm.webBrowser.DocumentText = htmlContent;
    }
    private void DeleteFolderProfile(object sender, EventArgs e)
    {
        if (GlobalVars.deleteProfileForm == null || GlobalVars.deleteProfileForm.IsDisposed)
        {
            GlobalVars.deleteProfileForm = new DeleteProfileForm();
            GlobalVars.deleteProfileForm.Show();
        }
        else
            GlobalVars.deleteProfileForm.BringToFront();
    }

    private void LoadFilesOnProfileSelect_Click(object sender, EventArgs e)
    {
        StartWithNewRgb.Enabled = LoadFilesOnProfileSelect.Checked;
    }

    private void RandomModifier_Click(object sender, EventArgs e)
    {
        if (GlobalVars.randomModiferForm == null || GlobalVars.randomModiferForm.IsDisposed)
        {
            GlobalVars.randomModiferForm = new RandomModifier();
            GlobalVars.randomModiferForm.Show();
        }
        else
            GlobalVars.randomModiferForm.BringToFront();
    }

    private void CustomPresets_Click(object sender, EventArgs e)
    {
        if (GlobalVars.presetForm == null || GlobalVars.presetForm.IsDisposed)
        {
            GlobalVars.presetForm = new PresetForm();
            GlobalVars.presetForm.Show();
        }
        else
            GlobalVars.presetForm.BringToFront();
    }

    private void PresetBox_SelectedValueChanged(object sender, EventArgs e)
    {
        if (PresetBox.Text == null || GlobalVars.presets.All(profile => profile.profileName != PresetBox.Text))
            return;
        PresetForm.SetDataForSelectedPreset(PresetBox.Text);
    }

    private void NextProvButton_Click(object sender, EventArgs e)
    {
        int.TryParse(ProvIDInput.Text, out var provID);
        ProvIDInput.Text = $"{provID + 1}";
        LoadProvinceFile();
    }

    private void PrevProvButton_Click(object sender, EventArgs e)
    {
        int.TryParse(ProvIDInput.Text, out var provID);
        ProvIDInput.Text = $"{provID - 1}";
        LoadProvinceFile();
    }

    private void WastelandPresetOne_Click(object sender, EventArgs e)
    {

    }
}