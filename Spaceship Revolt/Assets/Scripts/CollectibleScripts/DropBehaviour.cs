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
        tempReference.drag = 0.5f;
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
        gameObject.GetComponent<Rigidbody2D>().drag = 3f;
    }
    public void StaticLock()
    {
        Rigidbody2D tempReference = gameObject.GetComponent<Rigidbody2D>();
        tempReference.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY| RigidbodyConstraints2D.FreezeRotation;
    }
}
