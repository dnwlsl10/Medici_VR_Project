using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    CharacterController cc;
    float rx;
    float ry;
    public float rotSpeed = 20;
    public float speed = 5;
    float gravity = -9.81f;
    float yVelocity;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
#if UNITY_EDITOR
        if (!cc.isGrounded)
        {
            yVelocity += gravity * Time.deltaTime;
        }



        if (OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            Vector2 dirStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
            Vector3 dirMove = new Vector3(dirStick.x, 0, dirStick.y);
            dirMove.Normalize();
            dirMove = Camera.main.transform.TransformDirection(dirMove);
            Vector3 velocity = dirMove * speed;
            velocity.y = yVelocity;
            cc.Move(velocity * Time.deltaTime);
        }
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        Vector3 dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir);
        dir.y = 0;
        dir.Normalize();
        rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        rx = Mathf.Clamp(rx, -80, 80);

        Vector3 velocity2 = dir * speed;
        velocity2.y = yVelocity;
        transform.eulerAngles = new Vector3(-rx, -90 + ry, 0);
        cc.Move(velocity2 * Time.deltaTime);
#else
        
#endif

    }
}
