using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pattern
{
    public string answer;
}


public class WireBox : MonoBehaviour
{
    
    List<string> candidate = new List<string>();
    List<string> test = new List<string>();
    [SerializeField]
    private Pattern[] patern;
    int count = 0;
    private void Start()
    { 
        for (int i = 0; i < patern.Length; i++)
        {
            test.Add(patern[i].answer);
        }

    }
    private void Update()
    {
        this.OnClick();
    }

    void OnClick()
    {
        RaycastHit hitOut;
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitOut))
            {
               
                Debug.Log(hitOut.transform.tag);
                if (hitOut.collider.CompareTag("RedWire"))
                {
                    RedWire redWire = hitOut.collider.GetComponentInParent<RedWire>();
                    redWire.Init(()=> {
                        this.OnCompare(hitOut.collider.tag);
                    });
                }
                else if (hitOut.collider.CompareTag("GreenWire"))
                {
                    GreenWire greenWire = hitOut.collider.GetComponentInParent<GreenWire>();
                    greenWire.Init(()=> {
                        this.OnCompare(hitOut.collider.tag);
                    });
                }
                else if (hitOut.collider.CompareTag("BlueWire"))
                {
                    BlueWire buleWire = hitOut.collider.GetComponentInParent<BlueWire>();
                    buleWire.Init(()=> {
                        this.OnCompare(hitOut.collider.tag);
                    });
                }
                else if (hitOut.collider.CompareTag("YellowWire"))
                {
                    YellowWire yellowWire = hitOut.collider.GetComponentInParent<YellowWire>();
                    yellowWire.Init(()=> {
                        this.OnCompare(hitOut.collider.tag);
                    });
                }
            }
        }
    }


    void OnCompare(string tag)
    {
        Debug.Log("3");
        candidate.Add(tag);
        Debug.Log(count);
        if (test[count] == candidate[count])
        {
            Debug.LogFormat("{0} 번째 성공", count);
            count++;
        }
        else
        {
            Debug.Log(test[count]);
            Debug.Log(candidate[count]);
            Debug.Log("실패");
          
        }
    }
}
