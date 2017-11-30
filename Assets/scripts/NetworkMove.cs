using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class NetworkMove : PunBehaviour{

    private Vector3 correctPlayerPos;
    private Quaternion correctPlayerRot;

    // Update is called once per frame
    void Update() {
        if (!photonView.isMine) {
            transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

        Debug.Log("Entered in Serialize view");
        if (stream.isWriting) {
            Debug.Log("Writing Data");
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else {

            Debug.Log("Reading Data");
            // Network player, receive data
            correctPlayerPos = (Vector3)stream.ReceiveNext();
            correctPlayerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
