using UnityEngine;
using System.Collections;

public class Object_GlassFill : MonoBehaviour {
	public int fill;
	private int maxFill;
	private bool filled;
	public Material[] filledMaterial;
	private GameObject fillObject;
	void Start() {
		fillObject = transform.FindChild ("GlassFill").gameObject;
		fillObject.SetActive (false);
		maxFill = 100;
		fill = 0;
		filled = false;
	}

	void Update() {
		if (!filled) {
			if (fill > maxFill) {
				this.gameObject.name = "Whiskey-filled Glass";
				filled = true;
				fillObject.SetActive (true);
				//this.gameObject.GetComponent<MeshRenderer> ().material = filledMaterial[0];
			}
		}

	}

	void OnParticleCollision(GameObject other) {
		if (other.name == "PourParticle") {
			fill += 1;
		}
	}


}
