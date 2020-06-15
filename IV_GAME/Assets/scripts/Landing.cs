using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landing : MonoBehaviour
{

    public PlayerMovement2 landet;
    float landingTimer;
    bool landing;
    Vector3 oldPosition;
    
    void LateUpdate()
    {
        if (landet.hasLanded)
        {
            landingTimer = 0.4f;
            landing = true;
            oldPosition = transform.position;
        }

        if(landing)
        {
            landingTimer -= Time.deltaTime;
        }

        if(landingTimer > 0.2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }

        if(landingTimer >= 0.2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if(landingTimer <= 0 && landing)
        {
            transform.position = oldPosition;
            landing = false;
        }

    }
}
