using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : MonoBehaviour
{

    public GameObject bombPrefab;
    public Transform[] bombTarget;
    private void Start()
    {
        Init();
    }


    void Init()
    {
        //ÆøÅº »ý¼º 
        int randValue = Random.Range(0, bombTarget.Length);
        Bomb bomb = Instantiate(bombPrefab, bombTarget[randValue].transform.position, Quaternion.identity).GetComponent<Bomb>();
   

    }
}
