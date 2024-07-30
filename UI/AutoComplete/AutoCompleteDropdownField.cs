using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Ervean.Utilities.UI.Internal;

namespace Ervean.Utilities.UI
{
    public class AutoCompleteDropdownField : MonoBehaviour
    {
        [Header("Required Inspector Fields")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private AutoCompleteDropdown dropdown;
        [SerializeField] private Button buttonExpandDropdown;

        /// <summary>
        /// Forces the input to always have an option in the dropdown, defaults to first option at start and will return back to previous valid option if provided invalid option
        /// </summary>
        [Header("Settings")]
        [SerializeField] private bool forceValidOption = true;
        [SerializeField] private bool openDropdownWhenInputFieldIsEmpty = false;
        [SerializeField] private bool isCaseSensitive = false;

        private List<AutoCompleteOptionData> _options = new List<AutoCompleteOptionData>();
        public bool IsDropdownOpen => dropdown.gameObject.activeSelf;


        #region Events
        public event EventHandler<AutoCompleteDropdownFieldValueChangedEventArgs> ValueChanged;

        public void OnValueChanged(object sender, AutoCompleteDropdownFieldValueChangedEventArgs e)
        {
            ValueChanged?.Invoke(sender, e);
        }
        #endregion


        private bool _clickedInValidSpot = false;
        /// <summary>
        /// Text cached when input field is selected
        /// </summary>
        private string _cachedText;
        private bool _isInitialized = false;

         

        private void Start()
        {
            Initialize();
           // SetOptionsForTesting();
        }

        public string GetCurrentText()
        {
            return inputField.text;
        }

        public void SetOptions(List<AutoCompleteOptionData> options, string defaultSelection = null)
        {
            this._options = options;
            if (forceValidOption)
            {
                foreach (AutoCompleteOptionData o in options)
                {
                    if (o.Text == defaultSelection)
                    {
                        inputField.SetTextWithoutNotify(defaultSelection);
                        _cachedText = inputField.text;
                        return;
                    }
                }

                if(options.Count > 0)
                {
                    if (defaultSelection != null)
                    {
                        Debug.LogError("AutoCompleteDropdownField::SetOptions -> " + defaultSelection + " is not a valid option, setting to " + options[0].Text);
                    }
                    if (defaultSelection == null)
                    {
                        inputField.text = options[0].Text;
                    }
                    else
                    {
                        inputField.SetTextWithoutNotify(options[0].Text);
                    }
                    _cachedText = inputField.text;
                }
                else
                {
                    inputField.SetTextWithoutNotify(defaultSelection);
                    _cachedText = inputField.text;
                }
            }
            else
            {
                inputField.SetTextWithoutNotify(defaultSelection);
                _cachedText = inputField.text;
            }
        }

        public void RefreshDropdownItems(string filter = null)
        {
            List<AutoCompleteOptionData> options = new List<AutoCompleteOptionData>();

            foreach(var option in this._options)
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    if (isCaseSensitive)
                    {
                        if (option.Text.Contains(filter))
                        {
                            options.Add(option);
                        }
                    }
                    else
                    {
                        if(option.Text.ToLower().Contains(filter.ToLower()))
                        {
                            options.Add(option);
                        }
                    }
                }
                else
                {
                    options.Add(option);
                }
            }

            dropdown.Refresh(options.ToArray(), null);
        }

        public void ToggleInteractable(bool isInteractable)
        {
            inputField.interactable = isInteractable;
            buttonExpandDropdown.interactable = isInteractable;
            if(!isInteractable)
            {
                DisableDropdown();   
            }
        }

        private void Initialize()
        {
            DisableDropdown();
            AttachListeners();
            _isInitialized = true;
        }

        private void AttachListeners()
        {
            inputField.onSelect.AddListener(OnInputFieldSelected);
            inputField.onDeselect.AddListener(OnInputFieldEndEdit);
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);

            buttonExpandDropdown.onClick.AddListener(OnButtonClicked);

