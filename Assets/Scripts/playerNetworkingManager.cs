using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class playerNetworkingManager : MonoBehaviour
{
    //Replication
    private PhotonView myView;

    private Dictionary<PhotonView, int> allTotalScores;
    private LogisticsManagementScript logisticsManagementScript;

    void Start()
    {
        myView = GetComponent<PhotonView>();
        GameObject gameManager = GameObject.Find("LogisticManager");
        logisticsManagementScript = gameManager.GetComponent<LogisticsManagementScript>();
    }

    void Update()
    {
        if (myView.IsMine)
        {
            if (logisticsManagementScript.get_makeGameFinishedNetworkCall())
            {
                logisticsManagementScript.set_makeGameFinishedNetworkCall(false);
                myView.RPC("collect_finished_states", RpcTarget.Others);
                Invoke("register_gameover", 5f);
            }
        }
    }

    [PunRPC]
    public void collect_finished_states(PhotonMessageInfo info) { if (!logisticsManagementScript.get_finishedWithGame()) { myView.RPC("cancel_gameover", info.Sender); } }

    [PunRPC]
    public void cancel_gameover() { CancelInvoke("register_gameover"); }

    [PunRPC]
    public void score_request(PhotonMessageInfo info) { myView.RPC("accept_scores", info.Sender, logisticsManagementScript.get_myTotalScore()); }

    [PunRPC]
    public void accept_scores(PhotonMessageInfo info, int score) { allTotalScores[info.photonView] = score; }

    [PunRPC]
    public void designate_winner(PhotonView winner) 
    { 
        if (winner == myView) { logisticsManagementScript.i_won(); }
        else { logisticsManagementScript.someone_else_won();  }
    }

    private void register_gameover()
    {
        allTotalScores[myView] = logisticsManagementScript.get_myTotalScore();
        myView.RPC("score_request", RpcTarget.Others);

        int maxScore = allTotalScores.Values.ToList().Min();
        int index = allTotalScores.Values.ToList().IndexOf(maxScore);
        PhotonView winner = allTotalScores.Keys.ToList().ElementAt(index);

        myView.RPC("designate_winner", RpcTarget.All, winner);        
    }

}
