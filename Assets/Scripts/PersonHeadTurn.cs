using UnityEngine;
using System.Collections;

public class PersonHeadTurn : MonoBehaviour {
// Allows NPC to creepily follow the player's movement with their heads

	private Person person;

	void Start () {
		person = this.gameObject.transform.parent.GetComponent<Person> ();
	}

	void Update() {
		if (person.isActivated) {
			Transform target = GameObject.FindWithTag ("Player").transform.FindChild("Camera (eye)");
			Vector3 dir = target.position - transform.position;
			dir.Normalize ();
			transform.LookAt (target);
		}
	}

}
