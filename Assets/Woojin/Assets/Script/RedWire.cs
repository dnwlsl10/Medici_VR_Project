using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWire : WireBox
{
    public GameObject effect;
    public GameObject point;

    public void Init(System.Action callback)
    {
        
        Debug.Log("1");
        this.effect.SetActive(true);
        this.point.SetActive(false);
        callback();
    }

    public override void OnCompare(string tag)
    {
        base.OnCompare(tag);
    }
}
