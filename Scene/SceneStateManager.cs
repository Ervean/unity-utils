using Ervean.Utilities.DesignPatterns.SingletonPattern;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Ervean.Utilities.Scene
{
    /// <summary>
    /// Handles controlling scene switching and handling data between scnes
    /// </summary>
    public class SceneStateManager : Singleton<SceneStateManager>
    {
        [SerializeField] private List<SceneState> _scenes = new List<SceneState>();

        [Header("Optional")]
        [SerializeField] private SceneSwitcher _sceneSwitcher;

        /// <summary>
        /// Map is used to easily find scene based on its name
        /// </summary>
        private Dictionary<string,  SceneState> _map = new Dictionary<string, SceneState>();

        public event EventHandler<SceneSwitchedEventArgs> SceneSwitched;

        protected override void Awake()
        {
            base.Awake();

            _map.Clear();
            foreach(SceneState s in _scenes)
            {
                _map[s.SceneName] = s;
            }

            if(_sceneSwitcher == null) _sceneSwitcher = this.gameObject.AddComponent<SceneSwitcher>();
        }

        public void SwitchScene(string sceneName, Dictionary<string, object> payload = null)
        {
            if(!_map.ContainsKey(sceneName))
            {
                Debug.LogError("Unknown scene name " +  sceneName + " terminating scene switch");
                return;
            }
            
            SceneState sceneState = _map[sceneName];
            sceneState.SetPayload(payload);

            _sceneSwitcher.SwitchScene(sceneName);

            SceneSwitched?.Invoke(this, new SceneSwitchedEventArgs()
            {
                SceneState = sceneState
            });
        }
    }

    public class SceneSwitchedEventArgs
    {
        public SceneState SceneState;
    }
}