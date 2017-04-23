
// This class stores the parts of the dialogue used in conversations with Persons and StoryObjects

public class DialogueSegment {
	public int segmentId;
	public string segmentText;
	public bool hasChoice;
	public string nextSegment;
	public string nextSegmentPositive;
	public string nextSegmentNegative;
	public string speaker;

	// Segment (basic)
	public DialogueSegment (int segid, string segtext, string segnext, string speak) {
		segmentId = segid;
		segmentText = segtext;
		hasChoice = false;
		nextSegment = segnext;
		speaker = speak;
	}

	// Segment (decision moment)
	public DialogueSegment (int segid, string segtext, string segnextpos, string segnextmin, string speak) {
		segmentId = segid;
		segmentText = segtext;
		hasChoice = true;
		nextSegmentPositive = segnextpos;
		nextSegmentNegative = segnextmin;
		speaker = speak;
	}
}