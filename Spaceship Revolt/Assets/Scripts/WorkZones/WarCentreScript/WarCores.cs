using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WarCore", menuName = "ScriptableObjects/War Core", order = 2)]
public class WarCores : ScriptableObject
{
    
    public float researchValueOfWorker;
    public List<ResearchPacket> researchList = new List<ResearchPacket>();
}
