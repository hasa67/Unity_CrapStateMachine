using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject slashPrefab;
	public bool isGrounded = false;
	public bool secondJump = false;
	public bool isFacingRight = true;
	public bool disableControlls = false;

	private bool isCooldown = false;
	private float cooldownTime = 0.3f;
	private Rigidbody2D rb;
	private Animator animator;
	private Transform shootTransform;
	private float moveSpeed = 5f;
	private float jumpSpeed = 10f;
	private Vector2 movement;


	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		shootTransform = transform.Find ("SlashLocation").GetComponent<Transform> ();
	}

	void Update () {
		if (!disableControlls) {
			GetInput ();
			FaceDirection ();
			Move ();
			Jump ();
			Attack ();
		}
		ClampPosition ();
		SetAnimator ();
	}

	private void GetInput(){
		movement.x = Input.GetAxisRaw ("Horizontal");
		movement.y = Input.GetAxisRaw ("Horizontal");
	}

	private void FaceDirection(){
		if (movement.x > 0) {
			isFacingRight = true;
		}
		if (movement.x < 0) {
			isFacingRight = false;
		}

		Vector3 localScale = transform.localScale;
		if (isFacingRight) {
			localScale.x = 1f;
		} else {
			localScale.x = -1f;
		}
		transform.localScale = localScale;
	}

	private void Move(){
		Vector2 newVelocity = new Vector2 (movement.x * moveSpeed, rb.velocity.y);
		rb.velocity = newVelocity;
	}

	private void Jump(){
		if (Input.GetKeyDown (KeyCode.UpArrow) && isGrounded) {
			Vector2 newVelocity = new Vector2 (rb.velocity.x, jumpSpeed);
			rb.velocity = newVelocity;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow) && !isGrounded && secondJump) {
			secondJump = false;
			Vector2 newVelocity = new Vector2 (rb.velocity.x, jumpSpeed);
			rb.velocity = newVelocity;
		}
	}

	private void Attack(){
		if (Input.GetButtonDown ("Jump") && !isCooldown) {
			animator.SetTrigger ("Attack");
			isCooldown = true;
			StartCoroutine (Cooldown ());
		}
	}

	IEnumerator Cooldown(){
		yield return new WaitForSeconds (cooldownTime);
		isCooldown = false;
	}

	private void InstantiateSlash(){
		GameObject slash = Instantiate (slashPrefab, shootTransform.position, Quaternion.identity);
		Vector3 localScale = slash.transform.localScale;
		if (!isFacingRight) {
			localScale.x *= -1;
		}
		slash.transform.localScale = localScale;
	}

	private void ClampPosition(){
		Vector3 clampedPosition = transform.position;
		clampedPosition.x = Mathf.Clamp(transform.position.x, -7.5f, 7.5f);
		transform.position = clampedPosition;
	}

	private void SetAnimator(){
		animator.SetFloat ("Speed", Mathf.Abs (rb.velocity.x));
		animator.SetBool ("isGround", isGrounded);
		animator.SetBool ("isStunt", disableControlls);
	}

	public void PlayerStunt(){
		StartCoroutine (PlayerStunt (2f));
	}

	IEnumerator PlayerStunt(float stuntTime){
		disableControlls = true;
		yield return new WaitForSeconds (stuntTime);
		disableControlls = false;
	}
}
