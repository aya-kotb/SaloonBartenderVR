using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Object") {
			if (!this.GetComponent<AudioSource>().isPlaying) {
				this.GetComponent<AudioSource>().Play ();
			}
		}

	}
}
