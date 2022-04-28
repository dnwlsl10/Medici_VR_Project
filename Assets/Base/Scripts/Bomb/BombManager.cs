using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombManager : MonoBehaviour
{
    public GameObject bombMode;
    public bool isGameFail;
    public bool isGameSuccess;
    public bool isBombState;
    public static BombManager instance;
    public static event System.Action OnFailEventLight;
    public bool isFail;
    public bool isSucess;
    public bool isChangeScence;

    public System.Action OnFailed;

    [SerializeField] private bool isSucessButtonBox;
    [SerializeField] private bool isSucessImageBox;
    [SerializeField] private bool isSucessWireBox;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
    }
    void Update()
    {
        if (isSucessButtonBox && isSucessImageBox && isSucessWireBox)
        {
            OnAllSucess();
        }

        if (this.isBombState == true)
        {
            bombMode.SetActive(true);
        }

        if (this.isBombState == false)
        {
            bombMode.SetActive(false);
        }




    }

    //실패 메서드 
    public void OnFailEvent()
    {
        OnFailEventLight();
        OnFailed();
    }



    public void OnFailImageBox()
    {
        isSucessImageBox = false;
        Debug.Log("이미지 폭탄 미션 실패");

        OnFailEvent();
    }

    public void OnFailWireBox()
    {
        isSucessWireBox = false;
        Debug.Log("전선 폭탄 미션 실패");

        OnFailEvent();
    }

    public void OnFailButtonBox()
    {
        isSucessButtonBox = false;
        Debug.Log("버튼 폭탄 미션 실패");

        OnFailEvent();
    }



    //성공 메서드 
    public void OnSucessEvent()
    {
        this.isBombState = false;
        StartCoroutine(OnSucessEvnetSound());
    }

    IEnumerator OnSucessEvnetSound()
    {
        yield return new WaitForSeconds(3f);
        isGameSuccess = true;
    }
    public void OnSucessImageBox()
    {
        isSucessImageBox = true;
        Debug.Log("이미지 폭탄 미션 완료");
    }

    public void OnSucessWireBox()
    {
        isSucessWireBox = true;
        Debug.Log("전선 폭탄 미션 완료");
    }

    public void OnSucessButtonBox()
    {
        isSucessButtonBox = true;
        Debug.Log("버튼 폭탄 미션 완료");
    }

    public void OnAllSucess()
    {
        Debug.Log("폭탄해체 완료");
        isSucess = true;
        OnSucessEvent();
    }
}
