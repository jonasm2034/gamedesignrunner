using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadTransform : MonoBehaviour
{
    private Vector3 MyGameObjectPosition;

    void Start()
    {
        Vector3 pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //MyGameObjectPosition = GameObject.Find("EventTransform").transform.position;
        //Transform childTransform = transform.Find("ChildTransform");
        if (transform.position.x > 1)
        {
            ScoreManager.instance.AddScore();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScoreManager.instance.AddScore();
    }
}
