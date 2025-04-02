using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ervean.Utilities.UI
{
    public class PanelLerper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Required")] //test
        [SerializeField] private RectTransform movingPanel;

        [Header("Settings")]
        [SerializeField] private float lerpSpeed = 0.15f;
        [SerializeField] private Vector2 moveTo = Vector2.zero;


        private Vector2 _cachedPosition = Vector2.zero;

        public bool IsShown => isShown;
        private bool isShown = false;

        private void Awake()
        {
            _cachedPosition = movingPanel.transform.localPosition;
        }

        private void Update()
        {
            if (isShown && movingPanel.anchoredPosition != _cachedPosition)
            {
                movingPanel.anchoredPosition = Vector2.Lerp(movingPanel.anchoredPosition, isShown ? _cachedPosition : moveTo, lerpSpeed);
            }
            else if (!isShown && movingPanel.anchoredPosition != moveTo)
            {
                movingPanel.anchoredPosition = Vector2.Lerp(movingPanel.anchoredPosition, isShown ? _cachedPosition : moveTo, lerpSpeed);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isShown = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isShown = false;
        }
    }
}