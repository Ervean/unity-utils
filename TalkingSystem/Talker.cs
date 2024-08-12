using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Talking
{
    /// <summary>
    /// A talker
    /// </summary>
    public class Talker : MonoBehaviour
    {
        [SerializeField] private TalkerData data;

        [SerializeField] private float jitterUpAmount = 15f;
        [SerializeField] private float jitterDuration = .2f;
        float timeElapsed = 0;

        private Vector3 originalPosition = Vector3.zero;
        private bool isJitteringUp = false;
        private bool isJitteringDown = false;
        private void Awake()
        {
            originalPosition = (transform as RectTransform).anchoredPosition;
        }

        public void JitterUp()
        {
            isJitteringUp = true;
            isJitteringDown = false;
            timeElapsed = 0;
        }

        private void Update()
        {
            if(isJitteringUp)
            {
                if(timeElapsed < jitterDuration/2)
                {
                    float y = Mathf.Lerp(originalPosition.y, originalPosition.y + jitterUpAmount, timeElapsed / jitterDuration/2);
                    (transform as RectTransform).anchoredPosition = new Vector3(originalPosition.x, y, originalPosition.z);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    isJitteringUp = false;
                    isJitteringDown = true;
                    timeElapsed = 0;
                }
            }

            if(isJitteringDown)
            {
                if (timeElapsed < jitterDuration / 2)
                {
                    float y = Mathf.Lerp(originalPosition.y + jitterUpAmount, originalPosition.y, timeElapsed / jitterDuration/2);
                    (transform as RectTransform).anchoredPosition = new Vector3(originalPosition.x, y, originalPosition.z);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    isJitteringUp = false;
                    isJitteringDown = false;
                    timeElapsed = 0;    
                }
            }
        }
    }
}