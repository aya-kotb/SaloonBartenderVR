using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour {

	private Player player;
	public GameObject carryObject;
	public bool isCarryingObject;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		isCarryingObject = false;
	}

	void Update() {
		if (isCarryingObject) {
			if (Input.GetKeyDown(KeyCode.R)) {
				DropItem ();
			}
		}
	}

	// Unset Hand as object's parent
	void DropItem() { 
		carryObject.transform.parent = null;
		carryObject.GetComponent<Rigidbody>().isKinematic = false;
		carryObject.GetComponent<Rigidbody>().useGravity = true;
		carryObject = null;
		isCarryingObject = false;
		player.SetPlayerStatus(this);
	}

	// Transfer 
	public void ServeDrink(GameObject person) {
		carryObject.transform.parent = person.transform;
		carryObject.transform.rotation = Quaternion.identity;
		carryObject.transform.localPosition = new Vector3(0.5f, -1.5f, 0.2f);
		carryObject.transform.localScale = new Vector3 (0.3f, 0.4f, 0.3f);
		person.GetComponent<Person>().currentDrink = carryObject;
		carryObject = null;
		isCarryingObject = false;
		player.SetPlayerStatus(this);
	}

	void OnCollisionStay(Collision col) {
		// Checks if object is interactable and not dirt/liquid
		if (col.gameObject.tag != "Cleanable" && col.gameObject.tag != "Immovable") {
			if (!isCarryingObject && Input.GetKeyDown(KeyCode.E)) {
				print ("Grabbing " + col.gameObject.name);
				// Set Hand as object's parent
				col.gameObject.transform.SetParent (this.transform.parent);
				col.gameObject.GetComponent<Rigidbody>().isKinematic = true;
				col.gameObject.GetComponent<Rigidbody>().useGravity = false;
				// Set rotation of held object
				ChangeHeldObjectRotation(col.gameObject);
				carryObject = col.gameObject;
				isCarryingObject = true;
				player.SetPlayerStatus(this);
			}
		}
	}

	// Places items on the Hand in the correct position
	// !!! Will be changed to reading required object rotation from individual object scripts
	void ChangeHeldObjectRotation(GameObject go) {
		// Right Hand positions only
		if (this.gameObject.name == "HandRight") {
			switch (go.name) {
			case "Shotgun":
				go.transform.localPosition = new Vector3 (0.85f, -0.4f, 2.2f);
				go.transform.localEulerAngles = new Vector3 (5.5f, 80.0f, 0.0f);
				break;
			case "Whiskey":
				go.transform.localPosition = new Vector3(0.8f, -0.7f, 1.8f);
				go.transform.localEulerAngles = new Vector3(-7f, -136f, 6f);
				break;
			case "BeerBottle":
				go.transform.localPosition = new Vector3(0.9f, -1f, 1.8f);
				go.transform.localEulerAngles = new Vector3(180f, 5f, 180f);
				break;
			case "BeerBottleOpen":
				go.transform.localPosition = new Vector3(0.9f, -1f, 1.8f);
				go.transform.localEulerAngles = new Vector3(180f, 5f, 180f);
				break;
			case "Glass":
				go.transform.localPosition = new Vector3(0.88f, -0.7f, 1.8f);
				go.transform.localEulerAngles = new Vector3(6.5f, -142f, 3f);
				break;
			case "Brush":
				go.transform.localPosition = new Vector3(1f, -0.9f, 1.9f);
				go.transform.localEulerAngles = new Vector3(-270f, -117f, 3f);
				break;
			default:
				go.transform.rotation = Quaternion.identity;
				break;
			}
		}
	}


// End
}
