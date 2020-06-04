using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hookvisuals : MonoBehaviour
{
    public GameObject rope;
    public PlayerMovement2 grappling;
    private float dist;
    public Transform origin;
    public PlayerMovement2 destination;
    public Transform forward;
    public Material ropemat;
    private GameObject clone;
    Vector3 ropeRotation;
    float halfDistance;

    public float lineDrawSpeed = 6f;


    void LateUpdate()
    {
        if (grappling.isGrappling)
        {
            halfDistance = Vector3.Distance(destination.grapplePoint.point, transform.position)/2;
            ropeRotation = (destination.grapplePoint.point - transform.position).normalized;
            Destroy(clone);
            
            clone = (GameObject)Instantiate(rope, transform.position + halfDistance * ropeRotation, Quaternion.FromToRotation(Vector3.up, ropeRotation));
            clone.transform.localScale = new Vector3(0.05f, halfDistance, 0.05f);
        }

        if (!grappling.isGrappling)
        {
            Destroy(clone);
        }
    }
}
