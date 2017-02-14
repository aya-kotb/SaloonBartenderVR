using UnityEngine;
using System.Collections;

public class SelectorName : MonoBehaviour {
	private TextMesh nameTag;

	void Start () {
		nameTag = this.gameObject.transform.FindChild ("Name").GetComponent<TextMesh> ();
		nameTag.text = "";
	}

	public void Select() {
		nameTag.text = this.name;
	}

	public void Deselect() {
		nameTag.text = "";
	}
}
