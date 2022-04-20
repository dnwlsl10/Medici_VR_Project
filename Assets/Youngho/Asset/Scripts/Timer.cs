using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer instance;

    [SerializeField]
    Text time;

    [SerializeField]
    float startTime;

    public int curTime;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        curTime = (int)startTime;
        StartCoroutine(IntTimer());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator IntTimer()
    {
        while ( curTime > 0)
        {
            curTime -= 1;
            time.text =  (curTime / 60)   + ":" + (curTime % 60);
            yield return new WaitForSeconds(1);
        }
    }
}
