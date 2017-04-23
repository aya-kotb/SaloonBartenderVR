using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {
// Allows this object to be hovered over with the targeting reticule, showing its name and/or highlighting it

	private TextMesh nameTag;
	private DialogueController dialogueController;

	void Start () {
		dialogueController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<DialogueController> ();

		nameTag = this.gameObject.transform.FindChild ("Name").GetComponent<TextMesh> ();
		nameTag.text = "";
	}

	public void Select() {
		GetComponent<MeshRenderer> ().material.color = Color.red;
		nameTag.text = this.name;
		if (this.GetComponent<Person> () != null) {
			if (dialogueController.choosing && dialogueController.conversationPartner.name == this.name) {
				nameTag.text = "-" + this.name + "+";
			}
		}
	}

	public void Deselect() {
		GetComponent<MeshRenderer> ().material.color = Color.white;
		nameTag.text = "";
	}

}
