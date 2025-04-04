using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Scene
{
    [CreateAssetMenu(fileName = "SceneStateData", menuName = "ScriptableObjects/SceneState/SceneData")]
    public class SceneState : ScriptableObject
    {
        [SerializeField] protected string _sceneName; // The actual unity scene name
        [SerializeField] protected string _sceneDisplayName;

        protected Dictionary<string, object> _payload;
        public string SceneName => _sceneName;
        public string SceneDisplayName => _sceneDisplayName;

        public virtual void SetPayload(Dictionary<string, object> payload)
        {
            _payload = payload;
        }
    }
}