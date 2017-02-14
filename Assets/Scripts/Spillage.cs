using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spillage : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Cleanable" && col.gameObject.transform.localScale.x > this.gameObject.transform.localScale.x) {

			Destroy(this.gameObject);

		}
	}
}
