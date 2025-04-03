
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ervean.Utilities.GameStates
{
    [RequireComponent (typeof (GameStateManager))]
    public class GameStateUnityEvents : MonoBehaviour
    {
        [SerializeField] private List<GameStateUnityEvent> _gameStates = new List<GameStateUnityEvent>();
        private GameStateManager _gameStateManager;

        private void Awake()
        {
            _gameStateManager = GetComponent<GameStateManager>();
            _gameStateManager.GameStateChanged += _gameStateManager_GameStateChanged;
        }

        private void OnDestroy()
        {
            _gameStateManager.GameStateChanged -= _gameStateManager_GameStateChanged;
        }
        private void _gameStateManager_GameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            GameStateUnityEvent current = GetGameStateUnityEvent(e.Current);
            GameStateUnityEvent previous = GetGameStateUnityEvent(e.Previous);

            if(previous != null)
            {
                previous.ExitGameState?.Invoke();
            }

            if (current != null)
            {
                current.EnterGameState?.Invoke();
            }
        }

        private GameStateUnityEvent GetGameStateUnityEvent(GameState gs)
        {
            foreach (GameStateUnityEvent ue in _gameStates)
            {
                if(ue.GameState == gs) return ue;
            }
            return null;
        }
    }

    [Serializable] 
    public class GameStateUnityEvent
    {
        public GameState GameState;
        public UnityEvent EnterGameState;
        public UnityEvent ExitGameState;
    }
}