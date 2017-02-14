using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HandChanger : MonoBehaviour {
	private VRTK_ControllerEvents controller;
	private GameObject model;
	public Mesh handThumb;
	public Mesh handRegular;

	// Use this for initialization
	void Start () {
		controller = this.GetComponent<VRTK_ControllerEvents> ();
		model = this.transform.FindChild ("Hand(Clone)").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.touchpadPressed){
			float angle = controller.GetTouchpadAxisAngle ();
			if (angle > 0 && angle < 180) {
				print ("right");
				//if (model.GetComponent<MeshFilter>().mesh
			} else {
				print ("left");
			}
		}
	}
}
