using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ervean.Utilities.Scene
{
    public class SceneSwitcher : MonoBehaviour
    {
        public virtual void SwitchScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}