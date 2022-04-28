using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    private LineRenderer lr;
    public int count;
    void Start()
    {
        lr = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        Ray ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hitInfo;
        lr.SetPosition(0, ray.origin);

        if(Physics.Raycast(ray, out hitInfo))
        {
           Rigidbody rb =  hitInfo.collider.gameObject.GetComponent<Rigidbody>();
            lr.SetPosition(1, hitInfo.point);
            if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                count++;
                rb.isKinematic = false;
                rb.AddTorque(transform.up * 100f);
                rb.AddForce(-Vector3.back  * 10f , ForceMode.Impulse);
            }
        }
        else
        {
            lr.SetPosition(1, ray.origin + ray.direction * 100f);
        }
    }
}
