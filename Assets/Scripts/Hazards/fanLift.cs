using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanLift : MonoBehaviour
{
    public bool reverseDirection = false;
    public int pushStrength = 5;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("effectableObject"))
        {
            if (reverseDirection == false) { collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * pushStrength); }
            if (reverseDirection == true) { collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * -pushStrength); }

        }
    }
}
