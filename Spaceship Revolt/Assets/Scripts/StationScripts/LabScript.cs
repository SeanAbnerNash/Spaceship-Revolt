using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabScript : MonoBehaviour
{
    // Start is called before the first frame update
    public string labID;

    [SerializeField]
    private int currentWorkers = 0;
    [SerializeField]
    private int maxWorkers = 5;

    public TextMeshProUGUI currentWorkerUI;
    public TextMeshProUGUI maxWorkerUI;
    public Transform researchSlotLocation;

    [SerializeField]
    private float currentResearch = 0f;
    [SerializeField]
    private float totalResearch = 0f;
    [SerializeField]
    private string currentResearchID;
    [SerializeField]
    private Rigidbody2D researchObjectReference = null;
    [SerializeField]
    private float unitResearchMove = 1f;
    void Start()
    {
        GameEvents.current.onSlotDrop += ResolveResearchDrop;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        DoResearch();
        PullResearchObjectToSlot();
    }


    public void DoResearch()
    {

        if(currentResearchID != null && currentWorkers > 0)
        {
            currentResearch = (Mathf.Clamp(currentWorkers, 0, maxWorkers) * ResearchManager.current.techList.researchValueOfWorker) * Time.deltaTime;
            ResearchManager.current.DoResearch(currentResearchID, currentResearch);
        }

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

    public void LostResearch()
    {
        currentResearchID = null;
        researchObjectReference = null;
    }

    public void PullResearchObjectToSlot()
    {
        if (researchObjectReference != null)
        {
            if ((researchObjectReference.position - (Vector2)researchSlotLocation.position).magnitude > 0.25f)
            {
                Vector2 moveObjectVector = ((Vector2)researchSlotLocation.position - researchObjectReference.position).normalized;
                moveObjectVector = (moveObjectVector * unitResearchMove) * Time.deltaTime;
                researchObjectReference.MovePosition((Vector2)researchObjectReference.gameObject.transform.position + moveObjectVector);
            }
            else
            {
                researchObjectReference.MovePosition(researchSlotLocation.position);
            }
        }

    }
    public void ResolveResearchDrop(string t_id, GameObject t_droppedObject)
    {
        if (labID == t_id)
        {
            switch (t_droppedObject.tag)
            {
                case "Research":
                    if (researchObjectReference == null)
                    {
                        ResearchObject tempObject = t_droppedObject.GetComponent<ResearchObject>();
                        currentResearchID = tempObject.GetID();
                        tempObject.SetLab(gameObject);
                        researchObjectReference = t_droppedObject.GetComponent<Rigidbody2D>();
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
