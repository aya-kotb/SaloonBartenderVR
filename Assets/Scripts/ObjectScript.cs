using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectScript : MonoBehaviour {

	private AudioSource audioSource;
	public bool isActivated;
	private string objectName;
	private string dialogueText;

	private DialogueController dialogueController;

	public Dictionary<string, DialogueSegment> dialogueTree = new Dictionary<string, DialogueSegment>();
	
	void Start() {
		dialogueController = GameObject.FindGameObjectWithTag("GameController").GetComponent<DialogueController> ();
		isActivated = false;
		audioSource = GetComponent<AudioSource> ();
		AssignDialogue ();
	}

	IEnumerator OnActivation () {
		if (!isActivated && !dialogueController.dialoguePlaying) {
			isActivated = true;
			dialogueController.dialoguePlaying = true;
			print (this.gameObject.name + " was activated!");

			audioSource.Play ();

			dialogueController.subtitles.text = dialogueController.formatDialogueText(dialogueText, dialogueController.subtitleLength);
			while (audioSource.isPlaying) {
				yield return null;
			}
			dialogueController.subtitles.text = "";
			yield return new WaitForSeconds (0.5f);
			isActivated = false;
			dialogueController.dialoguePlaying = false;
		}
	}

	public void AssignDialogue() {
		objectName = this.gameObject.name;
		dialogueTree = dialogueController.returnDialogueTree(objectName);
		audioSource.clip = dialogueController.objectAudioClips [dialogueController.objectDialogueTree[objectName].segmentId];
		dialogueText = dialogueTree [objectName].segmentText;
	}
	

}
