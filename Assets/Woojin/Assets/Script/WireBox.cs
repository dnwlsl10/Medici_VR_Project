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
    string[] question;
    [SerializeField] int count;
    bool isFail;

    public void OnMakePattern(eCode code)
    {
        if (code == eCode.Right)
        {
            question = new string[] { "GreenWire", "BlueWire", "RedWire", "YellowWire" };
        }
        if (code == eCode.Left)
        {
            question = new string[] { "GreenWire", "YellowWire", "RedWire", "BlueWire" };
        }
        if (code == eCode.Up)
        {
            question = new string[] { "RedWire", "BlueWire", "GreenWire", "YellowWire" };
        }
        if (code == eCode.Down)
        {
            question = new string[] { "RedWire", "YellowWire", "GreenWire", "BlueWire" };
        }
        if (code == eCode.Back)
        {
            question = new string[] { "RedWire", "GreenWire", "BlueWire", "YellowWire" };
        }
    }
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
            SoundManager.instance.PlaySound(Camera.main.transform.position, "FaildSound");
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
        SoundManager.instance.PlaySound(Camera.main.transform.position, "SuccessedSound");
        BombManager.instance.OnSucessWireBox();
        return true;
    }
}
