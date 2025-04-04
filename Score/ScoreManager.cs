using Ervean.Utilities.DesignPatterns.SingletonPattern;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Score
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        private List<IScorable> _scores = new List<IScorable>();
        public event EventHandler<ScoreUpdatedEventArgs> ScoreUpdated;
        
        public void OnScoreUpdated(object sender, ScoreUpdatedEventArgs args)
        {
            ScoreUpdated?.Invoke(this, args);
        }

        public void Clear()
        {
            _scores.Clear();
        }

        public void ResetScores()
        {
            foreach (IScorable score in _scores)
            {
                score.ResetScore();
            }
        }

        public void Register(IScorable score)
        {
            _scores.Add(score);
            score.SubscribeToScoreManager(this);
        }

        public void Remove(IScorable score)
        {
            if(_scores.Contains(score)) _scores.Remove(score);
        }

        public float GetMaxScore()
        {
            float total = 0f;
            foreach (IScorable score in _scores)
            {
                if (score.WasScored)
                {
                    total += score.MaxScore;
                }
            }
            return total;
        }

        public float GetTotalScore()
        {
            float total = 0f;
            foreach (IScorable score in _scores)
            {
                if (score.WasScored)
                {
                    total += score.Score;
                }
            }
            return total;
        }

        public void Refresh()
        {
            OnScoreUpdated(this, new ScoreUpdatedEventArgs()
            {
                Score = GetTotalScore(),
                MaxScore = GetMaxScore()
            });
        }
    }

    public class ScoreUpdatedEventArgs
    {
        public float Score;
        public float MaxScore;
    }
}