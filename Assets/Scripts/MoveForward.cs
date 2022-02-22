using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour {

	private float moveSpeed = 10f;
	private Vector3 newPosition;
	private bool isFacingRight;

	void Start () {
		isFacingRight = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().isFacingRight;
	}

	void Update () {
		newPosition = transform.position;
		if (isFacingRight) {
			newPosition.x += moveSpeed * Time.deltaTime;
		} else {
			newPosition.x -= moveSpeed * Time.deltaTime;
		}

		transform.position = newPosition;
	}
}
