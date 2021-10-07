using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string researchID;
    [SerializeField]
    private GameObject labReference;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Invoke("StaticLock", 1f);

        GameEvents.current.onResearchCompletion += ResearchFinished;
    }

    public void StaticLock()
    {
        Rigidbody2D tempReference = gameObject.GetComponent<Rigidbody2D>();
        tempReference.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public string GetID()
    {
        return researchID;
    }
    public void SetLab(GameObject t_labReference)
    {
        if (t_labReference == null)
        {
            labReference = null;
        }
        else
        {
            labReference = t_labReference;
        }
    }

    public void SetID(string t_ID)
    {
        researchID = t_ID;
    }

    public void ResearchFinished(string t_id)
    {
        Debug.Log("EVENT ID: " + t_id + " PERSONAL ID: " + researchID );
        if (t_id == researchID)
        {
            Debug.Log("HERE");
            if (labReference != null)
            {
                labReference.GetComponent<LabScript>().LostResearch();
            }
            SetLab(null);
            Destroy(gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().TryAddCargo(gameObject))
            {
                //Expand on behaviour here.
                if(labReference != null)
                {
                    labReference.GetComponent<LabScript>().LostResearch();
                }
                SetLab(null);
                gameObject.SetActive(false);
            }
        }
    }
}
