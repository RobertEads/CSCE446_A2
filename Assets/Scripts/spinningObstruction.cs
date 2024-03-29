using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningObstruction : MonoBehaviour
{
    public bool reverseDirection = false;
    public int speed = 50;

    // Update is called once per frame
    void Update()
    {
        int mult = speed;
        if(reverseDirection) { mult *= -1; }
        transform.Rotate(Vector3.up * mult * Time.deltaTime, Space.Self);
    }
}
