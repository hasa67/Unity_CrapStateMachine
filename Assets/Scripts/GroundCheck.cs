using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	GameObject player;

	void Start () {
		player = GameObject.FindWithTag ("Player");
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Ground") {
			player.GetComponent<PlayerController> ().isGrounded = true;
			player.GetComponent<PlayerController> ().secondJump = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Ground") {
			player.GetComponent<PlayerController> ().isGrounded = false;
		}
	}
}
