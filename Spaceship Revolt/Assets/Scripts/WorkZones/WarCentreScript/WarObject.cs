using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarObject : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string warID;
    [SerializeField]
    private GameObject warReference;


    [SerializeField]
    private float objectHealth = 100f;

    [SerializeField]
    private float objectUsePerSecond = 2f;

    [SerializeField]
    private bool finished = false;

    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Invoke("StaticLock", 1f);
    }

    public void StaticLock()
    {
        Rigidbody2D tempReference = gameObject.GetComponent<Rigidbody2D>();
        tempReference.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public string GetID()
    {
        return warID;
    }
    public void SetWarCentre(GameObject t_warReference)
    {
        if (t_warReference == null)
        {
            warReference = null;
        }
        else
        {
            warReference = t_warReference;
        }
    }

    public void SetID(string t_ID)
    {
        warID = t_ID;
    }

    public void ObjectFinished()
    {
        if(objectHealth <= 0f)
        {
            finished = true;
            if (warReference != null)
            {
                warReference.GetComponent<WarCentre>().LostObject();
            }
            SetWarCentre(null);
            Destroy(gameObject,0.1f);
        }
    }

    public float UseObject()
    {
        float objectUseValue = 0f;
        float usePerFrame = 0f;

        if(!finished)
        {
            usePerFrame = objectUsePerSecond * Time.deltaTime;
            if(objectHealth < usePerFrame)
            {
                objectUseValue = objectHealth;
            }
            else
            {
                objectUseValue = usePerFrame;
            }
            objectHealth -= objectUseValue;
            ObjectFinished();
        }   
        return objectUseValue;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().TryAddCargo(gameObject))
            {
                //Expand on behaviour here.
                if (warReference != null)
                {
                    warReference.GetComponent<WarCentre>().LostObject();
                }
                SetWarCentre(null);
                gameObject.SetActive(false);
            }
        }
    }
}
