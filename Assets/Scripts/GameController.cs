using UnityEngine;
using System.Collections;
using System;
using VRTK;

// !!! Combine both raycasts to reduce number of unnecessary raycasts
public class GameController : MonoBehaviour {
	
	public GameObject storedObj;

	// Wishing
	public Sprite wishConversation;
	public Sprite wishChoice;
	public Sprite wishWhiskey;
	public Sprite wishBeer;
	public Sprite wishTrouble;

	// References
	private GameObject player;
	private VRTK_ControllerEvents leftController;
	private VRTK_ControllerEvents rightController;

	void Start() {
		storedObj = this.gameObject; // *only assigned so storedObj is not empty
		player = GameObject.FindGameObjectWithTag ("Player");
		leftController = player.transform.FindChild ("Controller (left)").GetComponent<VRTK_ControllerEvents> ();
		rightController = player.transform.FindChild ("Controller (right)").GetComponent<VRTK_ControllerEvents> ();
	}

	void Update() {
		// Checks for input from triggers or Q (used for testing)
		if (Input.GetKeyDown (KeyCode.Q) || leftController.triggerTouched || rightController.triggerTouched){
			Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0.0f)); 
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				// Send Activation message
				hit.collider.SendMessageUpwards("OnActivation");
			}
		}
	}

	void FixedUpdate() {
	// Select Object/Person on Look
		// Cast ray from center of the screen
		Ray ray = Camera.main.ViewportPointToRay (new Vector3(0.5f, 0.5f, 0.0f)); 
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			HitSelection (hit);
		}
	}

	// Select and Deselects the objects hit by the raycast
	void HitSelection (RaycastHit hit) {
		// Check if collider has the Selector component e.g. if it can be selected
		if (hit.collider.gameObject.GetComponent<Selector>() != null) {
			GameObject obj = hit.collider.gameObject;
			Select (obj);
			// Set new storedObject
			if (storedObj == this.gameObject) {
				storedObj = obj;
			}
			// Delect old object, select new object
			if (obj != storedObj) {
				Deselect (storedObj);
				Select (obj);
				storedObj = obj;
			} else {
				Select (obj);
			}
		}
		// Deselect the previously selected object, if possible
		else {
			if (storedObj != this.gameObject) {
				try {
					Deselect (storedObj);
				}
				catch (Exception e) {
					print (e);
				}
			}
		}
	}

	void Select (GameObject obj) {
		obj.GetComponent<Selector>().Select();
	}

	void Deselect (GameObject obj) {
		obj.GetComponent<Selector>().Deselect();
	}
	
	void OnGUI(){
		// Draw the targeting reticle in the center of the screen
		GUI.Box(new Rect(Screen.width/2,Screen.height/2, 10, 10), "");
	}

} // End
