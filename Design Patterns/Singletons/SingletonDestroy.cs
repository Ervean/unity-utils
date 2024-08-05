using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
/// such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// Modified from: http://wiki.unity3d.com/index.php/Singleton
/// </summary>
/// 

namespace Ervean.Utilities.DesignPatterns.SingletonPattern
{
    public class SingletonDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static T instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                        "' already destroyed on application quit." +
                        " Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it.");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            if (beingDestroyed)
                            {
                                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                                "' already destroyed on scene reload." +
                                                " Won't create again - returning null.");
                                return null;
                            }

                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = typeof(T).Name;

                            Debug.Log("[Singleton] An instance of " + typeof(T) +
                                " is needed in the scene, so '" + singleton +
                                "' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            Debug.Log("[Singleton] Using instance already created: " +
                                _instance.gameObject.name);
                        }
                    }

                    return _instance;
                }
            }
        }
        /// <summary>
        /// If this script is placed on game objec in the scene, make sure to
        /// keep only instance.
        /// </summary>
        protected virtual void Awake()
        {
            if (_instance == null)
            {
                beingDestroyed = false;
                _instance = this as T;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        private static bool applicationIsQuitting = false;
        /// <summary>
        /// When Unity quits, it destroys objects in a random order.
        /// In principle, a Singleton is only destroyed when application quits.
        /// If any script calls Instance after it have been destroyed, 
        /// it will create a buggy ghost object that will stay on the Editor scene
        /// even after stopping playing the Application. Really bad!
        /// So, this was made to be sure we're not creating that buggy ghost object.
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            applicationIsQuitting = true;
        }

        private static bool beingDestroyed = false;
        /// <summary>
        /// When leaving a scene, this object will get destroyed. However, some other gameobjects
        /// maybe cause it to regenerate in their own OnDestroy functions. We don't want this 
        /// gameobject to linger and so we should mark whether we have been destroyed recently.
        /// </summary>
        protected virtual void OnDestroy()
        {
            beingDestroyed = true;
        }
    }
}
