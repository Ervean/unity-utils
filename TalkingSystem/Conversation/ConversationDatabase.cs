using Ervean.Utilities.DesignPatterns.SingletonPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Talking.Conversations
{
    public class ConversationDatabase : Singleton<ConversationDatabase>
    {
        [SerializeField] private List<Conversation> conversations = new List<Conversation>();


        private void OnValidate()
        {
            HashSet<int> takenIds = new HashSet<int>();

            if (conversations != null && conversations.Count > 0)
            {
                if (conversations.Count > 2)
                {
                    if (conversations[conversations.Count - 1] == conversations[conversations.Count - 2]) // usually occurs when adding a new entry into the database
                    {
                        return;
                    }
                }

                foreach (Conversation td in conversations)
                {
                    if (td != null)
                    {
                        if (takenIds.Contains(td.Id))
                        {
                            ProvideId(td);
                        }
                        else
                        {
                            takenIds.Add(td.Id);
                        }
                    }
                }
            }
        }

        public Conversation GetConversation(int id)
        {
            foreach(Conversation conversation in conversations)
            {
                if(conversation.Id == id)
                {
                    return conversation;
                }
            }
            return null;
        }

        private void ProvideId(Conversation td)
        {
            HashSet<int> ids = new HashSet<int>();
            foreach (Conversation c in conversations)
            {
                ids.Add(c.Id);
            }
            int id = 0;

            while (true)
            {
                if (ids.Contains(id))
                {
                    id++;
                }
                else
                {
                    td.SetId(id);
                    return;
                }
            }
        }

    }
}