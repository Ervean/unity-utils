using Ervean.Utilities.Talking.Conversations;
using UnityEngine;

namespace Ervean.Utilities.Talking
{
    public class ConversationStarter : MonoBehaviour
    {
        [SerializeField] private TalkingManager talkingManager;
        private ConversationDatabase conversationDatabase;

        [SerializeField] private int conversationId = 0;
        private static bool isConsumed = false;
        private void Awake()
        {
            conversationDatabase = ConversationDatabase.Instance;
            if (!isConsumed)
            {
                talkingManager.Talk(conversationDatabase.GetConversation(conversationId).Talking);
            }
            isConsumed = true;
        }
    }
}