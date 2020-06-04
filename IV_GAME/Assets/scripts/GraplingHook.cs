using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    float grapDistance;
    Vector3 grapDirection;

    
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
                Debug.DrawLine(ray.origin, hit.point);
                grapDistance = Vector3.Distance(ray.origin, transform.position); 
                grapDirection = ray.direction;

            

        }
    }
}
