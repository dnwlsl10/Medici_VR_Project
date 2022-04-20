using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;


    [SerializeField]
    GameObject canvus;

    List<UIS> uiArray;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiArray = new List<UIS>();
        int uiCount = canvus.transform.childCount;

        for (int i = 0; i < uiCount; i++) {
            GameObject ui = canvus.transform.GetChild(i).gameObject;

            UIS uiS = new UIS(ui, ui.name);
            uiArray.Add(uiS);
        }
    }

    // 스크린 명을 써주면 그 스크린을 켜줍니다.
    public void TurnOnUI(string uiNames)
    {
        string[] uiNameArr = uiNames.Split('/');

        foreach (UIS uis in uiArray)
        {
            foreach (var uiName in uiNameArr)
            {
                if (uis.uiName.Contains(uiName))
                {
                    uis.ui.SetActive(true);
                }
            }
        }
    }

    // 스크린 명을 써주면 그 스크린을 꺼줍니다.
    public void TurnOffUI(string uiNames)
    {
        string[] uiNameArr = uiNames.Split('/');

        foreach (UIS uis in uiArray)
        {
            foreach(var uiName in uiNameArr)
            {
                if (uis.uiName.Contains(uiName))
                {
                    uis.ui.SetActive(false);
                }
            }
            
        }
    }
 

}
public struct UIS
{
    public UIS(GameObject ui,string uiName)
    {
        this.ui = ui;
        this.uiName = uiName;
    }

    public GameObject ui;
    public string uiName;
}
