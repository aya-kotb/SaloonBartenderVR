using UnityEngine;
using System.Collections;

public class Object_GlassFill : MonoBehaviour {
// Allows the object to track how much liquid has been pored 'into' (onto x_X) it. 

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
			}
		}
	}

	void OnParticleCollision(GameObject other) {
		if (other.name == "PourParticle") {
			fill += 1;
		}
	}

} // End
