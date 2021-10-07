using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ResearchList", menuName = "ScriptableObjects/ResearchRecord", order = 1)]
public class ResearchRecord : ScriptableObject
{
    public float researchValueOfWorker;
    public List<ResearchPacket> researchList = new List<ResearchPacket>();
}


[System.Serializable]
public class ResearchPacket
{
    public string researchName;
    public string researchID;
    public float currentResearch;
    public float maxResearch;
    public bool researched;
    public float cargoWeight;
}
