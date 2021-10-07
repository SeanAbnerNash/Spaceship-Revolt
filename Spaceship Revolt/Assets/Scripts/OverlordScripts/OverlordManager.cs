using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlordManager : MonoBehaviour
{
    [SerializeField]
    private float currentProgress = 0f;
    [SerializeField]
    private float progressPerSecond = 5f;
    [SerializeField]
    private float maxProgress;
    public Slider progressBar;

    [SerializeField]
    private bool defeated = false;


    void Start()
    {
        maxProgress = GameData.Instance.GetMaxOverlordProgression();
        GameData.Instance.SetCurrentOverlordProgression(currentProgress);
        GameEvents.current.onCounterCompletion += DefeatCompleted;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgress();
        UpdateUI();

    }


    private void UpdateProgress()
    {
        if(!defeated)
        {
            if (currentProgress < maxProgress)
            {
                currentProgress += progressPerSecond * Time.deltaTime;
                GameData.Instance.SetCurrentOverlordProgression(currentProgress);
            }
            else
            {
                Debug.Log("Game Over");
            }
        }
    }

    public void DefeatCompleted()
    {
        defeated = true;
        Debug.Log("AI DEFEATED SUCCESS!");
    }


    private void UpdateUI()
    {
        progressBar.value = currentProgress / maxProgress;
    }    

}
