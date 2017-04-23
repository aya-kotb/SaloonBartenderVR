using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Object_Story : MonoBehaviour {
// This script is attached to objects that can be activated for dialogue similar to Persons.
// These objects can also be manipulated by attaching the other, more specific, object scripts. 

	// Object
	public string objectName;
	public bool isActivated;

	// Dialogue
	private string dialogueText;
	public AudioSource audioSource;
	public float dialoguePause;
	public Dictionary<string, DialogueSegment> dialogueTree = new Dictionary<string, DialogueSegment>();

	// References
	private DialogueController dialogueController;

	void Start() {
		dialogueController = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueController> ();
		audioSource = GetComponent<AudioSource> ();
		isActivated = false;
		dialoguePause = 0.5f;
		AssignDialogue ();
	}

	IEnumerator OnActivation () {
		// Check if no other dialogue is currently playing
		if (!isActivated && !dialogueController.dialoguePlaying) {
			isActivated = true;
			dialogueController.dialoguePlaying = true;
			print (this.gameObject.name + " was activated!");
			// Plays audio associated with current dialogue choice
			audioSource.Play ();
			// Update subtitles
			dialogueController.subtitles.text = dialogueController.formatDialogueText(dialogueText, dialogueController.subtitleLength);
			while (audioSource.isPlaying) {
				yield return null;
			}
			// Reset subtitles
			dialogueController.subtitles.text = "";
			yield return new WaitForSeconds (dialoguePause);
			isActivated = false;
			dialogueController.dialoguePlaying = false;
		}
	}

	// This function is called at Start to assign the correct dialogue to the object based on its name
	public void AssignDialogue() {
		objectName = this.gameObject.name;
		dialogueTree = dialogueController.returnDialogueTree(objectName);
		audioSource.clip = dialogueController.objectAudioClips [dialogueController.objectDialogueTree[objectName].segmentId];
		dialogueText = dialogueTree [objectName].segmentText;
	}
	

}
