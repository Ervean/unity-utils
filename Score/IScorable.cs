using Unity.VisualScripting;
using UnityEngine;

namespace Ervean.Utilities.Score
{
    public interface IScorable
    {
        float Score { get; }
        float MaxScore { get; }
        bool WasScored { get; }

        void ResetScore();

        void SubscribeToScoreManager(ScoreManager manager);
    }
}