using System;
using System.Collections;
using System.Collections.Generic;
using Ervean.Utilities.UI;
using UnityEngine.UI;
using UnityEngine;
namespace Ervean.Utilities.UI.Internal
{
    public class AutoCompleteDropdown : MonoBehaviour, IRootLayoutGroupController
    {
        [Header("Required Inspector")]
        [SerializeField] private GameObject rootPanel;
        [SerializeField] private Transform transformScrollRectContent;
        [SerializeField] private VerticalLayoutGroup layout;

        [Header("Prefabs")]
        [SerializeField] private GameObject prefabOption;

        public event EventHandler<AutoCompleteDropdownValueChangedEventArgs> AutoCompleteDropdownValueChanged;
        public event EventHandler<UpdateLayoutEventArgs> UpdateLayout;

        private void Start()
        {
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            foreach (Transform child in transformScrollRectContent)
            {
                AutoCompleteDropdownOption option = child.GetComponent<AutoCompleteDropdownOption>();
                if (!option.IsInitialized)
                {
                    option.Initialize(this);
                }
            }
        }



        public void Refresh(AutoCompleteOptionData[] data, AutoCompleteOptionData currentOption)
        {
            if (transformScrollRectContent.childCount < data.Length) // generate any new options that are needed
            {
                int toGenerate = data.Length - transformScrollRectContent.childCount;
                for (int i = 0; i < toGenerate; i++)
                {
                    GameObject prefabInstance = Instantiate(prefabOption, transformScrollRectContent);
                    AutoCompleteDropdownOption option = prefabInstance.GetComponent<AutoCompleteDropdownOption>();
                    option.Initialize(this);
                }
            }

            int dataCount = 0;
            foreach (Transform child in transformScrollRectContent)
            {
                AutoCompleteDropdownOption option = child.GetComponent<AutoCompleteDropdownOption>();
                if (dataCount < data.Length)
                {
                    option.SetData(data[dataCount]);
                    dataCount++;
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
            RequestUpdateLayout();

        }

        public void OnAutoCompleteDropdownValueChanged(object sender, AutoCompleteDropdownValueChangedEventArgs e)
        {
            AutoCompleteDropdownValueChanged?.Invoke(sender, e);
        }

        public void RequestUpdateLayout()
        {
            foreach (Transform child in transformScrollRectContent)
            {
                ILayoutGroupItem item = child.GetComponent<ILayoutGroupItem>();
                item.UpdateLayout();
            }

            layout.CalculateLayoutInputVertical();
            layout.SetLayoutVertical();
            UpdateLayout?.Invoke(this, new UpdateLayoutEventArgs());
            LayoutRebuilder.ForceRebuildLayoutImmediate(layout.transform as RectTransform);
        }
    }

    public class AutoCompleteDropdownValueChangedEventArgs
    {
        public AutoCompleteOptionData selectedData;
    }
}