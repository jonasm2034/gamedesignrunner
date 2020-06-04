using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public GameObject controller;

    public PlayerMovement2 grappling;
    public PlayerMovement2 wallRunning;
    public PlayerMovement2 movspeed;
    public PlayerMovement2 direc;
    public PlayerMovement2 velocity;

    
    void Start()
    {
        transform.position = controller.transform.position;
    }

    
    void LateUpdate()
    {

        if (wallRunning.wallRunning && controller.transform.rotation.y > -90 && controller.transform.rotation.y < 90)
        {
            Quaternion.FromToRotation(Vector3.forward, direc.finalHit.normal);
            transform.Translate(-direc.runDirection.normalized * Time.deltaTime * movspeed.speed);
            transform.Translate(transform.up * velocity.currentVelocity * Time.deltaTime);
        }

        if (wallRunning.wallRunning && controller.transform.rotation.y < -90 && controller.transform.rotation.y > 90)
        {
            Quaternion.FromToRotation(Vector3.forward, direc.finalHit.normal);
            transform.Translate(direc.runDirection.normalized * Time.deltaTime * movspeed.speed);
            transform.Translate(transform.up * velocity.currentVelocity * Time.deltaTime);
        }


        if (!grappling.isGrappling && !wallRunning.wallRunning)
        {
            transform.position = controller.transform.position;
            transform.rotation = Quaternion.identity;
        }

        if (grappling.isGrappling && !wallRunning.wallRunning)
        {
            transform.RotateAround(grappling.grapplePoint.point, grappling.ortho, grappling.swingSize* -1 * Time.deltaTime);
        }

    

    }


}
