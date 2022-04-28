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

    public OVRInput.Controller hand;
    public LineRenderer lr;
    private Ray ray;


    private void OnEnable()
    {
        
    }
    private void Update()
    {
        //this.OnRayCastButtonDown();
        if (BombManager.instance.isBombState)
        {
            this.OcculusRayCast();
        }

    }

    void OcculusRayCast()
    {
        ray = new Ray(transform.position, transform.forward);

        bool isHit = Physics.Raycast(ray, out hitOut);

        lr.SetPosition(0, ray.origin);
        if (isHit)
        {
            Debug.Log(hitOut.collider.tag);
            lr.SetPosition(1, hitOut.point);
            this.OnOutlineWireBox();
            this.OnActionButtonBox();
            this.OnOutlineIMageBox();
            this.OnOutlineArrowBox();
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
            {
                Debug.Log("test");
                this.OnActionWindow();
                this.OnActionWireBox();
                this.OnActionImageBox();
                this.OnActionArrowBox();
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 10);
        }




    }

    void OnRayCastButtonDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitOut))
        {
            this.OnOutlineWireBox();
            this.OnActionButtonBox();
            this.OnOutlineIMageBox();
            this.OnOutlineArrowBox();
            if (Input.GetButtonDown("Fire1"))
            {
                this.OnActionWindow();
                this.OnActionWireBox();
                this.OnActionImageBox();
                this.OnActionArrowBox();
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
            if (redWire != null)
                redWire.OffOutline();
        }

        if (hitOut.collider.CompareTag("GreenWire"))
        {
            greenWire = hitOut.collider.GetComponentInParent<GreenWire>();
            greenWire.OnOutline();
        }
        else
        {
            if (greenWire != null)
                greenWire.OffOutline();
        }

        if (hitOut.collider.CompareTag("BlueWire"))
        {
            blueWire = hitOut.collider.GetComponentInParent<BlueWire>();
            blueWire.OnOutline();
        }
        else
        {
            if (blueWire != null)
                blueWire.OffOutline();

        }

        if (hitOut.collider.CompareTag("YellowWire"))
        {
            yellowWire = hitOut.collider.GetComponentInParent<YellowWire>();
            yellowWire.OnOutline();
        }
        else
        {
            if (yellowWire != null)
                yellowWire.OffOutline();
        }

    }

    void OnActionWireBox()
    {
        if (hitOut.collider.CompareTag("RedWire"))
        {
            SoundManager.instance.PlaySound(Camera.main.transform.position, "CutSound");
            redWire = hitOut.collider.GetComponentInParent<RedWire>();
            redWire.Init();
        }
        else if (hitOut.collider.CompareTag("GreenWire"))
        {
            SoundManager.instance.PlaySound(Camera.main.transform.position, "CutSound");
            greenWire = hitOut.collider.GetComponentInParent<GreenWire>();
            greenWire.Init();

        }
        else if (hitOut.collider.CompareTag("BlueWire"))
        {
            SoundManager.instance.PlaySound(Camera.main.transform.position, "CutSound");
            blueWire = hitOut.collider.GetComponentInParent<BlueWire>();
            blueWire.Init();

        }
        else if (hitOut.collider.CompareTag("YellowWire"))
        {
            SoundManager.instance.PlaySound(Camera.main.transform.position, "CutSound");
            yellowWire = hitOut.collider.GetComponentInParent<YellowWire>();
            yellowWire.Init();
        }
    }

    bool isBreakWindow;
    void OnActionWindow()
    {

        if (hitOut.collider.CompareTag("Window"))
        {
            BreakableWindow window = hitOut.collider.gameObject.GetComponent<BreakableWindow>();
            window.OnBreakWindow();
            isBreakWindow = true;
        }
    }


    void OnActionButtonBox()
    {
        if (isBreakWindow && hitOut.collider.CompareTag("ButtonBox"))
        {
            buttonBox = hitOut.collider.gameObject.GetComponentInParent<ButtonBox>();
            buttonBox.outline.OnRayCastEnter();
            if (buttonBox.state == ButtonBox.State.Normal)
            {
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
                {
                    buttonBox.PushButton();
                    buttonBox.NormalStateTryDefuse();
                    return;
                }

            }



            if (buttonBox.state == ButtonBox.State.Warnning)
            {
                if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, hand))
                {

                    Debug.Log("UpUPUPUPUPUPUPUPUPUP");
                    buttonBox.PullButton();
                    buttonBox.WarnningStateKeyUp();

                    return;
                }

                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, hand))
                {
                    buttonBox.PushButton();
                    buttonBox.WarnningStateKeyDown();
                    return;
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

    Monitor monitor;
    void OnActionArrowBox()
    {
        if (hitOut.collider.CompareTag("RightButton"))
        {
            monitor = hitOut.collider.GetComponentInParent<Monitor>();
            monitor.MoveLight(0, 1);
        }
        else if (hitOut.collider.CompareTag("UpButton"))
        {
            monitor = hitOut.collider.GetComponentInParent<Monitor>();
            monitor.MoveLight(-1, 0);
        }
        else if (hitOut.collider.CompareTag("DownButton"))
        {
            monitor = hitOut.collider.GetComponentInParent<Monitor>();
            monitor.MoveLight(1, 0);
        }
        else if (hitOut.collider.CompareTag("LeftButton"))
        {
            monitor = hitOut.collider.GetComponentInParent<Monitor>();
            monitor.MoveLight(0, -1);
        }
    }

    Outline btnRight;
    Outline btnLeft;
    Outline btnUp;
    Outline btnDown;
    void OnOutlineArrowBox()
    {
        if (hitOut.collider.CompareTag("RightButton"))
        {
            btnRight = hitOut.collider.GetComponent<Outline>();
            btnRight.OnRayCastEnter();
        }
        else
        {
            if (btnRight != null)
                btnRight.OnRayCastExit();
        }

        if (hitOut.collider.CompareTag("UpButton"))
        {
            btnUp = hitOut.collider.GetComponent<Outline>();
            btnUp.OnRayCastEnter();
        }
        else
        {
            if (btnUp != null)
                btnUp.OnRayCastExit();
        }

        if (hitOut.collider.CompareTag("DownButton"))
        {
            btnDown = hitOut.collider.GetComponent<Outline>();
            btnDown.OnRayCastEnter();
        }
        else
        {
            if (btnDown != null)
                btnDown.OnRayCastExit();
        }

        if (hitOut.collider.CompareTag("LeftButton"))
        {
            btnLeft = hitOut.collider.GetComponent<Outline>();
            btnLeft.OnRayCastEnter();
        }
        else
        {
            if (btnLeft != null)
                btnLeft.OnRayCastExit();
        }
    }

}
