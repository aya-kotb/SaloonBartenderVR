using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VRTK;

public class DialogueController : MonoBehaviour {

	// !!! Currently storing everything in separate Dicts, will come up with a better plan
	public Dictionary<string, DialogueSegment> playerDialogueTree = new Dictionary<string, DialogueSegment>();
	public AudioClip[] playerAudioClips;
	public Dictionary<string, DialogueSegment> objectDialogueTree = new Dictionary<string, DialogueSegment>();
	public AudioClip[] objectAudioClips;
	public Dictionary<string, DialogueSegment> paquitoDialogueTree = new Dictionary<string, DialogueSegment>();
	public AudioClip[] paquitoAudioClips;
	public Dictionary<string, DialogueSegment> abigailDialogueTree = new Dictionary<string, DialogueSegment>();
	public AudioClip[] abigailAudioClips;
	public Dictionary<string, DialogueSegment> desperadoDialogueTree = new Dictionary<string, DialogueSegment>();
	public AudioClip[] desperadoAudioClips;
	public Dictionary<string, DialogueSegment> lucyDialogueTree = new Dictionary<string, DialogueSegment>();
	public AudioClip[] lucyAudioClips;

	public Person conversationPartner; 
	public Player player;
	public TextMesh subtitles;

	public int subtitleLength;
	public AudioSource speaker;
	
	public bool dialoguePlaying;
	public bool choosing;
	
	public AudioClip currentDialogue;
	public DialogueSegment currentDialogueSegment;

	private VRTK_ControllerEvents leftController;
	private VRTK_ControllerEvents rightController;
	
