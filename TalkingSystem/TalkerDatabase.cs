using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ervean.Utilities.DesignPatterns.SingletonPattern;
namespace Ervean.Utilities.Talking
{
    public class TalkerDatabase : Singleton<TalkerDatabase>
    {
        [SerializeField] private List<TalkerData> talkers;


        private void OnValidate()
        {
            HashSet<int> takenIds = new HashSet<int>();



            if (talkers != null && talkers.Count > 0)
            {
                if (talkers.Count > 2)
                {
                    if (talkers[talkers.Count - 1]  == talkers[talkers.Count - 2]) // usually occurs when adding a new entry into the database
                    {
                        return;
                    }
                }

                foreach (TalkerData td in talkers)
                {
                    if (td != null)
                    {
                        if (takenIds.Contains(td.Id))
                        {
                            ProvideId(td);
                        }
                        else
                        {
                            takenIds.Add(td.Id);
                        }
                    }
                }
            }
        }

   
        public TalkerData GetTalker(int id)
        {
            foreach(TalkerData t in talkers)
            {
                if(t.Id == id)
                {
                    return t;
                }
            }
            return null;
        }

        private void ProvideId(TalkerData td)
        {
            HashSet<int> ids = new HashSet<int>();
            foreach (TalkerData a in talkers)
            {
                ids.Add(a.Id);
            }
            int id = 0;

            while (true)
            {
                if (ids.Contains(id))
                {
                    id++;
                }
                else
                {
                    td.SetId(id);
                    return;
                }
            }
        }


    }
}