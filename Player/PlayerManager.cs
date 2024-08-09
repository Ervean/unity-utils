
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Ervean.Utilities.Player
{
    /// <summary>
    /// Handles all the different scipts that the player has
    /// </summary>
    public class PlayerManager : MonoBehaviour, IPlayer
    {
        [SerializeField] protected PlayerMovement movement;
        [SerializeField] protected IItemConsumer consumer;


        protected virtual void Awake()
        {
            consumer = GetComponent<IItemConsumer>();
        }

        protected virtual void OnEnable()
        {
            consumer.ItemConsumed += Consumer_ItemConsumed;
        }

        protected virtual void OnDisable()
        {
            consumer.ItemConsumed -= Consumer_ItemConsumed;
        }
        protected virtual void Consumer_ItemConsumed(object sender, ItemConsumedEventArgs e)
        {
            
        }
    }
}