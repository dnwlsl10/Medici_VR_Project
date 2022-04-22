using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWire : MonoBehaviour
{
    public Outline[] outlines;
    public GameObject effect;
    public GameObject point;
    public System.Action<string> OnAction;
    public void Init()
    {
        if(!this.point.activeSelf)
        {
            return;
        }
        this.effect.SetActive(true);
        this.point.SetActive(false);
        OnAction(this.gameObject.tag);
    }

    public void OnOutline()
    {
        for(int i =0; i<outlines.Length; i++)
        {
            outlines[i].OnRayCastEnter();
        }
    }

    public void OffOutline()
    {
        for(int i =0; i<outlines.Length; i++)
        {
            outlines[i].OnRayCastExit();
        }
    }
}
