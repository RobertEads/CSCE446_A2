using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementManager : MonoBehaviour
{
    //private collisionDetection myColDec;
    //private logisticsManager myLogisticsManager;

    //Replication
    private PhotonView myView;

    //Movement
    private GameObject myBody;
    private Rigidbody myRB;
    private float xInput;
    private float zInput;
    private float movementSpeed = 10.0f;

    //Material
    private MeshRenderer myMesh;
    private int index = 0;
    [SerializeField] List<Material> myMaterials;

    // Start is called before the first frame update
    void Start()
    {
        myView = GetComponent<PhotonView>();
        myBody = transform.GetChild(0).gameObject;
        myRB = myBody.GetComponent<Rigidbody>();
        myMesh = myBody.GetComponent<MeshRenderer>();

        GameObject logistics = GameObject.Find("LogisticsManager");
        //myLogisticsManager = logistics.GetComponent<logisticsManager>();

        //myColDec = myBody.GetComponent<collisionDetection>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(myView.IsMine)
        {
            xInput = Input.GetAxis("Horizontal");
            zInput = Input.GetAxis("Vertical");

            /*if(myColDec.collided)
            {
                myLogisticsManager.collisionFound = true;
                myColDec.collided = false;
            }

            if(myLogisticsManager.iLost)
            {
                myView.RPC("communicateLoss", RpcTarget.Others, 6); //Loser initiates this call
                myLogisticsManager.resetGameplay();
                
            }*/
        }

        if(Input.GetKeyDown(KeyCode.Space)) { myView.RPC("changeMaterial", RpcTarget.Others); }
    }

    private void FixedUpdate()
    {
        myRB.AddForce(xInput*movementSpeed, 0, zInput*movementSpeed);
    }

    /*[PunRPC]
    void changeMaterial()
    {
        if(myView.IsMine)
        {
            myMesh.material = myMaterials[index];
            index++;
            if (index == myMaterials.Count) { index = 0; }
        }
    }

    [PunRPC]
    void communicateLoss(int randomNumber) //called by all but losing player
    {
        myLogisticsManager.otherLost();
    }*/
}
