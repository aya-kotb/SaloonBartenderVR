using UnityEngine;
using System.Collections;

// Rotates text on objects towards the player
public class ObjectText : MonoBehaviour {

	private GameObject player;
	private Transform target ;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		target = player.transform;
	}
	
	void Update () {
		transform.LookAt (2 * transform.position - target.position);
	}
}
