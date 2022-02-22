using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
	public bool isFacingRight = true;
	public int attackDamage;
	public float swordLength;
	public int enrageAttackDamage;
	public float enrageSwordLength;
	public LayerMask attackMask;

	private Transform player;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	public void LookAtPlayer(){
		Vector3 localScale = transform.localScale;
		if (transform.position.x > player.position.x && isFacingRight == true) {
			localScale.x *= -1f;
			isFacingRight = false;
		} else if (transform.position.x < player.position.x && isFacingRight == false) {
			localScale.x *= -1f;
			isFacingRight = true;
		}
		transform.localScale = localScale;
	}

	public void Attack(){
		Vector3 pos = transform.position;

		Collider2D colInfo = Physics2D.OverlapCircle (pos, 1f, attackMask);
		if (colInfo != null) {
			colInfo.GetComponent<HealthController> ().TakeDamage (attackDamage);
		}
	}

	public void EnrageAttack(){
		Vector3 pos = transform.position;

		Collider2D colInfo = Physics2D.OverlapCircle (pos, 2f, attackMask);
		if (colInfo != null) {
			colInfo.GetComponent<HealthController> ().TakeDamage (enrageAttackDamage);
		}
	}
}
