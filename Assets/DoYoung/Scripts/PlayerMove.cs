using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    AudioSource walkSource;
    AudioSource breathSource;
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
        walkSource = GetComponent<AudioSource>();
        breathSource = GetComponent<AudioSource>();
    }


    void Update()
    {
#if UNITY_EDITOR
        if (!BombManager.instance.isBombState)
        {
            if (!BombManager.instance.isGameFail)
            {
                if (!cc.isGrounded)
                {
                    yVelocity += gravity * Time.deltaTime;
                }

                {
                    Vector2 dirStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
                    Vector3 dirMove = new Vector3(dirStick.x, 0, dirStick.y);
                    dirMove.Normalize();
                    dirMove = Camera.main.transform.TransformDirection(dirMove);
                    Vector3 velocity = dirMove * speed;
                    velocity.y = yVelocity;
                    cc.Move(velocity * Time.deltaTime);

                    if (dirMove != Vector3.zero)
                    {
                        if (!walkSource.isPlaying)
                        {
                            walkSource.Play();
                        }
                    }
                    else
                    {
                        walkSource.Stop();
                    }
                }
            }


            {
                Vector2 dirStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
                dirStick.y = 0;
                Vector3 camRotateDir = new Vector3(0, dirStick.x, 0);
                transform.Rotate(camRotateDir);


            }
        }

#else
        
#endif

    }
}
