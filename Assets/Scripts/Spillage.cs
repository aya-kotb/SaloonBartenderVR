using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spillage : MonoBehaviour {
// Used to increase the size of spillage when hit by another 'piece' of liquid
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Cleanable" && col.gameObject.transform.localScale.x > this.gameObject.transform.localScale.x) {
			Destroy(this.gameObject);
		}
	}
}
