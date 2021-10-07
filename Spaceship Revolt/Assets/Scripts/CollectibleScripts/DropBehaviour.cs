using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBehaviour : MonoBehaviour
{
    public void DroppedCargo(Vector2 newPosition, Vector2 force)
    {
        gameObject.SetActive(true);
        gameObject.layer = 8;
        gameObject.transform.position = newPosition;
        Rigidbody2D tempReference = gameObject.GetComponent<Rigidbody2D>();
        switch (gameObject.tag)
        {
            case "Worker":
                tempReference.drag = 0.5f;
                tempReference.mass = 1;
                break;
            case "Research":
                tempReference.drag = 0.5f;
                tempReference.mass = 1;
                break;
            case "WarCentre":
                tempReference.drag = 0.5f;
                tempReference.mass = 1;
                break;
            default:
                break;
        }
        tempReference.constraints = RigidbodyConstraints2D.None;
        tempReference.constraints = RigidbodyConstraints2D.FreezeRotation;
        tempReference.AddForce(force, ForceMode2D.Impulse);
        Invoke("TurnOnCollider", 1f);
        Invoke("AnchorNow", 0.5f);
        Invoke("StaticLock", 0.9f);
    }
    public void TurnOnCollider()
    {
        gameObject.layer = 0;
    }

    public void AnchorNow()
    {
        Rigidbody2D tempReference = gameObject.GetComponent<Rigidbody2D>();
        switch (gameObject.tag)
        {
            case "Worker":
                tempReference.drag = 3f;
                tempReference.mass = 500f;
                break;
            case "Research":
                tempReference.drag = 3f;
                tempReference.mass = 10000;
                break;
            case "WarCentre":
                tempReference.drag = 3f;
                tempReference.mass = 10000;
                break;
            default:
                break;
        }
    }
    public void StaticLock()
    {
        Rigidbody2D tempReference = gameObject.GetComponent<Rigidbody2D>();
        switch (gameObject.tag)
        {
            case "Worker":
                tempReference.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
            case "Research":
                tempReference.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
            case "WarCentre":
                tempReference.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
            default:
                break;
        }

    }
}
