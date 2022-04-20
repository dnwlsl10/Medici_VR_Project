using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WireBox : MonoBehaviour
{
    protected List<string> candidate = new List<string>();

    string[] question = { "RedWire", "BlueWire", "GreenWire", "YellowWire" };
    int count = 0;
    public virtual void OnCompare(string tag)
    {

        Debug.Log(question.Length);
        Debug.Log("2");
        candidate.Add(tag);
        Debug.Log(count);
        Debug.Log(question[count]);
        if (question[count] == candidate[count])
        {
            Debug.LogFormat("{0} 번째 성공", count);
            count++;
        }
        else
        {
            Debug.Log(question[count]);
            Debug.Log(candidate[count]);
            Debug.Log("실패");

        }
    }
}
