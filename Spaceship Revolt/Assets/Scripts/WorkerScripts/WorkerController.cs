using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool playerAligned = false;
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Invoke("StaticLock", 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StaticLock()
    {
        Rigidbody2D tempReference = gameObject.GetComponent<Rigidbody2D>();
        tempReference.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void SetAllignment(bool align)
    {
        playerAligned = align;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().TryAddCargo(gameObject))
            {
                //Expand on behaviour here.
                playerAligned = true;
                gameObject.SetActive(false);
            }
        }
    }
}
