using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    void Awake()
    {
        current = this;
    }

    public event Action<string, GameObject> onSlotDrop;
    public void SlotDrop(string t_id, GameObject t_droppedObject)
    {
        if (onSlotDrop != null)
        {
            onSlotDrop(t_id, t_droppedObject);
        }
    }

    public event Action <string> onResearchCompletion;
    public void ResearchCompleted(string t_id)
    {
        if (onResearchCompletion != null)
        {
            onResearchCompletion(t_id);
        }
    }

    public event Action onCounterCompletion;
    public void CounterCompleted()
    {
        if (onCounterCompletion != null)
        {
            onCounterCompletion();
        }
    }

    public event Action<string> onSignalSent;
    public void SignalSent(string t_id)
    {
        if (onSignalSent != null)
        {
            onSignalSent(t_id);
        }
    }

    public event Action onCargoChange;
    public void CargoChange()
    {
        if (onCargoChange != null)
        {
            onCargoChange();
        }
    }
}
