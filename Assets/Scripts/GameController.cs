using UnityEngine;
using System.Collections;
using System;
using VRTK;

public class GameController : MonoBehaviour {

	public Texture highlighted;
	public Texture notHighlighted;

	public GameObject storedObj;

	public Sprite wishConversation;
	public Sprite wishChoice;
	public Sprite wishWhiskey;
	public Sprite wishBeer;
	public Sprite wishTrouble;

	private GameObject player;
	private VRTK_ControllerEvents leftController;
	private VRTK_ControllerEvents rightController;

	void Start() {
		storedObj = this.gameObject;
		player = GameObject.FindGameObjectWithTag ("Player");
		leftController = player.transform.FindChild ("Controller (left)").GetComponent<VRTK_ControllerEvents> ();
		rightController = player.transform.FindChild ("Controller (right)").GetComponent<VRTK_ControllerEvents> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Q) || leftController.triggerTouched || leftController.triggerTouched){
			Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0.0f)); 
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				print (hit.transform.name);
				hit.collider.SendMessageUpwards("OnActivation");
			}
		}
	}

	void FixedUpdate() {
		// Select Object/Person on Look
		Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0.0f)); 
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if (hit.collider.gameObject.GetComponent<Selector>() != null) {
				GameObject obj = hit.collider.gameObject;
				obj.GetComponent<Selector>().Select();
				if (storedObj == this.gameObject) {
					storedObj = obj;
				}
				if (obj != storedObj) {
					storedObj.GetComponent<Selector>().Deselect();
					obj.GetComponent<Selector>().Select();
					storedObj = obj;
				} else {
					obj.GetComponent<Selector>().Select();
				}
			}
			else {
				if (storedObj != this.gameObject) {
					try {
						storedObj.GetComponent<Selector>().Deselect();
					}
					catch (Exception e) {
						print (e);
					}
				}
			}
		}
	}
	
	void OnGUI(){
		GUI.Box(new Rect(Screen.width/2,Screen.height/2, 10, 10), "");
	}



}
