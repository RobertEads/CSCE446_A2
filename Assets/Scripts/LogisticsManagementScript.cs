using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum hand { LEFT, RIGHT }

public class LogisticsManagementScript : MonoBehaviour
{
    private bool userLookingToTeleport;
    private Vector3 teleportTargetLocation;

    [SerializeField] private hand userDominantHand;



    public void set_userLookingToTeleport(bool newValue) { userLookingToTeleport = newValue; }
    public void set_userDominantHand(hand newValue) { userDominantHand = newValue; }
    public void set_teleportTargetLocation(Vector3 newValue) { teleportTargetLocation = newValue; }


    public bool get_userLookingToTeleport() { return userLookingToTeleport; }
    public hand get_userDominantHand() { return userDominantHand; }
    public Vector3 get_teleportTargetLocation() { return teleportTargetLocation; }
}
