using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateManager : MonoBehaviour
{
    public GameObject rotateCube;

    bool isRotateState;
    public Transform rotatePoint;

    private Vector3 FirstTouch;
    private Vector3 LastTouch;


    // Start is called before the first frame update
    void Start()
    {

    }
    GameObject grapedBomb;
    public float bombRoteAcel = 5;
    // Update is called once per frame
    void Update()
    {
        if (BombManager.instance.isBombState)
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
            {
                Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch) * bombRoteAcel;
                // Vector3 rotDir = new Vector3(dir.y, -dir.x, 0);
                // print("rotateDir =" + dir);
                // grapedBomb.transform.eulerAngles += rotDir * bombRoteAcel;
                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.up, -dir.x);
                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.right, dir.y);
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * bombRoteAcel;
                // Vector3 rotDir = new Vector3(dir.y, -dir.x, 0);
                // print("rotateDir =" + dir);
                // rotateCube.transform.eulerAngles += rotDir * bombRoteAcel;
                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.up, -dir.x);
                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.right, dir.y);
            }
        }

        // if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        // {
        //     Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        //     print(dir.x + "//" + dir.y);
        //     SetParentRotationZeroAndSetPosition();
        //     SetCubeParent();
        //     rotatePoint.eulerAngles += Vector3.up * dir.x * (-1);
        //     LossCubesParent();
        //     SetParentRotationZeroAndSetPosition();
        //     SetCubeParent();
        //     rotatePoint.eulerAngles += Vector3.right * dir.y;
        //     LossCubesParent();
        // }

        // if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        // {
        //     Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        //     print(dir.x + "//" + dir.y);
        //     SetParentRotationZeroAndSetPosition();
        //     SetCubeParent();
        //     rotatePoint.eulerAngles += Vector3.up * dir.x * (-1);
        //     LossCubesParent();
        //     SetParentRotationZeroAndSetPosition();
        //     SetCubeParent();
        //     rotatePoint.eulerAngles += Vector3.right * dir.y;
        //     LossCubesParent();
        // }
    }


    void SetParentRotationZeroAndSetPosition()
    {
        rotatePoint.transform.position = rotateCube.transform.position;
        //rotatePoint.transform.eulerAngles = Vector3.zero;
        rotatePoint.transform.forward = Camera.main.transform.forward;

    }

    void SetCubeParent()
    {
        rotateCube.transform.parent = rotatePoint.transform;
    }

    void LossCubesParent()
    {
        rotateCube.transform.parent = null;
    }
}


