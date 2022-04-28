using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{
    public System.Action OnCompelet;
    public bool isChangeScence;
    public void Update()
    {
        if (BombManager.instance.isChangeScence)
        {
            OnCompelet();
        }
    }


}
