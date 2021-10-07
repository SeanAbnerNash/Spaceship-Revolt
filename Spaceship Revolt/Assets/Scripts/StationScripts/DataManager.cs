using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public static DataManager current;
    void Awake()
    {
        current = this;
    }

    public event Action<string, GameObject> onResearchDrop;
    public void ResearchDrop(string t_id, GameObject t_droppedObject)
    {
        if (onResearchDrop != null)
        {
            onResearchDrop(t_id, t_droppedObject);
        }
    }

    public event Action<string> onResearchCompletion;
    public void ResearchCompleted(string t_id)
    {
        if (onResearchCompletion != null)
        {
            onResearchCompletion(t_id);
        }
    }
}
