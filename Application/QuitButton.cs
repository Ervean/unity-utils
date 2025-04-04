using UnityEngine;

namespace Ervean.Utilities.Applications
{
    public class QuitButton : MonoBehaviour
    {
        public void QuitApplication()
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }
    }
}