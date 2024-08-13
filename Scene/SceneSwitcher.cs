using Ervean.Utilities.DesignPatterns.SingletonPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ervean.Utilities.Scene
{
    public class SceneSwitcher : Singleton<SceneSwitcher>
    {
        public void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}