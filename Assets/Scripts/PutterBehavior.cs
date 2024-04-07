using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class PutterBehavior : MonoBehaviour
{
    private Vector3 myStartingPosition;
    private Quaternion myStartingRotation;
    private Rigidbody myRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myStartingPosition = transform.position;
        myStartingRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CancelInvoke("respawn_timer");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Invoke("respawn_timer", 2.5f);

        }
    }

    private void respawn_timer()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        myRigidbody.velocity = Vector3.zero;
        transform.SetPositionAndRotation(myStartingPosition, myStartingRotation);
        myRigidbody.constraints = RigidbodyConstraints.None;
    }
   
}
