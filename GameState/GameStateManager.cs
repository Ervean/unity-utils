using Ervean.Utilities.DesignPatterns.SingletonPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.GameStates
{
    /// <summary>
    /// Game State manager that controls the available game states in the game
    /// </summary>
    public class GameStateManager : SingletonDestroy<GameStateManager>
    {
        [SerializeField] private List<GameState> _gameStates = new List<GameState>();
        
        public GameState CurrentGameState { get; private set; }
        public GameState PreviousGameState { get; private set; }
        
        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;

        public void OnGameStateChanged(object sender, GameStateChangedEventArgs e)
        {
            GameStateChanged?.Invoke(this, e);
        }

        public void ChangeGameState(string name, Dictionary<string, object> payload = null)
        {
            GameState gs = GetGameStateByName(name);
            if (gs != null)
            {
                gs.SetPayload(payload);
                PreviousGameState = CurrentGameState != null ? CurrentGameState : gs;
                CurrentGameState = gs;
                OnGameStateChanged(this, new GameStateChangedEventArgs()
                {
                    Current = CurrentGameState,
                    Previous = PreviousGameState
                });
               // Debug.Log("Game State is now " + CurrentGameState.GameStateName + ", from " + PreviousGameState.GameStateName);
            }
            else
            {
                Debug.LogError(name + " was not found in GameStateManager");
            }
        }

        private GameState GetGameStateByName(string name)
        {
            foreach (var g in _gameStates)
            {
                if (g.GameStateName == name)
                {
                    return g;
                }
            }
            return null;
        }
    }

    public class GameStateChangedEventArgs
    {
        public GameState Current;
        public GameState Previous;
    }
}