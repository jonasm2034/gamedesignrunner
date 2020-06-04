using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hitMarker : MonoBehaviour
{
    public PlayerMovement2 hitMarkerTrigger, inRange;
    Image myImageComponent;
    public Sprite normal; 
    public Sprite grappling;
    public Sprite rangenormal;
    public Sprite grapplingrange;

    void Start() //Lets start by getting a reference to our image component.
    {
        myImageComponent = GetComponent<Image>(); //Our image component is the one attached to this gameObject.
    }


    private void Update()
    {
        if (hitMarkerTrigger.isGrappling && hitMarkerTrigger.inGrapplingRange) 
        {
            myImageComponent.sprite = grapplingrange;
        }

        if (!hitMarkerTrigger.isGrappling && !hitMarkerTrigger.inGrapplingRange)
        {
            myImageComponent.sprite = normal;
        }

        if (hitMarkerTrigger.isGrappling && !hitMarkerTrigger.inGrapplingRange)
        {
            myImageComponent.sprite = grappling;
        }

        if (!hitMarkerTrigger.isGrappling && hitMarkerTrigger.inGrapplingRange)
        {
            myImageComponent.sprite = rangenormal;
        }
    }
  

}
