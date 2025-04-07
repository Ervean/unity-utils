using UnityEngine;

namespace Ervean.Utilities.Applications
{
    public class SetTargetFrameRate : MonoBehaviour
    {
        [SerializeField] private int _targetFrameRate = 144;

        private void Awake()
        {
            Application.targetFrameRate = _targetFrameRate;
        }
    }
}