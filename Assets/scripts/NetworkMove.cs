using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class NetworkMove : PunBehaviour{
    
    Multiplayer multiplayer;
    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

    // Update is called once per frame

   
    void Start() {
        multiplayer = FindObjectOfType<Multiplayer>();
    }
    void Update() {
        if (multiplayer.turn == Multiplayer.Turn.local && !PhotonNetwork.isMasterClient) {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        Debug.Log("FIRED");
        if (stream.isWriting) {

            Debug.Log("is Writing the data");
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else {

            Debug.Log("is Reading the data");
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
