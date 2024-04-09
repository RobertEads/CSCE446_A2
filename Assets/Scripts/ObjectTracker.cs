using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTracker : MonoBehaviour
{
    public GameObject targetObject;

    public string targetObjectName;
    public bool trackPosition;
    public bool trackRotation;

    // Start is called before the first frame update
    void Start()
    {
        //targetObject = GameObject.Find(targetObjectName);
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            if (trackPosition)
                transform.position = targetObject.transform.position;
            if (trackRotation)
                transform.rotation = targetObject.transform.rotation;
        }
    }
}