	void Awake () {
	// !!! Will be read from Excel in later version
		paquitoDialogueTree.Add ("Paquito_0", new DialogueSegment (0, "A vaquero without equal, this dashing, young Mexican", "Paquito_1", "P"));
		paquitoDialogueTree.Add ("Paquito_1", new DialogueSegment (1, "Que tal, amigo? Yo quiero una cerveza por favor. Una, eh, beer.", "Paquito_2", "O"));
		paquitoDialogueTree.Add ("Paquito_2", new DialogueSegment (2, "He looked like he could use one, so I poured him a drink.", "", "P"));

		abigailDialogueTree.Add ("Abigail_0", new DialogueSegment (0, "While almost of marrying age and certainly a looker, none of the regular patrons would dare lay a finger on Abigail.", "Abigail_1", "O"));
		abigailDialogueTree.Add ("Abigail_1", new DialogueSegment (1, "The many drifters who attempted such a feat were prone to lose that finger, on account of her being the Sheriff's daughter and all.", "", "P"));

		lucyDialogueTree.Add ("Lucy_0", new DialogueSegment (0, "Hey there barkeep, can I git a cold beer for Mr. Macintosh?", "Lucy_1", "O"));
		lucyDialogueTree.Add ("Lucy_1", new DialogueSegment (1, "Of course doll, I'll serve you a couple immediately. I could never stand the way that boor treated dear little Lucy. But, she was his property, so not much I could do about it.", "", "P"));

		objectDialogueTree.Add ("Painting", new DialogueSegment (0, "This dingy old frame depicts a horse. Or an ox. No one really knows with this savage art. Still, mighty beautiful indeed.", "", "P"));
		objectDialogueTree.Add ("Cup", new DialogueSegment (1, "As clean as it gets around here.", "", "P"));

		desperadoDialogueTree.Add ("Desperado_0", new DialogueSegment (0, "A vaguely familiar figure appears in the window, unawares of his poorly devised hiding place. His hands twitch slightly and a nervous trickle of sweat runs across his bandana-covered face. When he pulls out his gun to check the ammo, I know what time it is.","Desperado_a1" , "Desperado_b1" ,"P"));
		desperadoDialogueTree.Add ("Desperado_a1", new DialogueSegment (1, "Come in here and you’ll have to contend with my double-barrelled Ithaca, young ‘un. That, or you’ll be chained up and won’t be seeing no sunlight for a looong while.", "Desperado_a2" , "P"));
		desperadoDialogueTree.Add ("Desperado_a2", new DialogueSegment (2, "I-I ain’t afraid of you an yer peashooter! *I need that money* H-hands where I can see ‘em!", "Desperado_a3" , "O"));
		desperadoDialogueTree.Add ("Desperado_a3", new DialogueSegment (3, "The young desperado raises his gun, a desperate look on his face. There ain’t no fire and brimstone behind those eyes. Not a lot of smarts either, else he would’ve seen the Sheriff sneak up behind him. ", "Desperado_a4" , "P"));
		desperadoDialogueTree.Add ("Desperado_a4", new DialogueSegment (4, "Hands up or I’ll blast ya in half!", "Desperado_a5" , "P"));
		desperadoDialogueTree.Add ("Desperado_a5", new DialogueSegment (5, "As Sheriff cuffs the boy and removes the bandana, a pang of regret fills me.  It’s the old marshall’s boy, Lewis. Such a waste of a good life. Perhaps he’ll learn to appreciate life more when he's spent some of it behind bars.", "" , "P"));
		desperadoDialogueTree.Add ("Desperado_b1", new DialogueSegment (6, "You best not be considering what you seem to be considering, boy. The Sheriff around here ain’t kind to desperados. You best be ready to face the consequences of pulling that gun out in public, boy. Once you go down that road, there ain’t no turning back. ", "Desperado_a2" , "P"));
		desperadoDialogueTree.Add ("Desperado_b2", new DialogueSegment (7, "But, w-why are you always bein’ so nice? I-I can’t do this.. I just need ta eat..", "Desperado_a3" , "O"));
		desperadoDialogueTree.Add ("Desperado_b3", new DialogueSegment (8, "The young desperado holsters his gun, a defeated look on his face. There ain’t no fire and brimstone behind those eyes. He removes the bandana and I recognize him: Sawyer, old marshall’s son. Why don’t you calm down and come inside for drink, Sawyer. It’s been a while.", "Desperado_a4" , "P"));
		desperadoDialogueTree.Add ("Desperado_b4", new DialogueSegment (9, "I can’t just..*pause* I’m sorry. Thank you. Thank you so much..", "" , "O"));
	// !!!
	}

	
	void Start () {
		dialoguePlaying = false;
		choosing = false;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
		subtitles = GameObject.Find("Subtitles").GetComponent<TextMesh> ();
		subtitleLength = 50;

		leftController = player.gameObject.transform.FindChild ("Controller (left)").GetComponent<VRTK_ControllerEvents> ();
		rightController = player.gameObject.transform.FindChild ("Controller (right)").GetComponent<VRTK_ControllerEvents> ();
	}
	
	void Update () {
		if (choosing) {
			// Negative dialogue choice (NO)
			if (Input.GetKeyDown(KeyCode.Z) || leftController.touchpadPressed) {
				MakeChoice(currentDialogueSegment.nextSegmentNegative, -1);
			}
			// Positive dialogue choice (YES)
			if (Input.GetKeyDown(KeyCode.X) || rightController.touchpadPressed) {
				MakeChoice(currentDialogueSegment.nextSegmentPositive, 1);
			}
		}
	}
		
	public void MakeChoice(string id, int choic) {
		// Store dialogue decision
		conversationPartner.memories.Add(id);
		
		// Next segment
		if (choic < 0) {
			conversationPartner.nextDialogue = currentDialogueSegment.nextSegmentNegative;
		}
		else {
			conversationPartner.nextDialogue = currentDialogueSegment.nextSegmentPositive;
		}
		
		choosing = false;
	}

