using Ervean.Utilities.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Player
{
    public interface IItemConsumer 
    {
        
        event EventHandler<ItemConsumedEventArgs> ItemConsumed;
    }

    public class ItemConsumedEventArgs
    {
        public IItemConsumer Consumer;
        public Item Consumed;
    }
}