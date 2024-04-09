using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    public List<TextMeshProUGUI> parScores = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> playerScores = new List<TextMeshProUGUI>();

    public GameObject scorePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    
    private LogisticsManagementScript logisticsManagementScript;

    private int currentScore;
    private int lowestScore = int.MaxValue;

    private List<string> playerIDs = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject logistics = GameObject.Find("LogisticManager");
        logisticsManagementScript = logistics.GetComponent<LogisticsManagementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerScores[0].text = logisticsManagementScript.get_score_for_specific_hole(whichHole.HOLE_1).ToString();
        playerScores[1].text = logisticsManagementScript.get_score_for_specific_hole(whichHole.HOLE_2).ToString();
        playerScores[2].text = logisticsManagementScript.get_score_for_specific_hole(whichHole.HOLE_3).ToString();
        playerScores[3].text = logisticsManagementScript.get_myTotalScore().ToString();

        if(logisticsManagementScript.get_finishedWithGame() )
        {
            DisplayWinPanel();
        }
    }

    public void UpdateScore(int holeNumber, int scoreValue)
    {
        // Update correct element on the scoreboard
        TextMeshProUGUI scoreText = playerScores[holeNumber - 1];
        scoreText.text = scoreValue.ToString();

        CalculateFinalScore();
    }

    public void IncrementScore(int holeNumber)
    {
        // Update score on the correct hole
        TextMeshProUGUI scoreText = playerScores[holeNumber - 1];
        int oldScore = int.Parse(scoreText.text);
        scoreText.text = (oldScore + 1).ToString();

        CalculateFinalScore();
    }

    public void CalculateFinalScore()
    {
        int finalScore = 0;
        for (int i = 0; i < 3; i++)
        {
            finalScore += int.Parse(playerScores[i].text);
        }

        playerScores[3].text = finalScore.ToString();

        currentScore = finalScore;
    }

    public void ChangeDominantToLeft()
    {
        logisticsManagementScript.set_userDominantHand(hand.LEFT);
    }

    public void ChangeDominantToRight()
    {
        logisticsManagementScript.set_userDominantHand(hand.RIGHT);
    }

    public void DisplayWinPanel()
    {
        winPanel.SetActive(true);
    }

    public void DisplayLossPanel()
    {
        losePanel.SetActive(true);
    }

    void CheckIfGameIsDone()
    {

    }

    [PunRPC]
    void CheckWhoWon()
    {

    }
}
