using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Multiplayer : PunBehaviour {

    public GameObject prefab;
    public enum Turn {
        local, remote
    }

    public Turn turn = Turn.local;
            
    // Use this for initialization
    void Start() {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.ConnectUsingSettings("0.1");
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
    }
    void Update() {
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
        if(PhotonNetwork.isMasterClient) {
            turn = Turn.local;
            StartTurn();
        }                            
    }      

    void StartTurn() {
        if(turn == Turn.local) {            
            this.photonView.RPC("NetworkSpawn", PhotonTargets.All, null);
        }
    }

    [PunRPC]
    void NetworkSpawn() {

        Transform localPlayer = Instantiate(prefab, new Vector3(0, 0.5f, 0), Quaternion.identity).transform;

        if (turn == Turn.local && PhotonNetwork.isMasterClient) {
           localPlayer.GetComponent<PlayerController>().enabled = true;
        }
        if (turn == Turn.remote && !PhotonNetwork.isMasterClient) {
           localPlayer.GetComponent<PlayerController>().enabled = true;
        }

    }


}