	public IEnumerator Dialogue () {
		dialoguePlaying = true;
		while (dialoguePlaying == true) {
			currentDialogueSegment = conversationPartner.dialogueTree[conversationPartner.nextDialogue];
			
			// Determine Speaker
			if (currentDialogueSegment.speaker == "P") {
				speaker = player.gameObject.GetComponent<AudioSource>();
			} 
			else if (currentDialogueSegment.speaker == "O") {
				speaker = conversationPartner.gameObject.GetComponent<AudioSource>();
			}
			else {
				dialoguePlaying = false;
				conversationPartner.isActivated = false;
				StopCoroutine("Dialogue");
			}
			
			// Segment has a choice
			if (conversationPartner.dialogueTree[conversationPartner.nextDialogue].hasChoice) {
				// Play and load audioclip
				currentDialogue = conversationPartner.audioClips[currentDialogueSegment.segmentId];
				speaker.clip = currentDialogue;
				print (currentDialogue.name);
				speaker.Play ();

				if (currentDialogueSegment.speaker == "O") {
					conversationPartner.gameObject.transform.Find ("Head/Mouth").GetComponent<Animator>().SetBool("Talking", true);
				}

				subtitles.text = formatDialogueText(currentDialogueSegment.segmentText, subtitleLength);

				while (speaker.isPlaying) {
					yield return null;
				}

				conversationPartner.gameObject.transform.Find ("Head/Mouth").GetComponent<Animator>().SetBool("Talking", false);

				// Enter choosing state
				choosing = true;
				InitiateChoosingState ();
				while (choosing) {
					yield return null;
				}
				subtitles.text = "";
			}
			// Segment does not have a choice
			else {
				// Play and load audioclip
				currentDialogue = conversationPartner.audioClips[currentDialogueSegment.segmentId];
				speaker.clip = currentDialogue;
				print (currentDialogue.name);
				speaker.Play ();
				subtitles.text = formatDialogueText(currentDialogueSegment.segmentText, subtitleLength);

				// Set talking animation
				if (currentDialogueSegment.speaker == "O") {
					conversationPartner.gameObject.transform.Find ("Head/Mouth").GetComponent<Animator>().SetBool("Talking", true);
				}
				while (speaker.isPlaying) {
					yield return null;
				}
				conversationPartner.gameObject.transform.Find ("Head/Mouth").GetComponent<Animator>().SetBool("Talking", false);
				subtitles.text = "";
				conversationPartner.nextDialogue = currentDialogueSegment.nextSegment;
			}

			// End conversation if there is no following segment
			if (conversationPartner.nextDialogue == "") {
				dialoguePlaying = false;
				conversationPartner.WishGranted();
				conversationPartner.isActivated = false;
			}
			yield return new WaitForSeconds (0.5f);
		}
	}
	
	void InitiateChoosingState() {
		// show GUI element: "Press A for YES, Press D for NO"
	}
	
	void ExitChoosingState() {
		// show confirmation of choice
		
		// play confirmation sound
		
		// hide GUI element
	}

	public AudioClip[] returnAudioClips (string personName) {
		switch (personName) {
		case "Paquito":
			return paquitoAudioClips;
		case "Abigail":
			return abigailAudioClips;
		case "Desperado":
			return desperadoAudioClips;
		case "Lucy":
			return lucyAudioClips;
		default:
			return paquitoAudioClips;
		}
	}

	public Dictionary<string, DialogueSegment> returnDialogueTree (string personName) {
		switch (personName) {
		case "Paquito":
			return paquitoDialogueTree;
		case "Abigail":
			return abigailDialogueTree;
		case "Desperado":
			return desperadoDialogueTree;
		case "Lucy":
			return lucyDialogueTree;
		default:
			return objectDialogueTree;
		}
	}

	public string formatDialogueText (string input, int sentenceLength) {
		// Wrap text by line height
		// Split string by char " "         
		string[] words = input.Split (" " [0]);
		string result = "";
		string line = "";
		// Iterate through all words    
		foreach (string s in words) {
			// Append current word into line
			string temp = line + " " + s;
			// If line length is bigger than lineLength
			if (temp.Length > sentenceLength) {
				// Append current line into result
				result += line + "\n";
				// Remain word append into new line
				line = s;
			}
			// Append current word into current line
			else {
				line = temp;
			}
		}
		result += line;
		return result.Substring (1, result.Length - 1);
	}

} // End














