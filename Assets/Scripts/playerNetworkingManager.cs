using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerNetworkingManager : MonoBehaviour
{
    //Replication
    private PhotonView myView;


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


    private void register_gameover()
    {
        //do stuff here - probs talk to gameManager & UI
        Debug.Log("Game over");
        // Send message to figure out who has the highest score
        // send message to logistics managmer to tell the ui to display who ever had the highest score
    }

}
