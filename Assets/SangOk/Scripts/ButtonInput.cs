using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInput : MonoBehaviour
{
    public Lights buttonBoxLight;
    public GameObject[] buttons;
    string[] objName = new string[4];
    public int clickCount = 4;
    string[] correctAnswer = new string[4];
    public GameObject buttonInputSound;
    public GameObject failedSound;
    public GameObject successedSound;
    public Outline[] outlines;


    enum State
    {
        Success,
        Fail,
        Playing

    }
    State state;


    //오브젝트를 클릭하면 그 오브젝트의 이름을 순서대로 리스트에 저장하고 싶다.
    //순서대로 입력받은 리스트값이 정답 리스트값과 같으면 통과하고싶다.
    //그렇지않거나 클릭카운트가 0이면 입력받은 리스트를 초기화하고 초기상태로 돌아가고 싶다.

    void Start()
    {
        correctAnswer[0] = "A";
        correctAnswer[1] = "B";
        correctAnswer[2] = "C";
        correctAnswer[3] = "D";
        state = State.Playing;
    }



   public void Test(RaycastHit raycastHit)
    {
            buttonInputSound.GetComponent<AudioSource>().Play();
            clickCount--;
        Debug.Log(raycastHit);
        Debug.Log(raycastHit.transform.GetChild(0).gameObject.name);
            raycastHit.collider.gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>()
                        .material.color = Color.green;

            if (objName[0] == null)
            {
                objName[0] = raycastHit.collider.gameObject.name;
              Debug.Log(objName[0]);
            }
            else if (objName[0] != null && objName[1] == null)
            {
                objName[1] = raycastHit.collider.gameObject.name;
            }
            else if (objName[1] != null && objName[2] == null)
            {
                objName[2] = raycastHit.collider.gameObject.name;
            }
            else if (objName[2] != null && objName[3] == null)
            {
                objName[3] = raycastHit.collider.gameObject.name;
            }

            for (int i = 0; i<objName.Length; i++)
            {
                print(objName[i]);
            }
            CompareNameArray();

            ChangeState();

    }

    void CompareNameArray()
    {
        if (clickCount == 0 &&
            objName[0] == correctAnswer[0] &&
            objName[1] == correctAnswer[1] &&
            objName[2] == correctAnswer[2] &&
            objName[3] == correctAnswer[3])
        {
            state = State.Success;
            print(state);
        }
        else if (clickCount == 0 &&
            (objName[0] != correctAnswer[0] ||
            objName[1] != correctAnswer[1] ||
            objName[2] != correctAnswer[2] ||
            objName[3] != correctAnswer[3]))
        {
            state = State.Fail;
            print(state);
        }
    }
    void ChangeState()
    {
        switch (state)
        {
            case State.Success:
                successedSound.GetComponent<AudioSource>().Play();
                buttonBoxLight.OnSucess();
                BombManager.instance.OnSucessImageBox();
                break;
            case State.Fail:

                clickCount = 4;
                objName = new string[4];
                StartCoroutine("FailedGlow", 0);
                failedSound.GetComponent<AudioSource>().Play();
                state = State.Playing;

                buttonBoxLight.OnFail(() =>
                {
                    BombManager.instance.OnFailImageBox();
                });

                break;
        }
    }


    private void Update()
    {
        if (BombManager.instance.isFail)
        {
            for(int i = 0; i< buttons.Length; i++)
            {
                buttons[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    IEnumerator FailedGlow()
    {
        int count = 0;
        while (count < 4)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Renderer>().material.color = Color.red;
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Renderer>().material.color = Color.black;
            }
            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }

}
