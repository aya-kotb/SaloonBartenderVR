using UnityEngine;
using System.Collections;

public class RespawnOnTouch : MonoBehaviour {

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.name == "Shotgun") {
			col.gameObject.transform.position = new Vector3 (2.7f, 0.8f, -1.5f);
			col.gameObject.transform.rotation = Quaternion.identity;
		} else if (col.gameObject.name == "Brush") {
			col.gameObject.transform.position = new Vector3 (3.5f, 1.5f, -2f);
			col.gameObject.transform.rotation = Quaternion.identity;
		}
	}
}
