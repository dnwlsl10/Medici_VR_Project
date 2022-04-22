using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WireBox : MonoBehaviour , BombInterface
{

    public RedWire redWire;
    public GreenWire greenWire;
    public BlueWire blueWire;
    public YellowWire yellowWire;
    protected List<string> candidate = new List<string>();
    public Lights wireBoxLight;
    string[] question = { "RedWire", "BlueWire", "GreenWire", "YellowWire" };
    [SerializeField] int count;
    bool isFail;

    void Update()
    {
        redWire.OnAction = (tag) =>
        {
            OnCompare(tag);
        };
        greenWire.OnAction = (tag) =>
        {
            OnCompare(tag);
        };
        blueWire.OnAction = (tag) =>
        {
            OnCompare(tag);
        };
        yellowWire.OnAction = (tag) =>
        {
            OnCompare(tag);
        };
    }

    public void OnCompare(string tag)
    {
        candidate.Add(tag);
        Debug.Log(this.count);
        Debug.Log(question[this.count]);
        Debug.Log(candidate[this.count]);

        if (this.count > 5)
        {
            return;
        }

        if (question[this.count] == candidate[this.count])
        {
            Debug.LogFormat("{0}번째 성공",this.count);
            this.count++;
            Debug.Log(count);
            if(this.count == 4)
            {
                this.Success();
            }
        }
        else
        {
            Debug.Log(question[count]);
            Debug.Log(candidate[count]);
            this.Fail();
        }
    }

    public bool Fail()
    {

        if (!isFail)
        {
            isFail = true;
            wireBoxLight.OnFail(() =>
            {
                BombManager.instance.OnFailWireBox();
            });
        }
      
      
        return false;
    }


    public bool Success()
    {
        wireBoxLight.OnSucess();
        BombManager.instance.OnSucessWireBox();
        return true;
    }
}
