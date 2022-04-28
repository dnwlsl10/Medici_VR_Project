using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : MonoBehaviour
{

    Animator ani;

    private void OnEnable()
    {
        ani = this.GetComponent<Animator>();
    }


}
