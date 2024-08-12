using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Talking
{
    [CreateAssetMenu(fileName = "Talk", menuName = "Talk/Talker")]
    public class TalkerData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private string talkerName;
        [SerializeField] private GameObject talkerDefault;
        [SerializeField] private GameObject talkerMad;
        [SerializeField] private GameObject talkerSad;
        [SerializeField] private GameObject talkerHappy;


        public int Id => id;
        public string TalkerName => talkerName;
        public GameObject TalkerDefault => talkerDefault;
        public GameObject TalkerMad => talkerMad;
        public GameObject TalkerSad => talkerSad;
        public GameObject TalkerHappy => talkerHappy;

        public GameObject GetTalkingSprite(TalkerEmotions e)
        {
            switch(e)
            {
                case TalkerEmotions.Mad:
                    {
                        return TalkerMad;
                    }
                case TalkerEmotions.Sad:
                    {
                        return TalkerSad;
                    }
                case TalkerEmotions.Happy:
                    {
                        return TalkerHappy;
                    }
                case TalkerEmotions.Default:
                    {
                        return TalkerDefault;
                    }
            }

            return TalkerDefault;
        }

        public void SetId(int id)
        {
            this.id = id;
        }

    }

    public enum TalkerEmotions
    {
        Default,
        Mad,
        Sad,
        Happy
    }
}