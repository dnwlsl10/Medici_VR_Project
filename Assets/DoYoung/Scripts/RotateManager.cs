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

                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.up, -dir.x);
                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.right, dir.y);
            }

            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
            {
                Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch) * bombRoteAcel;

                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.up, -dir.x);
                rotateCube.transform.RotateAround(rotateCube.transform.position, Camera.main.transform.right, dir.y);
            }
        }
    }
}


