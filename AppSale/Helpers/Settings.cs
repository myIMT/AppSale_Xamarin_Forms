// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AppSale.Helpers
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

        #region Setting Constants

        #region UserID Constants
        const string UserIdKey = "userid";
        static readonly string UserIdDefault = string.Empty;
        const string AuthTokenKey = "authtoken";
        static readonly string AuthTokenDefault = string.Empty;
      #endregion

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        // Initial favourites set
        private const string InitFavSetKey = "Init_Fav_Set_key";
        private static readonly bool InitFavSetKeyDefault = true;

        #endregion


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

        public static bool InitFavSet
        {
            get
            {
                return AppSettings.GetValueOrDefault(InitFavSetKey, InitFavSetKeyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(InitFavSetKey, value);
            }
        }

        #region UserID Methods
        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value); }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserIdKey, value); }
        } 
        #endregion
    }
}