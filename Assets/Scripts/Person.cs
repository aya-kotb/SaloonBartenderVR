using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person : MonoBehaviour {

	public Dictionary<string, DialogueSegment> dialogueTree = new Dictionary<string, DialogueSegment>();
	private DialogueController dialogueController;
	private GameController gameController;
	private Player player;
	public bool isActivated;
	private Quaternion originalRot;

	public string personName;
	public string nextDialogue;
	public string memory;
	public AudioClip[] audioClips;
	public AudioSource audio;
	public AudioClip drinkingAudio;

	public GameObject wish;
	public SpriteRenderer wishCurrentSprite;
	public string wishCurrent;
	public bool hasWish;

	public GameObject currentDrink;
	
	public List<string> memories = new List<string>();


	void Start() {
		wish = this.transform.FindChild ("Wish").gameObject;
		wishCurrentSprite = wish.transform.FindChild("WishBubble").FindChild("WishCurrent").GetComponent<SpriteRenderer>();
		wish.SetActive (false);
		hasWish = false;

		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		dialogueController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<DialogueController> ();
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		audio = this.GetComponent<AudioSource>();

		isActivated = false;
		AssignDialogue ();
		StartCoroutine ("Wishing");
	}

	IEnumerator Wishing () {
		// Give wish to talk
		if (nextDialogue != "") {
			wish.SetActive (true);
			hasWish = true;
			wishCurrentSprite.sprite = gameController.wishConversation;
		}

		while (true) {
			yield return new WaitForSeconds (Random.Range (2.0f, 8.0f));
			if (!hasWish) {
				int rand = Random.Range (1, 10);
				if (rand < 4) {
					wishCurrent = "Beer";
					wishCurrentSprite.sprite = gameController.wishBeer;
				} else if (rand < 7) {
					wishCurrent = "Whiskey";
					wishCurrentSprite.sprite = gameController.wishWhiskey;
				} else {
					wishCurrent = "Trouble";
					wishCurrentSprite.sprite = gameController.wishTrouble;
				}
				hasWish = true;
				wish.SetActive (true);
			}
		}
	}

	public void WishGranted () {
		wishCurrent = "";
		hasWish = false;
		wish.SetActive (false);
	}
	
	void HideWishing() {
		wish.SetActive (false);
	}
	
	IEnumerator OnActivation () {
		if (wishCurrent == "Trouble" && player.isThreatening) {
			if (player.isThreateningLeft) {
				player.handLeft.carryObject.GetComponent<AudioSource>().Play ();
			} else {
				player.handRight.carryObject.GetComponent<AudioSource>().Play ();
			}
			WishGranted();
		} else if (wishCurrent == "Drink" && player.isHoldingBooze) {
			if (player.isHoldingBoozeLeft) {
				player.handLeft.GiveDrink(this.gameObject);
			} else {
				player.handRight.GiveDrink(this.gameObject);
			}
			HideWishing();
			audio.clip = drinkingAudio;
			audio.Play ();
			yield return new WaitForSeconds (Random.Range (3.0f, 5.0f));
			WishGranted ();
			Destroy (currentDrink);
		} else {
			if (!isActivated && !dialogueController.dialoguePlaying && nextDialogue != "") {
				isActivated = true;
				originalRot = transform.rotation;
				dialogueController.conversationPartner = this;
				StartCoroutine (dialogueController.Dialogue ());
				while (isActivated) {
					yield return null;
				}
				yield return new WaitForSeconds (0.5f);
				transform.rotation = originalRot;
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		if (wishCurrent == "Beer") {
			if (col.gameObject.name == "BeerBottleOpen") {
				StartCoroutine(AcceptDrink (col.gameObject));
			}
		}

		if (wishCurrent == "Whiskey") {
			if (col.gameObject.name == "Whiskey-filled Glass") {
				StartCoroutine(AcceptDrink (col.gameObject));
			}
		}

	}

	public IEnumerator AcceptDrink(GameObject go) {
		go.transform.parent = this.gameObject.transform;
		go.transform.rotation = Quaternion.identity;
		go.transform.localPosition = new Vector3(0.5f, -1.5f, 0.2f);
		//go.transform.localScale = new Vector3 (0.3f, 0.4f, 0.3f);
		this.currentDrink = go;
		go = null;
		HideWishing();
		audio.clip = drinkingAudio;
		audio.Play ();
		yield return new WaitForSeconds (Random.Range (3.0f, 5.0f));
		WishGranted ();
		Destroy (currentDrink);
	}


	public void AssignDialogue() {
		personName = this.gameObject.name;
		nextDialogue = personName + "_0";
		dialogueTree = dialogueController.returnDialogueTree(personName);
		audioClips = dialogueController.returnAudioClips(personName);
	}
	
}
