using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalButton : MonoBehaviour
{
    [SerializeField]
    private bool oneUseSwitch = true;
    private bool switchDown = true;
    public string signalID = null;
    private Animator switchAnimator;

    // Start is called before the first frame update
    void Start()
    {
        switchAnimator = gameObject.GetComponent<Animator>();
    }


    public void EmitSignal()
    {
        if(signalID != null)
        {
            GameEvents.current.SignalSent(signalID);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(switchDown == true || oneUseSwitch == false)
            {
                switchDown = !switchDown;
                switchAnimator.SetTrigger("SwitchState");
                EmitSignal();
            }
        }
    }
}
