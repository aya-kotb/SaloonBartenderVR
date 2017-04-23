using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object_SpillageSurface : MonoBehaviour {

	// Particle System
	public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
	public ParticleSystem ps;
	public int numberOfCollisions;

	// Spillage
	public GameObject spill;
	private int maxSpillage;

	void Start () {
		ps = this.gameObject.GetComponent <ParticleSystem> ();
		maxSpillage = 35;
	}

	void OnParticleCollision(GameObject other) {
		// Find spillage by tag
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Cleanable");

		// Check if the particle collides with a spillage
		if (other.tag == "Cleanable" && other.transform.localScale.x < 1.0) {
			// If it does, increase size of spillage
			other.transform.localScale += new Vector3 (0.01f, 0, 0.01f);
		}

		// Creates new spill
		else if (objs.Length < maxSpillage && other.name != "Glass") {
			numberOfCollisions = ps.GetCollisionEvents (other, collisionEvents);
			Vector3 pos = collisionEvents [0].intersection;
			GameObject newSpill = (GameObject)Instantiate (spill, pos, Quaternion.identity);
		} 

		else {
			// Do nothing
		}
	}
	
} // End
