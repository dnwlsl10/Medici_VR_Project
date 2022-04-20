using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWire : MonoBehaviour
{
    public GameObject effect;
    public GameObject point;
    private System.Action OnComplete;
    public void Init(System.Action callback)
    {
        Debug.Log("1");
        this.effect.SetActive(true);
        this.point.SetActive(false);
        callback();
    }
}
