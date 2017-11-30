using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
public class PlayerController : PunBehaviour {

    int speed = 20;
	void Update () {

        //if (photonView.isMine) {

            float velocity = speed * Time.deltaTime;
            Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal") * velocity, 0, Input.GetAxisRaw("Vertical") * velocity);

            transform.position += move;
        //}
	}
}
