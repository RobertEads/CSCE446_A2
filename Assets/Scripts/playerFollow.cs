using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFollow : MonoBehaviour
{
    private GameObject origin;
    // Start is called before the first frame update
    void Start()
    {
        origin = GameObject.Find("XR Origin");
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetPositionAndRotation(origin.transform.position, origin.transform.rotation);
    }
}
