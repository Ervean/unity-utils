using Ervean.Utilities.Pause;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Ervean.Utilities.Pause
{
    public static class PauseManager
    {
        private static List<IPausable> pausableList = new List<IPausable>();

        public static List<IPausable> Pausables => pausableList;

        private static bool isPaused = false;
        public static event System.EventHandler<PausedEventArgs> Paused;
        public static event System.EventHandler<UnpausedEventArgs> Unpaused;
        private static List<string> _keepPaused = new List<string>();

        public class PausedEventArgs
        {

        }

        public class UnpausedEventArgs
        {

        }

        public static void Subscribe(IPausable pausable)
        {
            pausableList.Add(pausable);
        }

        public static void UnSubscribe(IPausable pausable)
        {
            if (pausableList.Contains(pausable))
            {
                pausableList.Remove(pausable);
            }
        }

        public static void Clear()
        {
            pausableList.Clear();
            _keepPaused.Clear();
            UnPause();
        }

        /// <summary>
        /// Force Pause
        /// </summary>
        public static void Pause()
        {
            isPaused = true;
            List<IPausable> cachedPausibleList = new List<IPausable>(pausableList); // must cache first as list might change later on and enumeration error
            foreach (IPausable pause in cachedPausibleList)
            {
                pause.Pause();
            }
            Paused?.Invoke(null, new PausedEventArgs());
        }

        /// <summary>
        /// Pauses and adds id to list of things wanting system to be paused, only unpauses if list is empty
        /// </summary>
        /// <param name="id"></param>
        public static void Pause(string id)
        {
            _keepPaused.Add(id);
            // Debug.LogError("Adding pause: " + id);
            if (!isPaused)
            {
                Pause();
            }
        }

        /// <summary>
        /// Only unpauses if no id is trying to pause, will first remove the id from list, and if list is empty will actually unpause
        /// </summary>
        /// <param name="id"></param>
        public static void UnPause(string id)
        {
            if (_keepPaused.Contains(id))
            {
                _keepPaused.Remove(id);
                //  Debug.LogError("Removing pause: " + id);
            }
            if (_keepPaused.Count == 0 && isPaused)
            {
                UnPause();
            }
        }

        /// <summary>
        /// Force Unpause
        /// </summary>
        public static void UnPause()
        {
            isPaused = false;
            foreach (IPausable pause in pausableList)
            {
                pause.UnPause();
            }
            Unpaused?.Invoke(null, new UnpausedEventArgs());
        }

        public static bool IsPaused()
        {
            return isPaused;
        }

    }
}