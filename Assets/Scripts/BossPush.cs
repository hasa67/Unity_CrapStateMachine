using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPush : StateMachineBehaviour {

	public float pushRange;
	public float pushForce;
	public int pushDamage;

	private Transform player;
	private Rigidbody2D rb;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		rb = animator.GetComponent<Rigidbody2D> ();

//		pushRange = animator.GetComponent<BossController> ().pushRange;
//		pushForce = animator.GetComponent<BossController> ().pushForce;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (Vector2.Distance (rb.position, player.position) < pushRange) {
			animator.SetTrigger ("Push");
			player.GetComponent<PlayerController> ().PlayerStunt ();
			bool isFacingRight = animator.GetComponent<BossController> ().isFacingRight;
			Vector2 playerVelo = new Vector2 ();
			if (isFacingRight) {
				playerVelo = new Vector2 (pushForce, 0);
			} else {
				playerVelo = new Vector2 (-pushForce, 0);
			}
			player.GetComponent<Rigidbody2D> ().velocity = playerVelo;
			player.GetComponent<HealthController> ().TakeDamage (pushDamage);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.ResetTrigger ("Push");
	}
}
