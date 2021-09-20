using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardsCursor : MonoBehaviour
{
    private Rigidbody2D playerReference;
    private bool faceCursor = true;
    private Vector2 mousePos;
    public Camera cam;
    void Start()
    {
        if(gameObject.GetComponent<Rigidbody2D>() != null)
        {
            playerReference = gameObject.GetComponent<Rigidbody2D>();
        }
    }


    void Update()
    {
        if(faceCursor)
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void FixedUpdate()
    {
        if (faceCursor)
        {
            Vector2 lookDir = mousePos - playerReference.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;
            playerReference.rotation = angle;
        }
    }
}
