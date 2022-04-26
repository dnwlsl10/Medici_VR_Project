using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Transform Lhand;
    public LineRenderer lr;
    public Transform bombPosition;
    bool isBombHold;
    DoorColorRandom doorColorRandom;
    RaycastHit hitInfo = new RaycastHit();

    void Update()
    {
        OnLeftHandButton();

        OnLeftHandButtonUp();

        BombHold();
    }

    void OnLeftHandButton()
    {
        Ray ray = new Ray(Lhand.position, Lhand.forward);
        lr.SetPosition(0, ray.origin);

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            lr.enabled = true;

            if (Physics.Raycast(Lhand.position, Lhand.forward, out hitInfo))
            {
                lr.SetPosition(1, hitInfo.point);

                if(hitInfo.transform.tag == "door")
                {
                    doorColorRandom =  hitInfo.collider.GetComponentInParent<DoorColorRandom>();
                   // doorColorRandom.OnOutline();
                }
            }

        }
    }

    void OnLeftHandButtonUp()
    {
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            lr.enabled = false;
            if (hitInfo.transform.tag == "door")
            {

                hitInfo.transform.gameObject.GetComponent<Door>().ActionDoor();
               
                if(doorColorRandom != null)
                {
                    doorColorRandom.OffOutline();
                }
       

            }
            else if (hitInfo.transform.tag == "Bomb")
            {
                isBombHold = !isBombHold;
            }
        }
    }
    void BombHold()
    {
        if (hitInfo.transform == null) return;

        if (isBombHold && (hitInfo.collider.tag == "Bomb"))
        {
            hitInfo.transform.GetComponent<Rigidbody>().isKinematic = true;
            hitInfo.transform.parent = transform;
            hitInfo.transform.position = Vector3.Lerp(hitInfo.transform.position, bombPosition.position, Time.deltaTime * 2);
            hitInfo.transform.rotation = bombPosition.rotation;
        }
        else if (!isBombHold && (hitInfo.collider.tag == "Bomb"))
        {
            hitInfo.transform.GetComponent<Rigidbody>().isKinematic = false;
            hitInfo.transform.parent = null;
        }
        else
        {
            return;
        }
    }

}
