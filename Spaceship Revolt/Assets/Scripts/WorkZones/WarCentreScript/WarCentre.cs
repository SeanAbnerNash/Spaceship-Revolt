using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarCentre : MonoBehaviour
{
    // Start is called before the first frame update
    public string warID;

    [SerializeField]
    private int currentWorkers = 0;
    [SerializeField]
    private int maxWorkers = 5;

    public TextMeshProUGUI currentWorkerUI;
    public TextMeshProUGUI maxWorkerUI;
    public Transform workSlotLocation;

    [SerializeField]
    private float currentWork = 0f;
    [SerializeField]
    private float totalWork = 0f;
    [SerializeField]
    private string currentWorkID;
    [SerializeField]
    private Rigidbody2D workObjectReference = null;
    [SerializeField]
    private float unitObjectMoveMaxSpeed = 1f;
    [SerializeField]
    private bool centreActivated = false;
    [SerializeField]
    private string signalID = null;



    void Start()
    {
        GameEvents.current.onSlotDrop += ResolveObjectDrop;
        GameEvents.current.onSignalSent += SignalRecieved;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (centreActivated)
        {
            DoWork();
            PullWorkObjectToSlot();
        }
    }


    public void DoWork()
    {
        currentWork = 0f;
        if (currentWorkers > 0)
        {
            currentWork = (Mathf.Clamp(currentWorkers, 0, maxWorkers) * GameData.Instance.GetWorkerWarCentreValue()) * Time.deltaTime;
        }
        if (workObjectReference != null)
        {
            currentWork += workObjectReference.gameObject.GetComponent<WarObject>().UseObject();
        }

        GameData.Instance.SetCurrentCounterProgression(GameData.Instance.GetCurrentCounterProgression() + currentWork);
    }

    public void UpdateUI()
    {
        currentWorkerUI.text = Mathf.Clamp(currentWorkers, 0, maxWorkers).ToString();
        maxWorkerUI.text = maxWorkers.ToString();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Worker":

                currentWorkers++;
                UpdateUI();

                break;
            case "Player":
                break;
            default:
                Debug.Log("BUGGED OUT");
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Debug.Log("Exited by " + collision.gameObject.name);
        switch (collision.gameObject.tag)
        {
            case "Worker":
                currentWorkers--;
                UpdateUI();
                break;
            case "Player":
                break;
            default:
                Debug.Log("BUGGED OUT");
                break;
        }
    }

    public void LostObject()
    {
        currentWorkID = null;
        workObjectReference = null;
    }

    public void SignalRecieved(string t_id)
    {
        if(signalID != null && signalID == t_id)
        {
            centreActivated = true;
        }
    }


    public void PullWorkObjectToSlot()
    {
        if (workObjectReference != null)
        {
            if ((workObjectReference.position - (Vector2)workSlotLocation.position).magnitude > 0.25f)
            {
                Vector2 moveObjectVector = ((Vector2)workSlotLocation.position - workObjectReference.position).normalized;
                moveObjectVector = (moveObjectVector * unitObjectMoveMaxSpeed) * Time.deltaTime;
                workObjectReference.MovePosition((Vector2)workObjectReference.gameObject.transform.position + moveObjectVector);
            }
            else
            {
                workObjectReference.MovePosition(workSlotLocation.position);
            }
        }

    }
    public void ResolveObjectDrop(string t_id, GameObject t_droppedObject)
    {
        if (warID == t_id)
        {
            switch (t_droppedObject.tag)
            {
                case "WarCentre":
                    if (workObjectReference == null)
                    {
                        WarObject tempObject = t_droppedObject.GetComponent<WarObject>();
                        currentWorkID = tempObject.GetID();
                        tempObject.SetWarCentre(gameObject);
                        workObjectReference = t_droppedObject.GetComponent<Rigidbody2D>();
                    }
                    break;
                case "Player":
                    break;
                default:
                    Debug.Log("BUGGED OUT");
                    break;
            }
        }
    }
}
