using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public static BombManager instance;

    public bool isFail;

    private void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        isFail = false;
    }

    // Update is called once per frame
    void Update()
    {
        if( isFail )
        {
            print("실패코드");
        }
    }
}
