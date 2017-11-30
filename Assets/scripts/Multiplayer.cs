using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
namespace graystork.tst {
    public class Multiplayer : PunBehaviour {

        // Use this for initialization
        void Start() {
            PhotonNetwork.ConnectUsingSettings("0.1");
            PhotonNetwork.logLevel = PhotonLogLevel.Full;
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
                        
                PhotonNetwork.Instantiate("cube", new Vector3(0, 0.5f, 0), Quaternion.identity, 0);
                     
        }

    }
}
