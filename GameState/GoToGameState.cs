using UnityEngine;

namespace Ervean.Utilities.GameStates
{
    /// <summary>
    /// Helper class that has a method to request game state manager to go to a state, can be placed on unity events to trigger going to new game states
    /// Typically used for small fast transitions
    /// </summary>
    public class GoToGameState : MonoBehaviour
    {
        private GameStateManager _manager;

        private void Awake()
        {
            _manager = GetComponent<GameStateManager>();
        }

        public void ChangeGameState(string name)
        {
            if(_manager == null)
            {
                return;
            }
            _manager.ChangeGameState(name);
        }
    }
}