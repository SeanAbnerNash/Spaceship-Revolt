using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTERED ");
    }

    public void DoSomething()
    {
        Debug.Log("ENTERED  C");
    }

}
