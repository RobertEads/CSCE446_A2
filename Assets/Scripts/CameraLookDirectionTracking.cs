using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLookDirectionTracking : MonoBehaviour
{
    private LogisticsManagementScript myLogisticsManager;

    [SerializeField] private LayerMask mask;


    // Start is called before the first frame update
    void Start()
    {
        GameObject logistics = GameObject.Find("LogisticManager");
        myLogisticsManager = logistics.GetComponent<LogisticsManagementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask))
        {
            //var obj = hit.collider.gameObject;
            var tpPoint = hit.collider.gameObject.transform.parent.GetChild(2);
            myLogisticsManager.set_userLookingToTeleport(true);
            myLogisticsManager.set_teleportTargetLocation(tpPoint.transform.position);
        }
        else { myLogisticsManager.set_userLookingToTeleport(false); }
    }
}
