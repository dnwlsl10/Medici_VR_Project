using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject[] code;

    private void Start()
    {
        int randomIndex = Random.Range(0, code.Length);
        code[randomIndex].SetActive(true);
        
    }
}
