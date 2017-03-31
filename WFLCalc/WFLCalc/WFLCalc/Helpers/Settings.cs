// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Diagnostics;

namespace WFLCalc.Helpers
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

        private const string UnitsKey = "units";
        private static readonly Unit UnitsDefault = Unit.Kilometers;

        #endregion

        public static Unit CurrentUnits
        {
            get
            {
                return (Unit)AppSettings.GetValueOrDefault<int>(UnitsKey, (int)UnitsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<int>(UnitsKey, (int)value);
            }
        }
    }
}