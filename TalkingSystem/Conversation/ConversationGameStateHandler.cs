using UnityEngine;
using Ervean.Utilities.Talking;
using System;
using System.Collections.Generic;
using Ervean.Utilities.Talking.Conversations;

namespace Ervean.Utilities.GameStates
{
    /// <summary>
    /// Handles starting a conversation and then going to next game state when completed
    /// </summary>
    public class ConversationGameStateHandler : MonoBehaviour
    {
        [SerializeField] private TalkingManager _talkingManager;
        [SerializeField] private List<ConversationGameStateRoute> _states = new List<ConversationGameStateRoute>();

        private Dictionary<GameState, ConversationGameStateRoute> _map = new Dictionary<GameState, ConversationGameStateRoute>();
        private ConversationDatabase _database;
        private GameState _current;
        private void Awake()
        {
            _database = ConversationDatabase.Instance;
            GameStateManager.Instance.GameStateChanged += Instance_GameStateChanged;
            foreach(var state in _states)
            {
                _map[state.Incoming] = state;
            }
        }

        private void OnDestroy()
        {
            if (GameStateManager.Instance != null)
            {
                GameStateManager.Instance.GameStateChanged -= Instance_GameStateChanged;
            }
        }
        private void Instance_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            if(_map.ContainsKey(e.Current))
            {
                _current = e.Current;
                _talkingManager.EndConversation += _talkingManager_EndConversation;
                Conversation conversation = _database.GetConversation(_map[e.Current].ConversationId);
                _talkingManager.Talk(conversation);
            }
        }

        private void _talkingManager_EndConversation(object sender, EndedConversationEventArgs e)
        {
            _talkingManager.EndConversation -= _talkingManager_EndConversation;

            GameStateManager.Instance.ChangeGameState(_map[_current].Outgoing.GameStateName);
        }
    }

    [Serializable]
    public class ConversationGameStateRoute
    {
        public GameState Incoming; // when this game state has been entered
        public GameState Outgoing; // go to this gamestate when conversation is completed
        public int ConversationId; // id to play when entering game state
    }
    
}