using Ervean.Utilities.Talking.Conversations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Ervean.Utilities.Talking
{
    public class TalkingManager : MonoBehaviour
    {
        [SerializeField] private GameObject blocker;
        [SerializeField] private TalkerDatabase db;

        [SerializeField] private Transform leftPivot;
        [SerializeField] private Transform rightPivot;
        [SerializeField] private GameObject textPanel;
        [SerializeField] private TMP_Text textHeader;
        [SerializeField] private TMP_Text textBody;

        [Header("Settings")]
        [SerializeField] private float timerForTalk = .5f;

        public event EventHandler<StartedConversationEventArgs> StartConversation;
        public event EventHandler<EndedConversationEventArgs> EndConversation;
        public event EventHandler<StartedTalkEventArgs> StartTalk;
        public event EventHandler<EndedTalkEventArgs> EndTalk;

        private GameObject currentLeft;
        private GameObject currentRight;
        private bool isTalking = false;
        private TalkingSettings latestSetting = null;
        private Queue<TalkingSettings> conversation = new Queue<TalkingSettings>();
        private bool canAdvanceTalk = false;

        [Obsolete]
        private void Awake()
        {
            db = TalkerDatabase.Instance;
        }

        public void Talk(TalkingSettings s)
        {
            if(isTalking)
            {
                conversation.Enqueue(s);
                return;
            }
            canAdvanceTalk = false;
            isTalking = true;
            latestSetting = s;
            StartConversation?.Invoke(this, new StartedConversationEventArgs());
            StartTalk?.Invoke(this, new StartedTalkEventArgs());
            blocker.SetActive(true);
            
            if (s.LeftTalker != -1)
            {
                currentLeft = db.GetTalkerSprite(s.LeftTalker, s.LeftTalkerEmotion);
                if (currentLeft != null)
                {
                    currentLeft = Instantiate(currentLeft, leftPivot);
                    currentLeft.transform.localPosition = Vector3.zero;
                }
            }
            if (s.RightTalker != -1)
            {
                currentRight = db.GetTalkerSprite(s.RightTalker, s.RightTalkerEmotion);
                if (currentRight != null)
                {
                    currentRight = Instantiate(currentRight, rightPivot);
                    currentRight.transform.localPosition = Vector3.zero;
                }
            }

            string talkerName = null;
            switch(s.PrimaryTalker)
            {
                case PrimaryTalker.Right:
                    {
                        talkerName = db.GetTalker(s.RightTalker).TalkerName;
                        textHeader.alignment = TextAlignmentOptions.Right;
                        textBody.alignment = TextAlignmentOptions.TopRight;
                        break;
                    }
                case PrimaryTalker.Left:
                    {
                        talkerName = db.GetTalker(s.LeftTalker).TalkerName;
                        textHeader.alignment = TextAlignmentOptions.Left;
                        textBody.alignment = TextAlignmentOptions.TopLeft;
                        break;
                    }
                default:
                    {
                        textHeader.alignment = TextAlignmentOptions.Left;
                        textBody.alignment = TextAlignmentOptions.TopLeft;
                        textHeader.text = "";
                        break;
                    }
            }
            if(talkerName != null)
            {
                textHeader.text = talkerName;
            }
            if(s.Message != null)
            {
                textBody.text = s.Message;
                textPanel.SetActive(true);
            }

            StartCoroutine(WaitTilCanAdvance(timerForTalk));
        }

        public void FinishTalk()
        {
            if(!canAdvanceTalk)
            {
                return;
            }
            EndTalk?.Invoke(this, new EndedTalkEventArgs());
            if (currentLeft != null)
            {
                Destroy(currentLeft);
            }
            if (currentRight != null)
            {
                Destroy(currentRight);
            }
            if (latestSetting.Chain == TalkingChain.EndConversation)
            {
                EndConversation?.Invoke(this, new EndedConversationEventArgs());
                latestSetting = null;
                isTalking = false;
                textPanel.SetActive(false);
                blocker.SetActive(false);
 
            }
            else if(latestSetting.Chain == TalkingChain.ContinueConversation)
            {
                if (conversation.Count > 0)
                {
                    isTalking = false;
                    latestSetting = conversation.Dequeue();
                    Talk(latestSetting);
                }
            }
        }

        private IEnumerator WaitTilCanAdvance(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            canAdvanceTalk = true;
        }

    }



    public class StartedTalkEventArgs
    {

    }

    public class EndedTalkEventArgs
    {

    }

    public class StartedConversationEventArgs
    {

    }

    public class EndedConversationEventArgs
    {

    }

    [Serializable]
    public class TalkingSettings
    {
        public string Message;
        public int RightTalker = -1;
        public int LeftTalker = -1;
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