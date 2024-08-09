using Ervean.Utilities.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ervean.Utilities.Player
{
    public class PlayerItemPickUpManager : MonoBehaviour, IItemConsumer
    {
        public event EventHandler<ItemConsumedEventArgs> ItemConsumed;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DroppedItem item = collision.gameObject.GetComponent<DroppedItem>();
            if (item == null)
            {
                Debug.LogError("not found");
                return;
            }
            Debug.LogError("Touched a dropped item: " + item.ItemName);
            item.Consume(this);
            ItemConsumed?.Invoke(this, new ItemConsumedEventArgs()
            {
                Consumed = item,
                Consumer = this
            });
        }


    }
}