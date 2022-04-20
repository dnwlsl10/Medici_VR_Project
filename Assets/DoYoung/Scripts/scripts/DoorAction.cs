using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAction : MonoBehaviour
{


    void Update()
    {
#if UNITY_EDITOR
        if (0 != OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            RaycastHit hit;

            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit);


            if (hit.transform.tag == "door")
            {

                hit.transform.gameObject.GetComponent<Door>().ActionDoor();


            }
        }
#else
        
#endif






    }
}
