using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Multiplayer : PunBehaviour {

    public GameObject prefab;
    public enum Turn {
        local, remote
    }

    public Turn turn;
            
    // Use this for initialization
    void Start() {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }
    void Update() {
        if (!PhotonNetwork.isMasterClient)
            return;

        if (PhotonNetwork.room.PlayerCount == 2) {
            turn = Turn.local;
            StartTurn();

        }
    }

    void OnGUI() {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinRandomRoom();            
    }
    void OnPhotonRandomJoinFailed() {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedLobby() {                
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnJoinedRoom() {                   
    }      

    void StartTurn() {
        if(turn == Turn.local) {
            int id1 = PhotonNetwork.AllocateViewID();  
            photonView.RPC("NetworkSpawn", PhotonTargets.All, id1);
        }
    }

    [PunRPC]
    public void SwitchTurn() {
        turn = (turn == Turn.local) ? turn = Turn.remote : turn = Turn.local;       
    }

    [PunRPC]
    void NetworkSpawn(int id1) {

        Transform localPlayer = Instantiate(prefab, new Vector3(0, 0.5f, 0), Quaternion.identity).transform;
        PhotonView[] nViews = localPlayer.GetComponentsInChildren<PhotonView>();
        nViews[0].viewID = id1;

        if (turn == Turn.local && PhotonNetwork.isMasterClient) {
           localPlayer.GetComponent<PlayerController>().enabled = true;
        }
        if (turn == Turn.remote && !PhotonNetwork.isMasterClient) {
           localPlayer.GetComponent<PlayerController>().enabled = true;
        }

    }


}

