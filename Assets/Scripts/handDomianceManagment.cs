using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;



public class handDomianceManagment : MonoBehaviour
{
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
    }

    void Update()
    {
        if (whichHandAmI != myLogisticsManager.get_userDominantHand())
        {
            if(whichHandAmI == hand.LEFT)
            {
                if (controllerInput.leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isLeftPrimary))
                {
                    Debug.Log("left button pressed");
                    handle_teleport_request(isLeftPrimary);
                }
            }
            else
            {
                if (controllerInput.rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool isRightPrimary))
                {
                    Debug.Log("right button pressed");
                    handle_teleport_request(isRightPrimary);
                }
            }
        }
    }

   private void handle_teleport_request(bool isPrimary)
    {
        if (isPrimary && myLogisticsManager.get_userLookingToTeleport())
        {
            Debug.Log("Booking to TP");
            myLogisticsManager.set_userLookingToTeleport(false);
            myXrOrigin.transform.position = myLogisticsManager.get_teleportTargetLocation();

        }
    }
}
