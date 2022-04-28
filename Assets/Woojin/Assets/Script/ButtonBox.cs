using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ButtonBox : MonoBehaviour, BombInterface
{
    // button push and pull position
    Vector3 defaultPosition;
    Vector3 pushedPosition;
    [SerializeField]
    float pushedZDistance;


    // button push and pull time
    [SerializeField]
    float buttonPushTime = 2f;
    [SerializeField]
    float buttonPullTime = 1f;

    // light GameObjects
    [SerializeField]
    GameObject button;
    [SerializeField]
    GameObject light;
    [SerializeField]
    GameObject head;



    // Patterns about Bomb
    [SerializeField]
    List<ButtonBombPattern> patterns;
    ButtonBombPattern thisGamePattern;

    // timer relate
    string nowSecond;


    //light
    public Lights buttonBoxLight;
    public Outline outline;

    //Bomb State
    public enum State
    {
        Normal,
        Warnning,
        Wrong,
        Success
    }

   public State state;

    void  Start()
    {
       
        // ButtonPosition Setting
       
        //pullBack = new ButtonDeligate(PullButtonAndLightOff);

        thisGamePattern = patterns[Random.Range(0, patterns.Count)];

        SetBomb(thisGamePattern);

        state = State.Normal;
    }


    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PushButton();
                    NormalStateTryDefuse();
                }
                break;
            case State.Warnning:
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    PullButton();
                    WarnningStateKeyUp();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    PushButton();

                    WarnningStateKeyDown();
                }
                break;
            case State.Wrong:
                break;
            case State.Success:
                break;
        }
    }

    public void WarnningStateKeyDown()
    {
        bool isSuccess = false;
        string second = ((int)(kdy_Timer.instance.TIME % 60 )).ToString();

        switch (thisGamePattern.patternNum)
        {
            case 1:
            case 2:
                if (second == thisGamePattern.endTime)
                {
                    isSuccess = true;
                }
                CheckWarnningSuccessAndChangeState(isSuccess);
                break;
            default:
                isSuccess = true;
                break;
        }

        if (!isSuccess)
        {
            Fail();
        }
    }

    public void WarnningStateKeyUp()
    {
        bool isSuccess = false;
        string second = ((int)(kdy_Timer.instance.TIME % 60)).ToString();

        switch (thisGamePattern.patternNum)
        {
            case 0:
            case 3:
            case 4:
                if (second == thisGamePattern.endTime)
                {
                    isSuccess = true;
                }
                CheckWarnningSuccessAndChangeState(isSuccess);
                break;
            case 1:
            case 2:
                isSuccess = true;
                break;
        }

        if (!isSuccess)
        {
            Fail();
        }

    }

    public bool NormalStateTryDefuse()
    {
        bool isSuccess = false;
        //Timer 들어갈 곳.
        string temp = ((int)(kdy_Timer.instance.TIME % 60)).ToString();
        Debug.Log(temp);
        isSuccess = FirstTimeCheck(temp);
        Debug.Log(isSuccess);
        CheckNormalSuccessAndChangeState(isSuccess);

        return isSuccess;
    }


    private bool FirstTimeCheck(string second)
    {
        bool isSuccess = false;
        if (second == thisGamePattern.startTime)
        {
            isSuccess = true;
        }
        return isSuccess;

    }

    private void CheckNormalSuccessAndChangeState(bool isSuccess)
    {
        if (isSuccess)
        {
            state = State.Warnning;
            ChangeLightColor(thisGamePattern.lightColor);
            
        }
        else
        {
            state = State.Wrong;
            ChangeLightColor(Color.red);
            Fail();
        }
    }

    private void CheckWarnningSuccessAndChangeState(bool isSuccess)
    {
        if (isSuccess)
        {
            state = State.Success;
            ChangeLightColor(Color.green);
            Success();
        }
        else
        {
            state = State.Wrong;
            ChangeLightColor(Color.red);
        }
    }

    private void SetBomb(ButtonBombPattern thisGamePattern)
    {
        head.GetComponent<MeshRenderer>().material.color = thisGamePattern.buttonColor;

        button.GetComponentInChildren<Text>().text = thisGamePattern.buttonText;
    }

    void ChangeLightColor(Color color)
    {
        light.GetComponent<MeshRenderer>().material.color = color;
    }



    public void PushButton()
    {
        defaultPosition = button.gameObject.transform.position;
        pushedPosition = defaultPosition + button.gameObject.transform.up * pushedZDistance;
        isButtonPush = true;
        StartCoroutine(IEPushButton());

    }

    IEnumerator IEPushButton()
    {
        float timer = 0;
        while (timer < buttonPushTime && isButtonPush)
        {
            timer += Time.deltaTime;
            button.transform.position = Vector3.Lerp(button.transform.position, pushedPosition, 0.05f);
            yield return 0;
        }

    }

    bool isButtonPush;

    public void PullButton()
    {
        pushedPosition = button.transform.position;
        Debug.Log("UpUPUPUPUPUPUPUPUPUP");
        isButtonPush = false;
        StopCoroutine(IEPushButton());
        StartCoroutine(IEPullButton());
    }


    IEnumerator IEPullButton()
    {
        float timer = 0;
        while (timer < buttonPullTime)
        {
            timer += Time.deltaTime;
            button.transform.position = Vector3.Lerp(button.transform.position, defaultPosition, 0.05f);
            yield return 0;
        }

    }




    public bool Fail()
    {
        SoundManager.instance.PlaySound(Camera.main.transform.position, "FaildSound");

        buttonBoxLight.OnFail(() =>
        {
            BombManager.instance.OnFailButtonBox();
        });
        return false;
    }

    public bool Success()
    {
        SoundManager.instance.PlaySound(Camera.main.transform.position, "SuccessedSound");
        buttonBoxLight.OnSucess();
        BombManager.instance.OnSucessButtonBox();
        print("성공시 나올 자립니다.");
        return true;
    }


}



[Serializable]
struct ButtonBombPattern
{
    public int patternNum;
    public Color buttonColor;
    public Color lightColor;
    public string buttonText;
    public string startTime;
    public string endTime;
    public bool isKeepButtonPush;

    public ButtonBombPattern(int patternNum, Color buttonColor, Color lightColor, string buttonText, string startTime, string endTime, bool isKeepButtonPush)
    {
        this.patternNum = patternNum;
        this.buttonColor = buttonColor;
        this.lightColor = lightColor;
        this.buttonText = buttonText;
        this.startTime = startTime;
        this.endTime = endTime;
        this.isKeepButtonPush = isKeepButtonPush;
    }
}



