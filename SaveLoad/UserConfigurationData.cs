using System;
using System.Dynamic;
using UnityEngine;

namespace Ervean.Utilities.SaveLoad
{
    [Serializable]
    public class UserConfigurationData
    {
        /// <summary>
        /// General Master volume for application, doesnt affect other items that live on their own volume like videos
        /// </summary>
        public float Volume { get; set; }

        public const float MaxVolume = 1.0f;

        /// <summary>
        /// Normalized value of the volume with 1.0 being the MaxVolume
        /// </summary>
        /// <returns></returns>
        public float GetNormalizedVolume()
        {
            return Volume / MaxVolume;
        }
    }

}