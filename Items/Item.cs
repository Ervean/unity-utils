using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Items
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] protected string itemName;
        [SerializeField] protected ItemData data;
    }
}