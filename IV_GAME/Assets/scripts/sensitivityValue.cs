using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sensitivityValue : MonoBehaviour
{
    static float currentSensitivity;

    
    Text textComponent;
    void Start()
    {
        if (currentSensitivity == 100)
        {
            textComponent.text = "100";
        }
        textComponent = GetComponent<Text>();
        textComponent.text = currentSensitivity.ToString();
    }

    // Update is called once per frame
    public void SetSliderValue(float sliderValue)
    {
        textComponent.text = sliderValue.ToString();
        currentSensitivity = sliderValue;
    }
}
