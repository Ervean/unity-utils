using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Talking.Conversations
{
    [CreateAssetMenu(fileName = "Conversation", menuName = "Talk/Conversation")]
    public class Conversation : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private List<TalkingSettings> settings = new List<TalkingSettings>();

        public int Id => id;
        public List<TalkingSettings> Talking => settings;
        public void SetId(int id)
        {
            this.id = id;
        }

    }
}