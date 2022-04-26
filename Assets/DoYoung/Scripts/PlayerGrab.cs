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
    Outline outLine;
    public bool isMoused;
    public List<GameObject> highlightedDoors;
    GameObject grapedBomb;
    public float bombRoteAcel = 5;

    private void Start()
    {
        isMoused = false;
    }

    void Update()
    {

        OnLeftHandButton();

        OnLeftHandButtonUp();

        BombHold();

        if (isBombHold)
        {
            RatatebombByHandGrip();
        }

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

                if (hitInfo.transform.tag == "door")
                {
                    if (highlightedDoors.Contains(hitInfo.collider.gameObject))
                    {
                        return;
                    }
                    highlightedDoors.Add(hitInfo.collider.gameObject);
                    outLine = hitInfo.transform.gameObject.AddComponent<Outline>().GetComponent<Outline>();
                    outLine.OnRayCastEnter();
                }
                else
                {
                    ClearOutline();
                }

            }
            else
            {
                lr.SetPosition(1, ray.origin + ray.direction * 10);

            }

        }


    }

    void OnLeftHandButtonUp()
    {
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {

            ClearOutline();

            lr.enabled = false;
            if (hitInfo.transform.tag == "door")
            {
                hitInfo.transform.gameObject.GetComponent<Door>().ActionDoor();

            }
        }
    }
    void BombHold()
    {
        if (isBombHold)
        {
            grapedBomb.transform.position = Vector3.Lerp(grapedBomb.transform.position, bombPosition.position, Time.deltaTime * 2);
        }

        if (hitInfo.transform == null)
        {
            return;
        }

        if (hitInfo.collider.tag == "Bomb")
        {
            isBombHold = true;
            grapedBomb = hitInfo.collider.gameObject;
            grapedBomb.GetComponent<Rigidbody>().isKinematic = true;
            grapedBomb.transform.parent = transform;
        }

    }


    void RatatebombByHandGrip()
    {

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
            Vector3 rotDir = new Vector3(dir.y, -dir.x, 0);
            print("rotateDir =" + dir);
            grapedBomb.transform.eulerAngles += rotDir * bombRoteAcel;
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            Vector3 rotDir = new Vector3(dir.y, -dir.x, 0);
            print("rotateDir =" + dir);
            grapedBomb.transform.eulerAngles += rotDir * bombRoteAcel;
        }
    }

    void ClearOutline()
    {
        for (int i = highlightedDoors.Count - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(highlightedDoors[i].GetComponent<Outline>());
            highlightedDoors.Remove(highlightedDoors[i]);
        }

    }
}
