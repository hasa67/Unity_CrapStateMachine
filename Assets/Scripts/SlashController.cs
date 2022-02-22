using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour {

	public int damage;

	void Start () {
		StartCoroutine (SelfDestroy ());
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			other.GetComponentInParent<HealthController> ().TakeDamage (damage);
			Destroy (this.gameObject);
		}
	}

	IEnumerator SelfDestroy(){
		yield return new WaitForSeconds (1f);
		Destroy (this.gameObject);
	}
}
