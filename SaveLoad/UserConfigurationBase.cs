using System;
using System.IO;
using UnityEngine;

namespace Ervean.Utilities.SaveLoad
{
    public abstract class UserConfigurationBase
    {
        public abstract void Load();
        public abstract void Save();


        protected UserConfigurationBase()
        {
            PlatformInstance = this;
        }
        public static string GetDirectory()
        {
            /// <summary>
            /// Location of Configuration file located in streamingAssets folder, located in persistentDataPath for Android
            /// </summary>
#if UNITY_ANDROID || PLATFORM_LUMIN || UNITY_LUMIN
            Path.Combine(Application.persistentDataPath,"cached","user_configurations");
#elif UNITY_WEBGL// && !UNITY_EDITOR
            return Path.Combine(Application.persistentDataPath,"cached","user_configurations");
#elif UNITY_WSA_10_0
            Path.Combine(Application.persistentDataPath,"cached","user_configurations");
#elif UNITY_EDITOR
            return Path.Combine(Application.streamingDataPath,"cached","user_configurations");
#else
            return Path.Combine(Application.persistentDataPath,"cached","user_configurations");
#endif
        }

        public static UserConfigurationBase PlatformInstance { get; private set; }
    }

}