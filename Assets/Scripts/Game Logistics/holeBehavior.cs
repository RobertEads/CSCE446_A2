using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holeBehavior : MonoBehaviour
{
    [SerializeField] private whichHole myHole;

    private LogisticsManagementScript logisticsManagementScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameManager = GameObject.Find("LogisticManager");
        logisticsManagementScript = gameManager.GetComponent<LogisticsManagementScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "effectableObject") 
        {
            Destroy(other.gameObject);
            logisticsManagementScript.player_finished_hole(myHole); 
        }
    }
}
