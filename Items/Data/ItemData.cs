using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ervean.Utilities.Items
{
    public abstract class ItemData : ScriptableObject
    {
        [SerializeField] protected int id;
        [SerializeField] protected string itemName;
        [SerializeField] protected Texture2D thumbnail;

    }
}