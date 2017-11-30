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
        if (multiplayer.turn == Multiplayer.Turn.local && !PhotonNetwork.isMasterClient ) {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        if (stream.isWriting) {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else {
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
