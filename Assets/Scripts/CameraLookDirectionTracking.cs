using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLookDirectionTracking : MonoBehaviour
{
    private LogisticsManagementScript myLogisticsManager;

    [SerializeField] private LayerMask mask;

    void Start()
    {
        GameObject logistics = GameObject.Find("LogisticManager");
        myLogisticsManager = logistics.GetComponent<LogisticsManagementScript>();
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask))
        {
            var tpPoint = hit.collider.gameObject.transform.parent.GetChild(2);
            myLogisticsManager.set_teleportTargetLocation(tpPoint.transform.position);
            myLogisticsManager.set_targetHole(tpPoint.GetComponent<telepointPointManager>().get_myHoleNumber());
            myLogisticsManager.set_userLookingToTeleport(true);
        }
        else { myLogisticsManager.set_userLookingToTeleport(false); }
    }
}
