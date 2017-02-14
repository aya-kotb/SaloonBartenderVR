using UnityEngine;
using System.Collections;

public class ObjectText : MonoBehaviour {
	Transform target ;
	// Use this for initialization
	void Start () {
		target = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt (2 * transform.position - target.position);
	}
}
