using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [HideInInspector]
    public GrabberBase hand;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    virtual public void Catch(GrabberBase whereHand)
    {
        //잡은 손을 기억하겠다.
        hand = whereHand;
        if (whereHand)
        {
            rb.useGravity = false;
        }
    }
    virtual public void Release()
    {
        if (hand)
        {
            rb.useGravity = true;
            hand.PutDown();
        }
    }
}
