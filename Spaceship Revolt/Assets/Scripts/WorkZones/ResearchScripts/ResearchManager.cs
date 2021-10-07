using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ResearchRecord PrivateTechReference;
    [SerializeField]
    public ResearchRecord techList;
    public static ResearchManager current;
    void Awake()
    {
        current = this;
        techList = Instantiate(PrivateTechReference);
    }

    public void DoResearch(string researchID, float researchProgress)
    {
        for (int i = 0; i < techList.researchList.Count; i++)
        {
            if (researchID == techList.researchList[i].researchID)
            {
                techList.researchList[i].currentResearch += researchProgress;
                if (techList.researchList[i].currentResearch > techList.researchList[i].maxResearch)
                {
                    techList.researchList[i].researched = true;
                    GameEvents.current.ResearchCompleted(techList.researchList[i].researchID);
                }

            }
        }
    }

}


