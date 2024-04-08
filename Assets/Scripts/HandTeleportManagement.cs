using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;



public class HandTeleportManagement : MonoBehaviour
{
    private bool leftPrimaryUsed, rightPrimaryUsed, leftSecondaryUsed, rightSecondaryUsed;

    private InputData controllerInput;
    private LogisticsManagementScript myLogisticsManager;
    private GameObject myXrOrigin;

    [SerializeField] private hand whichHandAmI;
    [SerializeField] private LayerMask mask;


    void Start()
    {
        GameObject logistics = GameObject.Find("LogisticManager");
        myLogisticsManager = logistics.GetComponent<LogisticsManagementScript>();

        myXrOrigin = GameObject.Find("XR Origin");
        controllerInput = myXrOrigin.GetComponent<InputData>();

        leftPrimaryUsed = false;
        rightPrimaryUsed = false; 
        leftSecondaryUsed = false;
        rightSecondaryUsed = false;

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
                if (controllerInput.leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isLeftSecondary) && !leftSecondaryUsed) 
                { 
                    if(isLeftSecondary)
                    {
                        leftSecondaryUsed = true;
                        myLogisticsManager.set_inHalfTime(!myLogisticsManager.get_inHalfTime());
                        leftSecondaryUsed = false;
                    }
                }
                
                if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask)) { myLogisticsManager.set_ballHitForReset(true); }
                else { myLogisticsManager.set_ballHitForReset(false); }
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
                if (controllerInput.rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isLeftSecondary) && !rightSecondaryUsed)
                {
                    if (isLeftSecondary)
                    {
                        rightSecondaryUsed = true;
                        myLogisticsManager.set_inHalfTime(!myLogisticsManager.get_inHalfTime());
                        rightSecondaryUsed = false;
                    }
                }
                
                if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask)) { myLogisticsManager.set_ballHitForReset(true); }
                else { myLogisticsManager.set_ballHitForReset(false); }
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
