using UnityEngine;
using System.Collections;

public class Object_Brush : MonoBehaviour {
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Cleanable") {

			Destroy(col.gameObject);
			if (!this.GetComponent<AudioSource>().isPlaying) {
				this.GetComponent<AudioSource>().Play ();
			}
		}


	}



}
