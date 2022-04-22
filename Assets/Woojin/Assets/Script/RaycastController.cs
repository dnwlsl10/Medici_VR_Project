using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastController : MonoBehaviour
{
    RaycastHit hitOut;
    private RedWire redWire;
    private GreenWire greenWire;
    private BlueWire blueWire;
    private YellowWire yellowWire;
    private ButtonBox buttonBox;
    private void Update()
    {
        this.OnRayCastButtonDown();
    }

    void OnRayCastButtonDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitOut))
        {
            this.OnOutlineWireBox();
            this.OnActionButtonBox();
            this.OnOutlineIMageBox();

            if (Input.GetButtonDown("Fire1"))
            {
                this.OnActionWindow();
                this.OnActionWireBox();
                this.OnActionImageBox();
            }
        }
    }

    void OnOutlineWireBox()
    {
        if (hitOut.collider.CompareTag("RedWire"))
        {
            redWire = hitOut.collider.GetComponentInParent<RedWire>();
            redWire.OnOutline();
        }
        else
        {
            if(redWire != null)
            redWire.OffOutline();
        }

        if (hitOut.collider.CompareTag("GreenWire"))
        {
            greenWire = hitOut.collider.GetComponentInParent<GreenWire>();
            greenWire.OnOutline();
        }
        else
        {
            if(greenWire != null)
            greenWire.OffOutline();
        }

        if (hitOut.collider.CompareTag("BlueWire"))
        {
            blueWire = hitOut.collider.GetComponentInParent<BlueWire>();
            blueWire.OnOutline();
        }
        else
        {
            if(blueWire != null)
            blueWire.OffOutline();

        }

        if (hitOut.collider.CompareTag("YellowWire"))
        {
            yellowWire = hitOut.collider.GetComponentInParent<YellowWire>();
            yellowWire.OnOutline();
        }
        else
        {
            if(yellowWire != null)
            yellowWire.OffOutline();
        }

    }

    void OnActionWireBox()
    {
        if (hitOut.collider.CompareTag("RedWire"))
        {
            redWire = hitOut.collider.GetComponentInParent<RedWire>();
            redWire.Init();
        }
        else if (hitOut.collider.CompareTag("GreenWire"))
        {
            greenWire = hitOut.collider.GetComponentInParent<GreenWire>();
            greenWire.Init();

        }
        else if (hitOut.collider.CompareTag("BlueWire"))
        {
            blueWire = hitOut.collider.GetComponentInParent<BlueWire>();
            blueWire.Init();

        }
        else if (hitOut.collider.CompareTag("YellowWire"))
        {
            yellowWire = hitOut.collider.GetComponentInParent<YellowWire>();
            yellowWire.Init();
        }
    }


    void OnActionWindow()
    {
        if (hitOut.collider.CompareTag("Window"))
        {
            BreakableWindow window = hitOut.collider.gameObject.GetComponent<BreakableWindow>();
            window.OnBreakWindow();
        }
    }


    void OnActionButtonBox()
    {
        if (hitOut.collider.CompareTag("ButtonBox"))
        {
            buttonBox = hitOut.collider.gameObject.GetComponentInParent<ButtonBox>();
            buttonBox.outline.OnRayCastEnter();
            if (buttonBox.state == ButtonBox.State.Normal)
            {
                if(Input.GetButtonDown("Fire1"))
                {
                    buttonBox.PushButton();
                    buttonBox.NormalStateTryDefuse();
                }
               
            }

            if (buttonBox.state == ButtonBox.State.Warnning)
            {
                if (Input.GetButtonUp("Fire1"))
                {
                   buttonBox.PullButton();
                   buttonBox.WarnningStateKeyUp();
                }

                if (Input.GetButtonDown("Fire1"))
                {
                    buttonBox.PushButton();

                    buttonBox.WarnningStateKeyDown();
                }
            }
        }
        else
        {
            if (buttonBox != null)
            {
                buttonBox.outline.OnRayCastExit();
            }
        }
    }

    Outline btnA;
    Outline btnB;
    Outline btnC;
    Outline btnD;

    void OnOutlineIMageBox()
    {
        if (hitOut.collider.gameObject.name == "A")
        {
            btnA = hitOut.collider.GetComponent<Outline>();
            btnA.OnRayCastEnter();
        }
        else
        {
            if (btnA != null)
                btnA.OnRayCastExit();
        }

        if (hitOut.collider.gameObject.name == "B")
        {
            btnB = hitOut.collider.GetComponent<Outline>();
            btnB.OnRayCastEnter();
        }
        else
        {
            if (btnB != null)
                btnB.OnRayCastExit();
        }

        if (hitOut.collider.gameObject.name == "C")
        {
            btnC = hitOut.collider.GetComponent<Outline>();
            btnC.OnRayCastEnter();
        }
        else
        {
            if (btnC != null)
                btnC.OnRayCastExit();
        }

        if (hitOut.collider.gameObject.name == "D")
        {
            btnD = hitOut.collider.GetComponent<Outline>();
            btnD.OnRayCastEnter();
        }
        else
        {
            if (btnD != null)
                btnD.OnRayCastExit();
        }

    }

    ButtonInput IMageBox;
    void OnActionImageBox()
    {
        if (hitOut.collider.CompareTag("buttons"))
        {
            IMageBox = hitOut.collider.GetComponentInParent<ButtonInput>();
            IMageBox.Test(hitOut);
        }
    }


}
