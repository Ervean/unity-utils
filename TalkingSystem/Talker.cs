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

        private bool isJitteringUp = false;
        private bool isJitteringDown = false;


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
                    float y = Mathf.Lerp(0, jitterUpAmount, timeElapsed / jitterDuration/2);
                    transform.localPosition = new Vector3(this.transform.localPosition.x, y, this.transform.localPosition.z);
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
                    float y = Mathf.Lerp(jitterUpAmount, 0, timeElapsed / jitterDuration/2);
                    transform.localPosition = new Vector3(this.transform.localPosition.x, y, this.transform.localPosition.z);
                    timeElapsed += Time.deltaTime;
                }
                else
                {
                    transform.localPosition = new Vector3(this.transform.localPosition.x, 0, this.transform.localPosition.z);
                    isJitteringUp = false;
                    isJitteringDown = false;
                    timeElapsed = 0;    
                }
            }
        }
    }
}