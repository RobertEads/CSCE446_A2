using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabInteractionManagement : MonoBehaviour
{
    private LogisticsManagementScript myLogisticsManager;
    [SerializeField] private hand whichHandAmI;

    // Start is called before the first frame update
    void Start()
    {
        GameObject logistics = GameObject.Find("LogisticManager");
        myLogisticsManager = logistics.GetComponent<LogisticsManagementScript>();
        if (whichHandAmI != myLogisticsManager.get_userDominantHand()) { gameObject.SetActive(false); }
    }
}
