using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public int health;
	public bool vulnerable = true;
	public Slider healthSlider;

	private Animator animator;

	void Start(){
		animator = GetComponent<Animator> ();
		healthSlider.maxValue = health;
		healthSlider.value = health;
	}

	public void TakeDamage(int damage){
		if (vulnerable) {
			health -= damage;
			Enrage ();
			healthSlider.value = health;
			if (health <= 0) {
				print (this.name +" Died");
			}
		}
	}

	public void Enrage(){
		if (gameObject.tag != "Player") {
			if (health < 90) {
				print ("Boss Enraged");
				animator.SetBool ("IsEnrage", true);
			}
		}
	}
}
