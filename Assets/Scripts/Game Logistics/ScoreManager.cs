using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScoreManager : MonoBehaviour
{
    public NetworkManager networkManager;
    public List<TextMeshProUGUI> parScores = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> playerScores = new List<TextMeshProUGUI>();

    private List<string> playerIDs = new List<string>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
