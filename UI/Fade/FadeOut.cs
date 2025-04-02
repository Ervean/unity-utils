using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Ervean.Utilities.UI
{
    /// <summary>
    /// Fades in or out, out = 1 alpha to 0
    /// </summary>
    public class FadeOut : MonoBehaviour
    {
        [Header("Image")]
        [SerializeField] private Image image;

        [Header("Settings")]
        [SerializeField] private float speed = 1.5f;
        [SerializeField] private bool doOnStart = true;

        private void Awake()
        {
            if(doOnStart)
            {
                StartCoroutine(DoFadeOut());
            }
        }

        private void OnValidate()
        {
            if (image == null)
            {
                image = this.GetComponent<Image>();
            }
        }

        public IEnumerator DoFadeOut()
        {
            image.enabled = true;
            while(image.color.a > .05f)
            {
                Color color = image.color;
                if(image.color.a >= .6f)
                {
                    color.a = Mathf.Lerp(image.color.a, 0f, Mathf.Min(Time.deltaTime * speed, Time.deltaTime * speed / 2 ));
                }
                else
                {
                    color.a = Mathf.Lerp(image.color.a, 0f, Time.deltaTime * speed * 1.5f);
                }
                image.color = color;
                yield return null;
            }
            image.enabled = false;
        }

        

        public IEnumerator DoFadeIn()
        {
            image.enabled = true;
            while (image.color.a > .95f)
            {
                Color color = image.color;
                color.a = Mathf.Lerp(image.color.a, 1f, speed);
                image.color = color;
                yield return null;
            }
            image.enabled = false;
        }
    }
}