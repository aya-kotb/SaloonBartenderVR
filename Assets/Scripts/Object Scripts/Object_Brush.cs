using UnityEngine;
using System.Collections;

// This scrips allows the brush to destroy or 'clean' objects with the Cleanable tag
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
