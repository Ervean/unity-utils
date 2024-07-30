using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Pause
{
    public interface IPausable
    {
        void Pause();
        void UnPause();
        bool IsPaused();
    }
}