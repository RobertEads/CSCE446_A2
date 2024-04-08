using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManagement : MonoBehaviour
{
    private Vector3 lastStablePosition;
    private Rigidbody rb;
   
    void Start()
    {
        lastStablePosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.magnitude < 0.2) { rb.velocity = Vector3.zero; }
        if (rb.velocity.magnitude == 0) { lastStablePosition = transform.position; }
        if (transform.position.y < -10) { transform.position = lastStablePosition; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Water")) { transform.position = lastStablePosition; }
    }
}
