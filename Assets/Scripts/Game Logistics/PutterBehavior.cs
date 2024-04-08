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
    private LogisticsManagementScript logisticsManagementScript;
    private GameObject ballSpawnPoint;

    [SerializeField] private whichHole myHole;
    [SerializeField] private GameObject ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myStartingPosition = transform.position;
        myStartingRotation = transform.rotation;
        GameObject gameManager = GameObject.Find("LogisticManager");
        logisticsManagementScript = gameManager.GetComponent<LogisticsManagementScript>();
        ballSpawnPoint = GameObject.Find("ballSpawn_holeOne");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CancelInvoke("respawn_timer");
            if (!logisticsManagementScript.get_ballCurrentlySpawned() && !logisticsManagementScript.finished_with_hole(myHole))
            {
                logisticsManagementScript.set_ballCurrentlySpawned(true);
                logisticsManagementScript.set_currentBallReference(Instantiate(ballPrefab, ballSpawnPoint.transform.position, ballSpawnPoint.transform.rotation));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) { Invoke("respawn_timer", 2.5f); }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ball")) { logisticsManagementScript.increase_score(myHole); Debug.Log("Hit registered"); }
    }


    private void respawn_timer()
    {
        myRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        myRigidbody.velocity = Vector3.zero;
        transform.SetPositionAndRotation(myStartingPosition, myStartingRotation);
        myRigidbody.constraints = RigidbodyConstraints.None;
    }
   
}
