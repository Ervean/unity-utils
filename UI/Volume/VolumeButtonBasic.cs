using UnityEngine;
using UnityEngine.UI;
namespace Ervean.Utilities.Volume
{
   public class VolumeButtonBasic : MonoBehaviour
    {
        [SerializeField] private Button _buttonMute;
        [SerializeField] private Image _buttonImage;
        [SerializeField] private Sprite _volumeOn;
        [SerializeField] private Sprite _volumeOff;
        [SerializeField] private Slider _volumeSlider;
        private bool _isMute = false;
        private float _volume = 1.0f;
      

        private void Start()
        {
            RefreshUI();
            _volumeSlider.onValueChanged.AddListener(VolumeSliderChanged);
        }

        private void VolumeSliderChanged(float v)
        {
            _volume = v;
            AudioListener.volume = _volume;
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
                AudioListener.volume = _volume;
            }
            RefreshUI();
        }

        public void RefreshUI()
        {
            if(_isMute)
            {
                _buttonImage.sprite = _volumeOff;
                _volumeSlider.SetValueWithoutNotify(0);
                _volumeSlider.interactable = false;
            }
            else
            {
                _buttonImage.sprite = _volumeOn;
                _volumeSlider.value = _volume; 
                _volumeSlider.interactable = true;
            }
        }
    }
}