            dropdown.AutoCompleteDropdownValueChanged += OnDropdownValueChanged;
        }


        private void OnDestroy()
        {
            if(_isInitialized)
            {
                dropdown.AutoCompleteDropdownValueChanged -= OnDropdownValueChanged;
            }
        }


        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(IsCursorOnDropdown())
                {
                    _clickedInValidSpot = true;
                }
                else
                {
                    if (_cachedText != inputField.text)
                    {
                        ValidateInput();
                    }
                    DisableDropdown();
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                if (_clickedInValidSpot)
                {
                    // do nothing
                }
                else
                {
                    if (IsCursorOnDropdown())
                    {

                    }
                    else
                    {
                        if (_cachedText != inputField.text)
                        {
                            ValidateInput();
                        }
                        DisableDropdown();
                    }
                }
                _clickedInValidSpot = false;
            }

            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(IsDropdownOpen)
                {
                    if (_cachedText != inputField.text)
                    {
                        ValidateInput();
                    }
                    DisableDropdown();
                }
            }
        }

        #region Raycast
        /// <summary>
        /// Also includes button and input field
        /// </summary>
        /// <returns></returns>
        private bool IsCursorOnDropdown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = Input.mousePosition;

                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    foreach (var go in raycastResults)
                    {
                        if (go.gameObject == dropdown.gameObject || go.gameObject == inputField.gameObject || go.gameObject == buttonExpandDropdown.gameObject)
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }


        #endregion

        #region UI Listeners
        private void OnInputFieldSelected(string value)
        {
            _clickedInValidSpot = true;
            _cachedText = value;
            if (!openDropdownWhenInputFieldIsEmpty && value == "")
            {
                DisableDropdown();
            }
            else
            {
                EnableDropdown();
                RefreshDropdownItems(value);
                inputField.ActivateInputField();
            }
        }

        private void OnInputFieldValueChanged(string value)
        {
            if (!openDropdownWhenInputFieldIsEmpty && value == "")
            {
                DisableDropdown();
            }
            else
            {
                EnableDropdown();
                RefreshDropdownItems(value);
                inputField.ActivateInputField();
            }

        }

        private void OnInputFieldEndEdit(string value)
        {
        }

        private void OnButtonClicked()
        {
            EnableDropdown();
            RefreshDropdownItems();
        }

        private void OnDropdownValueChanged(object sender, AutoCompleteDropdownValueChangedEventArgs e)
        {
            inputField.SetTextWithoutNotify(e.selectedData.Text);
            ValidateInput();
            DisableDropdown();
        }

        #endregion

        private void ValidateInput()
        {
            if (forceValidOption)
            {

                AutoCompleteOptionData firstOption = null;
                foreach(var option in _options)
                {
                    if(firstOption == null)
                    {
                        firstOption = option;
                    }
                    if(inputField.text == option.Text)
                    {
                        ValueChanged?.Invoke(this, new AutoCompleteDropdownFieldValueChangedEventArgs()
                        {
                            ForceValidOption = forceValidOption,
                            SelectedOption = option,
                            RawText = option.Text,
                            OldText = _cachedText
                        });
                        _cachedText = inputField.text;
                        return;
                    }
                }
                if (firstOption != null)
                {
                    Debug.LogError("AutoCompleteDropdownField::ValidateInput -> " + inputField.text + " failed validation, setting back to first option");
                    inputField.SetTextWithoutNotify(firstOption.Text);
                    ValueChanged?.Invoke(this, new AutoCompleteDropdownFieldValueChangedEventArgs()
                    {
                        ForceValidOption = forceValidOption,
                        SelectedOption = firstOption,
                        RawText = firstOption.Text,
                        OldText = _cachedText
                    });
                    _cachedText = inputField.text;
                }
            }
            else
            {
                ValueChanged?.Invoke(this, new AutoCompleteDropdownFieldValueChangedEventArgs()
                {
                    ForceValidOption = forceValidOption,
                    SelectedOption = null,
                    RawText = inputField.text,
                    OldText = _cachedText
                });
                _cachedText = inputField.text;
            }
        }

        private void DisableDropdown()
        {
            dropdown.gameObject.SetActive(false);
        }

        private void EnableDropdown()
        {
            dropdown.gameObject.SetActive(true);
        }


        private void SetOptionsForTesting()
        {
            _options.Clear();
            for(int i = 0; i < 26; i++)
            {
                string result = "";
                for(int k = 0; k < i; k++)
                {
                    result += i.ToString();
                }
                _options.Add(new AutoCompleteOptionData()
                {
                    Text = result
                });
            }
        }
    }
    public class AutoCompleteOptionData
    {
        public string Text;
    }

    public class AutoCompleteDropdownFieldValueChangedEventArgs
    {
        /// <summary>
        /// This dropdown must have a selection based on a dropdown option
        /// </summary>
        public bool ForceValidOption;
        public AutoCompleteOptionData SelectedOption;
        /// <summary>
        /// Usually used when ForceValidOption is false, and user input something that is not an option
        /// </summary>
        public string RawText;
        public string OldText;

    }
}
