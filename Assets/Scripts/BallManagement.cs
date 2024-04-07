using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManagement : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude < 0.2) 
        {
            rb.velocity = Vector3.zero;
        }
    }
}
