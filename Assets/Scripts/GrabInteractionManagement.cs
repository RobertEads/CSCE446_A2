using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteractionManagement : MonoBehaviour
{
    private LogisticsManagementScript myLogisticsManager;

    [SerializeField] private hand whichHandAmI;

    void Start()
    {
        GameObject logistics = GameObject.Find("LogisticManager");
        myLogisticsManager = logistics.GetComponent<LogisticsManagementScript>();
        run_hand_setup();
    }

    public void run_hand_setup()
    {
        if (whichHandAmI != myLogisticsManager.get_userDominantHand()) { gameObject.SetActive(false); }
    }
}
