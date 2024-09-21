using System.Text.Json;
using System.Text.Json.Serialization;

namespace Kill_Sound_GoldKingZ.Config
{
    public static class Configs
    {
        public static class Shared {
            public static string? CookiesModule { get; set; }
        }
        
        private static readonly string ConfigDirectoryName = "config";
        private static readonly string ConfigFileName = "config.json";
        private static readonly string jsonFilePath = "Kill_Settings.json";
        private static readonly string jsonFilePath2 = "MySql_Settings.json";
        private static readonly string jsonFilePath3 = "Kill_Paths.json";
        private static string? _configFilePath;
        private static string? _jsonFilePath;
        private static string? _jsonFilePath2;
        private static string? _jsonFilePath3;
        private static ConfigData? _configData;

        private static readonly JsonSerializerOptions SerializationOptions = new()
        {
            Converters =
            {
                new JsonStringEnumConverter()
            },
            WriteIndented = true,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
        };

        public static bool IsLoaded()
        {
            return _configData is not null;
        }

        public static ConfigData GetConfigData()
        {
            if (_configData is null)
            {
                throw new Exception("Config not yet loaded.");
            }
            
            return _configData;
        }

        public static ConfigData Load(string modulePath)
        {
            var configFileDirectory = Path.Combine(modulePath, ConfigDirectoryName);
            if(!Directory.Exists(configFileDirectory))
            {
                Directory.CreateDirectory(configFileDirectory);
            }
            _jsonFilePath = Path.Combine(configFileDirectory, jsonFilePath);
            Helper.CreateDefaultWeaponsJson(_jsonFilePath);

            _jsonFilePath2 = Path.Combine(configFileDirectory, jsonFilePath2);
            Helper.CreateDefaultWeaponsJson2(_jsonFilePath2);

            _jsonFilePath3 = Path.Combine(configFileDirectory, jsonFilePath3);
            Helper.CreateDefaultWeaponsJson3(_jsonFilePath3);

            _configFilePath = Path.Combine(configFileDirectory, ConfigFileName);
            if (File.Exists(_configFilePath))
            {
                _configData = JsonSerializer.Deserialize<ConfigData>(File.ReadAllText(_configFilePath), SerializationOptions);
            }
            else
            {
                _configData = new ConfigData();
            }

            if (_configData is null)
            {
                throw new Exception("Failed to load configs.");
            }

            SaveConfigData(_configData);
            
            return _configData;
        }

        private static void SaveConfigData(ConfigData configData)
        {
            if (_configFilePath is null)
            {
                throw new Exception("Config not yet loaded.");
            }
            string json = JsonSerializer.Serialize(configData, SerializationOptions);

            json = "// Note: To Use Modify Version And Lower Volume \n// Download https://github.com/Source2ZE/MultiAddonManager  With Gold KingZ WorkShop \n// https://steamcommunity.com/sharedfiles/filedetails/?id=3230015783\n// mm_extra_addons 3230015783\n// OtherWise Use Normal Sounds https://github.com/oqyh/cs2-Kill-Sound-GoldKingZ/blob/main/sounds/sounds.txt \n\n" + json;

            File.WriteAllText(_configFilePath, json);
        }

        public class ConfigData
        {
            public bool KS_EnableQuakeSounds { get; set; }
            public bool KS_DisableQuakeSoundsOnWarmUp { get; set; }
            public bool KS_ResetKillStreakOnEveryRound { get; set; }
            public bool KS_UseMySql { get; set; }
            
            public string empty { get; set; }
            public bool KS_AddMenu_FreezeOnOpenMenu { get; set; }
            public string KS_AddMenu_HeadShotKillSoundPath { get; set; }
            
            public string KS_AddMenu_BodyKillSoundPath { get; set; }
            
            public string KS_AddMenu_HeadShotHitSoundPath { get; set; }
            
            public string KS_AddMenu_BodyHitSoundPath { get; set; }
            public bool KS_AddMenu_QuakeSoundsToggle { get; set; }
            public bool KS_AddMenu_QuakeCenterMessageToggle { get; set; }
            public bool KS_AddMenu_QuakeChatMessageToggle { get; set; }
            
            public string empty2 { get; set; }
            public bool KS_DefaultValue_FreezeOnOpenMenu { get; set; }
            public bool KS_DefaultValue_HeadShotKillSound { get; set; }
            public bool KS_DefaultValue_BodyKillSound { get; set; }
            public bool KS_DefaultValue_HeadShotHitSound { get; set; }
            public bool KS_DefaultValue_BodyHitSound { get; set; }
            public string KS_InGameMenu { get; set; }
            public string KS_OnlyAllowTheseGroupsToToggle { get; set; }
            public int KS_AutoRemovePlayerCookieOlderThanXDays { get; set; }
            public string empty3 { get; set; }
            public string Information_For_You_Dont_Delete_it { get; set; }
            
            public ConfigData()
            {
                KS_EnableQuakeSounds = false;
                KS_DisableQuakeSoundsOnWarmUp = true;
                KS_ResetKillStreakOnEveryRound = true;
                KS_UseMySql = false;
                empty = "-----------------------------------------------------------------------------------";
                KS_AddMenu_FreezeOnOpenMenu = true;
                KS_AddMenu_HeadShotKillSoundPath = "sounds/GoldKingZ/Training/bell_normal.vsnd_c";
                KS_AddMenu_BodyKillSoundPath = "sounds/GoldKingZ/Training/timer_bell.vsnd_c";
                KS_AddMenu_HeadShotHitSoundPath = "sounds/GoldKingZ/Training/bell_impact.vsnd_c";
                KS_AddMenu_BodyHitSoundPath = "sounds/GoldKingZ/Training/timer_bell.vsnd_c";
                KS_AddMenu_QuakeSoundsToggle = true;
                KS_AddMenu_QuakeCenterMessageToggle = true;
                KS_AddMenu_QuakeChatMessageToggle = true;
                empty2 = "-----------------------------------------------------------------------------------";
                KS_DefaultValue_FreezeOnOpenMenu = true;
                KS_DefaultValue_HeadShotKillSound = true;
                KS_DefaultValue_BodyKillSound = false;
                KS_DefaultValue_HeadShotHitSound = false;
                KS_DefaultValue_BodyHitSound = false;
                KS_InGameMenu = "!quake,!quakesounds,!soundmenu,!soundsmenu,!menusound,!menusounds,!soundsettings,!soundsetting";
                KS_OnlyAllowTheseGroupsToToggle = "";
                KS_AutoRemovePlayerCookieOlderThanXDays = 7;
                empty3 = "-----------------------------------------------------------------------------------";
                Information_For_You_Dont_Delete_it = " Vist  [https://github.com/oqyh/cs2-Kill-Sound-GoldKingZ/tree/main?tab=readme-ov-file#-configuration-] To Understand All Above";
            }
        }
    }
}