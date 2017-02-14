using UnityEngine;
using System.Collections;

public class Object_BottleOpener : MonoBehaviour {

	private Player player;
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "BeerBottle") {
			GameObject cork = col.gameObject.transform.FindChild ("Cork").gameObject;

			this.GetComponent<AudioSource>().Play();
			//player.SetPlayerStatus (player.handLeft);
			//player.SetPlayerStatus(player.handRight);

			cork.transform.parent.gameObject.name = "BeerBottleOpen";
			cork.transform.parent = null;
			cork.GetComponent<Rigidbody>().isKinematic = false;
			cork.GetComponent<Rigidbody>().useGravity = true;
			cork.GetComponent<BoxCollider> ().enabled = true;

		}
	}

	/*
	 * 	void OnCollisionEnter(Collision col) {
		if (col.gameObject.name == "Cork") {
			col.gameObject.name = "Cork ";
			col.gameObject.transform.parent.gameObject.name = "BeerBottleOpen";
			this.GetComponent<AudioSource>().Play();
			player.SetPlayerStatus (player.handLeft);
			player.SetPlayerStatus(player.handRight);
			col.gameObject.transform.parent = null;
			col.gameObject.GetComponent<Rigidbody>().isKinematic = false;
			col.gameObject.GetComponent<Rigidbody>().useGravity = true;

		}
	}*/



}
