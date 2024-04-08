using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



public class HandTeleportManagement : MonoBehaviour
{
    private bool leftPrimaryUsed;
    private bool rightPrimaryUsed;

    private InputData controllerInput;
    private LogisticsManagementScript myLogisticsManager;
    private GameObject myXrOrigin;

    [SerializeField] private hand whichHandAmI;


    void Start()
    {
        GameObject logistics = GameObject.Find("LogisticManager");
        myLogisticsManager = logistics.GetComponent<LogisticsManagementScript>();

        myXrOrigin = GameObject.Find("XR Origin");
        controllerInput = myXrOrigin.GetComponent<InputData>();

        leftPrimaryUsed = false;
        rightPrimaryUsed = false;
    }

    void Update()
    {
        if (whichHandAmI != myLogisticsManager.get_userDominantHand())
        {
            if(whichHandAmI == hand.LEFT)
            {
                if (controllerInput.leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isLeftPrimary) && !leftPrimaryUsed)
                {
                    if (isLeftPrimary)
                    {
                        leftPrimaryUsed = true;
                        handle_teleport_request();
                        leftPrimaryUsed = false;
                    }
                }
            }
            else
            {
                if (controllerInput.rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isRightPrimary) && !rightPrimaryUsed)
                {
                    if(isRightPrimary)
                    {
                        rightPrimaryUsed = true;
                        handle_teleport_request();
                        rightPrimaryUsed = false;
                    }
                    
                }
            }
        }
    }

   private void handle_teleport_request()
    {
        if (myLogisticsManager.get_userLookingToTeleport())
        {
            myLogisticsManager.set_userLookingToTeleport(false);
            myLogisticsManager.reset_score_on_leave();
            myXrOrigin.transform.position = myLogisticsManager.get_teleportTargetLocation();
        }
    }
}
