using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fan : MonoBehaviour
{
    public bool reverseDirection = false;
    public int speed = 50;

    // Update is called once per frame
    void Update()
    {
        int mult = speed;
        if (reverseDirection) { mult *= -1; }
        transform.Rotate(new Vector3(transform.forward.x, transform.forward.y, transform.forward.z-75) * mult * Time.deltaTime, Space.Self);
    }
}
