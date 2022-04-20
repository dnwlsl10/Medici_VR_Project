using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    RaycastHit hitOut;
    public WireBox wireBox;

    public void Start()
    {
    
    }
    private void Update()
    {
        this.OnRayCast();
    }


    void OnActionWireBox()
    {
        if (hitOut.collider.CompareTag("RedWire"))
        {
            RedWire redWire = hitOut.collider.GetComponentInParent<RedWire>();
            redWire.Init(() =>
            {
                redWire.OnCompare(redWire.tag);
            });

        }
        else if (hitOut.collider.CompareTag("GreenWire"))
        {
            GreenWire greenWire = hitOut.collider.GetComponentInParent<GreenWire>();
            greenWire.Init(() =>
            {
                greenWire.OnCompare(greenWire.tag);
            });

        }
        else if (hitOut.collider.CompareTag("BlueWire"))
        {
            BlueWire blueWire = hitOut.collider.GetComponentInParent<BlueWire>();
            blueWire.Init(() =>
            {
                blueWire.OnCompare(blueWire.tag);
            });

        }
        else if (hitOut.collider.CompareTag("YellowWire"))
        {
            YellowWire yellowWire = hitOut.collider.GetComponentInParent<YellowWire>();
            yellowWire.Init(() =>
            {
                yellowWire.OnCompare(yellowWire.tag);
            });
        }
    }

    void OnActionButtonBox()
    {
        if (hitOut.collider.CompareTag("Window"))
        {
            BreakableWindow window = hitOut.collider.gameObject.GetComponent<BreakableWindow>();
            window.OnBreakWindow();
        }
    }

    void OnActionImageBox()
    {

    }

    void OnRayCast()
    {
       
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitOut))
            {

                Debug.Log(hitOut.transform.tag);
                this.OnActionButtonBox();
                this.OnActionWireBox();
                this.OnActionImageBox();
            }
        }
    }
}
