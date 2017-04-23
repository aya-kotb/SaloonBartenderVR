using UnityEngine;
using System.Collections;

public class Object_BottleOpener : MonoBehaviour {
// Unparents the Cork of any BeerBottle object that comes into contact with this

	private Player player;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "BeerBottle") {
			GameObject cork = col.gameObject.transform.FindChild ("Cork").gameObject;

			this.GetComponent<AudioSource>().Play();

			cork.transform.parent.gameObject.name = "BeerBottleOpen";
			cork.transform.parent = null;
			cork.GetComponent<Rigidbody>().isKinematic = false;
			cork.GetComponent<Rigidbody>().useGravity = true;
			cork.GetComponent<BoxCollider> ().enabled = true;

		}
	}


}
