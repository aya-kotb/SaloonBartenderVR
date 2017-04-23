using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

// !!! Script still needs integration with new hand models
// This script determines the direction of thumb movements for YES and NO commands
public class ThumbDirection : MonoBehaviour {
	private VRTK_ControllerEvents controller;
	private GameObject model;
	public Mesh handThumb;
	public Mesh handRegular;

	void Start () {
		controller = this.GetComponent<VRTK_ControllerEvents> ();
		model = this.transform.FindChild ("Hand(Clone)").gameObject;
	}
	
	void Update () {
		if (controller.touchpadPressed){
			float angle = controller.GetTouchpadAxisAngle ();
			if (angle > 0 && angle < 180) {
				// right
			} else {
				// left
			}
		}
	}
}
