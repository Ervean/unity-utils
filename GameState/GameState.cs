using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.GameStates
{
    [CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState/GameStateData")]
    public class GameState : ScriptableObject
    {
        [SerializeField] private string _gameStateName;
        public string GameStateName => _gameStateName;

        private Dictionary<string, object> _payload = new Dictionary<string, object>();

        public Dictionary<string, object> Payload => _payload;

        public void SetPayload(Dictionary<string, object> payload)
        {
            _payload = payload;
        }
        
    }
}