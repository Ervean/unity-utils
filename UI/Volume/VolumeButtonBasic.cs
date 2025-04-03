using UnityEngine;
using UnityEngine.UI;
namespace Ervean.Utilities.Volume
{
    /// <summary>
    /// Very basic volume, only handles muting and unmuting, no volume bar
    /// </summary>
   public class VolumeButtonBasic : MonoBehaviour
    {
        [SerializeField] private Button _buttonMute;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _volumeOn;
        [SerializeField] private Sprite _volumeOff;
        private bool _isMute = false;

        private void Start()
        {
            RefreshIcon();
        }

        public void ToggleMute()
        {
            _isMute = !_isMute;
            if(_isMute)
            {
                AudioListener.volume = 0f;
            }
            else
            {
                AudioListener.volume = 1f;
            }
            RefreshIcon();
        }

        public void RefreshIcon()
        {
            if(_isMute)
            {
                _buttonImage.sprite = _volumeOff;
            }
            else
            {
                _buttonImage.sprite = _volumeOn;
            }
        }
    }
}