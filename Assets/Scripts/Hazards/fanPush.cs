using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanPush : MonoBehaviour
{
    public bool reverseDirection = false;
    public int pushStrength = 5;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("effectableObject"))
        {
            if (reverseDirection == false) { other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * pushStrength); }
            if (reverseDirection == true) { other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * -pushStrength); }

        }
    }
}
