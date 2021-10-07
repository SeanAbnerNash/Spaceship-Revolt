using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTriggerEnter : MonoBehaviour
{
    public string ID;

    //0 Is Research, 1 Is War Centre, 2 Is Manufacturing
    public int typeOfWorkStation;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch(typeOfWorkStation)
        {
            case 0:
                GameEvents.current.SlotDrop(ID, collision.gameObject);
                break;
            case 1:
                GameEvents.current.SlotDrop(ID, collision.gameObject);
                break;
            case 2:
                break;
            default:
                Debug.Log("BUGGED OUT DROP SLOT");
                break;
        }

    }
}
