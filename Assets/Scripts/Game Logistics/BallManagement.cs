using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManagement : MonoBehaviour
{
    private bool localTimeState = false;
    private bool needTimeStateUpdate = false;

    private Vector3 lastStablePosition;
    private Rigidbody rb;
    private LogisticsManagementScript myLogisticsManager;
    

    void Start()
    {
        lastStablePosition = transform.position;
        rb = GetComponent<Rigidbody>();
        GameObject logistics = GameObject.Find("LogisticManager");
        myLogisticsManager = logistics.GetComponent<LogisticsManagementScript>();
    }

    void Update()
    {
        if (rb.velocity.magnitude < 0.2) { rb.velocity = Vector3.zero; }
        if (rb.velocity.magnitude == 0) { lastStablePosition = transform.position; }
        if (transform.position.y < -10) { reset_ball(); }

        if(myLogisticsManager.get_inHalfTime() != localTimeState) 
        {
            localTimeState = !localTimeState;
            needTimeStateUpdate = true; 
        }
        if(needTimeStateUpdate) 
        {
            needTimeStateUpdate = false;
            if(localTimeState) { cut_ball_time(); }
            else { resume_ball_time(); }
        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Water")) { reset_ball(); }
    }

    private void reset_ball()
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        transform.position = lastStablePosition;
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.None;
    }

    public void cut_ball_time()
    {
        rb.velocity = rb.velocity * 0.5f;
        rb.drag = rb.drag * 0.5f;
    }

    public void resume_ball_time() 
    {
        rb.velocity = rb.velocity * 2f;
        rb.drag = rb.drag * 2f;
    }
}
