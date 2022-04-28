using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public System.Action onComplete;
    public void Init()
    {
        StartCoroutine(DisplayTitle());
    }

    IEnumerator DisplayTitle()
    {
        yield return new WaitForSeconds(1f);
        this.onComplete();
    }
}
