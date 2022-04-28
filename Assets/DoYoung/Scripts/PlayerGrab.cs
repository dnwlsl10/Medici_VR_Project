using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public Transform Lhand;
    public Transform Rhand;
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
    public GameObject photo;

    private void Start()
    {
        isMoused = false;
    }

    float timeyouwant = 0;
    void Update()
    {
        if (BombManager.instance.isGameFail)
        {
            timeyouwant += Time.deltaTime;

            int time = 10;
            if (this.GetComponent<Rigidbody>())
            {
                this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                if (timeyouwant > time)
                {
                    this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                    this.photo.gameObject.SetActive(true);
                }
            }
        }


        if (!BombManager.instance.isBombState)
        {
            OnLeftHandButton();

            OnLeftHandButtonUp();

            OnRightIndexButtonDown();
        }

        BombHold();

        if (isBombHold)
        {
            // RatatebombByHandGrip();
        }

        if (BombManager.instance.isBombState == false)
        {
            this.isBombHold = false;
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
                    doorColorRandom = hitInfo.collider.GetComponentInParent<DoorColorRandom>();
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

            print(highlightedDoors.Count);
            ClearOutline();


            lr.enabled = false;
            if (hitInfo.transform.tag == "door")
            {

                hitInfo.transform.gameObject.GetComponent<Door>().ActionDoor();

            }

        }
    }

    void OnRightIndexButtonDown()
    {

        Ray ray = new Ray(Rhand.position, Rhand.forward);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (hitInfo.transform.tag == "Bomb")
            {
                isBombHold = true;
                BombManager.instance.isBombState = isBombHold;
                grapedBomb = hitInfo.transform.gameObject;
            }
        }

    }
    bool stactBombMode;
    void BombHold()
    {

        if (isBombHold)
        {
            grapedBomb.transform.GetComponent<Rigidbody>().isKinematic = true;
            grapedBomb.transform.parent = transform;
            if (Vector3.Distance(grapedBomb.transform.position, bombPosition.position) < 0.05f)
            {
                stactBombMode = true;
            };
            if (!stactBombMode)
            {
                grapedBomb.transform.position = Vector3.Lerp(grapedBomb.transform.position, bombPosition.position, Time.deltaTime * 2);
                grapedBomb.transform.rotation = bombPosition.rotation;
            }
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

    //     void RatatebombByHandGrip()
    //     {

    //         if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
    //         {
    //             Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch) * bombRoteAcel;
    //             // Vector3 rotDir = new Vector3(dir.y, -dir.x, 0);
    //             // print("rotateDir =" + dir);
    //             // grapedBomb.transform.eulerAngles += rotDir * bombRoteAcel;
    //             grapedBomb.transform.RotateAround(grapedBomb.transform.position, transform.up, -dir.x);
    //             grapedBomb.transform.RotateAround(grapedBomb.transform.position, transform.right, dir.y);
    //         }

    //         if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
    //         {
    //             Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * bombRoteAcel;
    //             // Vector3 rotDir = new Vector3(dir.y, -dir.x, 0);
    //             // print("rotateDir =" + dir);
    //             // grapedBomb.transform.eulerAngles += rotDir * bombRoteAcel;
    //             grapedBomb.transform.RotateAround(grapedBomb.transform.position, transform.up, -dir.x);
    //             grapedBomb.transform.RotateAround(grapedBomb.transform.position, transform.right, dir.y);
    //         }
    //     }
}
