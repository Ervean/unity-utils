using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ervean.Utilities.Talking
{
    public class TalkingManager : MonoBehaviour
    {
        [SerializeField] private GameObject blocker;
        [SerializeField] private TalkerDatabase db;

        [SerializeField] private Transform leftPivot;
        [SerializeField] private Transform rightPivot;

        public event EventHandler<StartedConversationEventArgs> StartConversation;
        public event EventHandler<EndedConversationEventArgs> EndConversation;


        public void Talk(TalkingSettings s)
        {
            
        }
    }


    public class StartedConversationEventArgs
    {

    }

    public class EndedConversationEventArgs
    {

    }

    public class TalkingSettings
    {
        public int RightTalker;
        public int LeftTalker;
        public PrimaryTalker PrimaryTalker = PrimaryTalker.None;
        public TalkerEmotions RightTalkerEmotion = TalkerEmotions.Default;
        public TalkerEmotions LeftTalkerEmotion = TalkerEmotions.Default;
        public TalkingChain Chain = TalkingChain.ContinueConversation;     
    }

    public enum TalkingChain
    {
        ContinueConversation,
        EndConversation,
        UserQuiz
    }

    public enum PrimaryTalker
    {
        None,
        Right,
        Left
    }
    
}