using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ervean.Utilities.UI.Internal;

namespace Ervean.Utilities.UI
{
    public class AutoCompleteDropdownOption : MonoBehaviour, ILayoutGroupItem
    {
        [Header("Required")]
        [SerializeField] private GameObject rootPanel;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI text;

        [SerializeField] private VerticalLayoutGroup layout;

        private AutoCompleteOptionData option;
        private AutoCompleteDropdown dropdown;

        private bool isInitialized = false;
        public bool IsInitialized => isInitialized;

        private IRootLayoutGroupController rootController;
        public IRootLayoutGroupController RootController => rootController;

        public void Initialize(AutoCompleteDropdown dropdown)
        {
            this.dropdown = dropdown;
            SetRootLayoutGroupController(dropdown);
            AttachListeners();
            isInitialized = true;
        }

        private void AttachListeners()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        #region UI Listeners
        private void OnButtonClicked()
        {
            dropdown.OnAutoCompleteDropdownValueChanged(this, new AutoCompleteDropdownValueChangedEventArgs()
            {
                selectedData = option,
            });
        }

        #endregion

        public void SetData(AutoCompleteOptionData option)
        {
            this.option = option;
            this.text.text = option.Text;
        }

        public void SetRootLayoutGroupController(IRootLayoutGroupController controller)
        {
            this.rootController = controller;
        }

        public void UpdateLayout()
        {
            layout.CalculateLayoutInputVertical();
            layout.SetLayoutVertical();
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.transform as RectTransform);
        }
    }
}