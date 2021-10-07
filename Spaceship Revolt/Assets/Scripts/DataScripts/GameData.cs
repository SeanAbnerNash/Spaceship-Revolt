using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : Singleton<GameData>
{
    // (Optional) Prevent non-singleton constructor use.
    protected GameData() { }

    public void Start()
    {
    }

    [SerializeField]
    private float currentOverlordProgression;
    [SerializeField]
    private float maxOverlordProgression = 300f;

    public void SetCurrentOverlordProgression(float t_progression)
    {
        currentOverlordProgression = t_progression;
    }
    public float GetCurrentOverlordProgression()
    {
        return currentOverlordProgression;
    }
    public float GetMaxOverlordProgression()
    {
        return maxOverlordProgression;
    }


    [SerializeField]
    private float currentCounterProgression;
    [SerializeField]
    private float maxCounterProgression = 300f;
    [SerializeField]
    private float workerWarcentreValue = 1f;

    public void SetCurrentCounterProgression(float t_progression)
    {
        currentCounterProgression = t_progression;
        if(currentCounterProgression > maxCounterProgression)
        {
            GameEvents.current.CounterCompleted();
        }
    }
    public float GetCurrentCounterProgression()
    {
        return currentCounterProgression;
    }
    public float GetMaxCounterProgression()
    {
        return maxCounterProgression;
    }
    public float GetWorkerWarCentreValue()
    {
        return workerWarcentreValue;
    }


    //CARGO STUFF
    private List<GameObject> cargoList = new List<GameObject>();

    public void SetCargo(List<GameObject> cargo)
    {
        cargoList = cargo;
        GameEvents.current.CargoChange();
    }

    public List<GameObject> GetCargo()
    {
        return cargoList;
    }


}
