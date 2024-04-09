using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum hand { LEFT, RIGHT }
public enum whichHole { SPAWN, HOLE_1, HOLE_2, HOLE_3 }

public class LogisticsManagementScript : MonoBehaviour
{
    private int myTotalScore = 0;
    private bool ballCurrentlySpawned = false;
    private bool finishedWithGame = false;
    private bool userLookingToTeleport = false;
    private bool makeGameFinishedNetworkCall = false;
    private bool inHalfTime = false;
    private bool ballHitForReset = false;

    private Vector3 teleportTargetLocation;
    private whichHole currentHole;
    private whichHole targetHole;
    private Dictionary<whichHole, int> myScores;
    private Dictionary<whichHole, bool> holeCompleted;
    private GameObject currentBallReference;

    [SerializeField] private hand userDominantHand;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        currentHole = whichHole.SPAWN;
        myScores = new Dictionary<whichHole, int>() { { whichHole.HOLE_1, 0 }, { whichHole.HOLE_2, 0 }, { whichHole.HOLE_3, 0 } };
        holeCompleted = new Dictionary<whichHole, bool>() { { whichHole.HOLE_1, false }, { whichHole.HOLE_2, false }, { whichHole.HOLE_3, false } };
    }




    public void reset_score_on_leave()
    {
        if (currentHole != whichHole.SPAWN && !holeCompleted[currentHole]) { myScores[currentHole] = 0; }
        if (currentBallReference) { Destroy(currentBallReference); ballCurrentlySpawned = false; }
        currentHole = targetHole;
    }

    public void player_finished_hole(whichHole completedHole)
    {
        ballCurrentlySpawned = false;
        holeCompleted[completedHole] = true;
        myTotalScore += myScores[completedHole];
        if (holeCompleted[whichHole.HOLE_1] && holeCompleted[whichHole.HOLE_2] && holeCompleted[whichHole.HOLE_3]) { finishedWithGame = true; }
        if (finishedWithGame) { makeGameFinishedNetworkCall = true; }
    }

    public void increase_score(whichHole selectedHole) { myScores[selectedHole]++; }

    public bool finished_with_hole(whichHole selectedHole) { return holeCompleted[selectedHole]; }

    public void i_won() {/* UI Stuff Here */ }

    public void someone_else_won() {/* UI Stuff Here */ }

    public void restart_ball_position() 
    {
        if (currentHole == whichHole.HOLE_1) { currentBallReference.transform.position = GameObject.Find("ballSpawn_holeOne").transform.position; }
        if (currentHole == whichHole.HOLE_2) { currentBallReference.transform.position = GameObject.Find("ballSpawn_holeTwo").transform.position; }
        if (currentHole == whichHole.HOLE_3) { currentBallReference.transform.position = GameObject.Find("ballSpawn_holeThree").transform.position; }
    }

    private void reset_hands()
    {
        leftHand.SetActive(true); 
        rightHand.SetActive(true);
    }




    //Setters
    public void set_currentHole(whichHole newValue) { currentHole = newValue; }
    public void set_ballCurrentlySpawned(bool newValue) { ballCurrentlySpawned = newValue; }
    public void set_finishedWithGame(bool newValue) { finishedWithGame = newValue; }
    public void set_makeGameFinishedNetworkCall(bool newValue) { makeGameFinishedNetworkCall = newValue; }
    public void set_userLookingToTeleport(bool newValue) { userLookingToTeleport = newValue; }
    public void set_userDominantHand(hand newValue) { userDominantHand = newValue;  reset_hands(); }
    public void set_teleportTargetLocation(Vector3 newValue) { teleportTargetLocation = newValue; }
    public void set_targetHole(whichHole newValue) { targetHole = newValue; }
    public void set_currentBallReference(GameObject newValue) { currentBallReference = newValue; }
    public void set_inHalfTime(bool newValue) { inHalfTime = newValue; }

    public void set_ballHitForReset(bool newValue) { ballHitForReset = newValue; }


    //Getters
    public whichHole get_currentHole() { return currentHole; }
    public bool get_ballCurrentlySpawned() { return ballCurrentlySpawned; }
    public bool get_finishedWithGame() { return finishedWithGame; }
    public bool get_makeGameFinishedNetworkCall() { return makeGameFinishedNetworkCall; }
    public bool get_userLookingToTeleport() { return userLookingToTeleport; }
    public hand get_userDominantHand() { return userDominantHand; }
    public Vector3 get_teleportTargetLocation() { return teleportTargetLocation; }
    public whichHole get_targetHole() { return targetHole; }
    public GameObject get_currentBallReference() {  return currentBallReference; }
    public int get_myTotalScore() { return myTotalScore; }
    public int get_score_for_specific_hole(whichHole selectedHole) { return myScores[selectedHole]; }
    public bool get_inHalfTime() { return inHalfTime; }
    public bool get_ballHitForReset() { return ballHitForReset; }
}
