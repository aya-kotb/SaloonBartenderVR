using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object_SpillageSurface : MonoBehaviour {

	//public ParticleCollisionEvent[] collisionEvents;
	public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
	public ParticleSystem ps;
	public int num;

	public GameObject spill;

	void Start () {
		ps = this.gameObject.GetComponent <ParticleSystem> ();
	}

	void OnParticleCollision(GameObject other) {
		GameObject[] objs = GameObject.FindGameObjectsWithTag ("Cleanable");
		//int objs = 10;
		if (other.tag == "Cleanable" && other.transform.localScale.x < 1.0) {
			other.transform.localScale += new Vector3 (0.01f, 0, 0.01f);
		}

		else if (other.name == "Glass") {

		}

		else if (objs.Length < 35 && other.name != "Glass") {
			num = ps.GetCollisionEvents (other, collisionEvents);
			Vector3 pos = collisionEvents [0].intersection;
			GameObject newSpill = (GameObject)Instantiate (spill, pos, Quaternion.identity);

		} 

		else {
		}
	}
	
	
	
}
