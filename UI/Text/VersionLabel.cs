using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Ervean.Utilities.UI
{
    public class VersionLabel : MonoBehaviour
    {
        [SerializeField] private TMP_Text textLabel;
        // Start is called before the first frame update
        void Awake()
        {
            textLabel.text = Application.version;
        }

       
    }
}