using System;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using Ervean.Utilities.SaveLoad;

namespace Ervean.Utilities.SaveLoad
{
    /// <summary>
    /// A singleton containing the user's confirguation before or during runtime
    /// </summary>
    public class UserConfiguration : UserConfigurationBase
    {
#if UNITY_WEBGL

        [DllImport("__Internal")]
        private static extern void SyncFiles();

        [DllImport("__Internal")]
        private static extern void WindowAlert(string message);

#endif

        /// <summary>
        /// Location of Configuration file located in streamingAssets folder, located in persistentDataPath for Android
        /// </summary>
#if UNITY_ANDROID || PLATFORM_LUMIN || UNITY_LUMIN
    private readonly static string ConfigurationFile = Path.Combine(GetDirectory() ,"config.ini");
#elif UNITY_WEBGL// && !UNITY_EDITOR
        private static readonly string ConfigurationFile = Path.Combine(GetDirectory() ,"config.ini");
#elif UNITY_WSA_10_0
    private static readonly string ConfigurationFile = Path.Combine(GetDirectory() ,"config.ini");
#elif UNITY_EDITOR
    private readonly static string ConfigurationFile =  Path.Combine(GetDirectory() ,"config.ini");
#else
    private readonly static string ConfigurationFile = Path.Combine(GetDirectory() ,"config.ini");
#endif

        #region Configuration Values

        public UserConfigurationData configurationData = new UserConfigurationData();

        #endregion Configuration Values

        /// <summary>
        /// Lazy singleton instance
        /// </summary>
        private static readonly Lazy<UserConfiguration> lazy = new Lazy<UserConfiguration>(() => new UserConfiguration());

        /// <summary>
        /// Get the User Configuration instance
        /// </summary>
        public static UserConfiguration Instance
        { get { return lazy.Value; } }

        private UserConfiguration()
        {
            #region Default Values

            configurationData = CreateNewUserConfiguration();

            #endregion Default Values

            Load();
        }

        public override void Load()
        {
            if (!Directory.Exists(GetDirectory()))
            {
                Directory.CreateDirectory(GetDirectory());
            }

#if UNITY_WEBGL //&& !UNITY_EDITOR
            configurationData = new UserConfigurationData();
            string dataPath = ConfigurationFile;

            try
            {
                if (File.Exists(dataPath))
                {
                    INIParser ini = new INIParser();
                    ini.Open(dataPath);
                    ReadValues(ini, configurationData);
                    ini.Close();

                    Debug.Log("UserConfiguration loaded::\n" + ToString());
                }
                else
                {
                    Debug.LogError("UserConfiguration::Load -> " + ConfigurationFile + " did not exist");

                    Save();
                }
            }
            catch (Exception e)
            {
                PlatformSafeMessage("Failed to Load: " + e.Message);
            }

#else
        INIParser ini = new INIParser();
        if (!File.Exists(ConfigurationFile))
        {
            File.Create(ConfigurationFile).Dispose();
            ini.Open(ConfigurationFile);
            WriteValues(ini, configurationData);
            ini.Close();
        }

        ini.Open(ConfigurationFile);

        // Connection

        ReadValues(ini, configurationData);
        ini.Close();

        //Debug.Log("UserConfiguration loaded::\n" + ToString());
#endif
        }

        /// <summary>
        /// Save the user configuration to the file
        /// </summary>
        public override void Save()
        {
#if UNITY_WEBGL //&& !UNITY_EDITOR
            string dataPath = ConfigurationFile;

            try
            {
                if (File.Exists(dataPath))
                {
                    INIParser ini = new INIParser();
                    ini.Open(dataPath);

                    WriteValues(ini, configurationData);

                    ini.Close();
                }
                else
                {
                    Debug.LogError("UserConfiguration:: Save -> " + ConfigurationFile + " did not exist, creating one now");

                    File.Create(ConfigurationFile).Dispose();
                    configurationData = CreateNewUserConfiguration();
                    Save();
                    return;
                }

                if (Application.platform == RuntimePlatform.WebGLPlayer)
                {
                    SyncFiles();
                }
            }
            catch (Exception e)
            {
                PlatformSafeMessage("Failed to Save: " + e.Message);
            }
#else
        INIParser ini = new INIParser();
        ini.Open(ConfigurationFile);

        WriteValues(ini, configurationData);

        ini.Close();
#endif
        }


        public override string ToString()
        {
            return "";
        }

        private static void PlatformSafeMessage(string message)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
#if UNITY_WEBGL
                WindowAlert(message);
#endif
            }
            else
            {
                Debug.Log(message);
            }
        }

        private UserConfigurationData CreateNewUserConfiguration()
        {
            UserConfigurationData result = new UserConfigurationData();
            return result;
        }

        private void WriteValues(INIParser ini, UserConfigurationData configurationData)
        {

        }

        private void ReadValues(INIParser ini, UserConfigurationData configurationData)
        {
          


        }

    }
}