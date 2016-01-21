using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace SQLI_CrossAR.Utils
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string RayonSettingsKey = "radius_key";
        public static readonly int RayonDefault = 1000;

        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
            }
        }

        public static int Rayon
        {
            get
            {
                return AppSettings.GetValueOrDefault(RayonSettingsKey, RayonDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RayonSettingsKey, value);
            }
        }
    }
}
