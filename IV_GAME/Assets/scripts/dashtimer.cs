using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashtimer : MonoBehaviour
{
    public PlayerMovement2 dashTimer;
    public Text changingText;
    void Start()
    {
        
    }

    void Update()
    {
        changingText.text = dashTimer.dashTimer.ToString("#0.0");
    }
}
