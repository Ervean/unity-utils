using Ervean.Utilities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Items
{
    public class DroppedItem : Item
    {
       public virtual void Consume(IItemConsumer consumer)
       {
            Destroy(this);
       }
    }
}