using UnityEngine;
using System.Collections;

// !!! Hand can't be non-MonoBehaviour > think of solution to distinguishing left/right hand w/o extending class.
public class Player : MonoBehaviour {
	public Hand handLeft, handRight;

	public bool isThreatening;
	public bool isThreateningLeft, isThreateningRight;

	public bool isHoldingBooze;
	public bool isHoldingBoozeLeft, isHoldingBoozeRight;

	void Start() {
		handRight = GameObject.FindGameObjectWithTag ("HandRight").GetComponent<Hand>();
		handLeft = GameObject.FindGameObjectWithTag ("HandLeft").GetComponent<Hand>();
	}

	// Checks whether equipped items change player status
	public void DeterminePlayerStatus(Hand hand) {
		// Check if hand is empty
		if (hand.carryObject == null) {
			HandEmpty (hand);
		} 
		// Hand is holding an object
		else {
			// Left holding object
			if (hand == handLeft) {
				CheckLeftHand (hand);
			} 
			// Right holding object
			else {
				CheckRightHand (hand);
			}
		}
		SetPlayerStatus ();
	}

	// Check which hand is empty
	void HandEmpty(Hand hand) {
		// Left empty
		if (hand == handLeft) {
			isThreateningLeft = false;
			isHoldingBoozeLeft = false;
		} 
		//Right empty
		else {
			isThreateningRight = false;
			isHoldingBoozeRight = false;
		}
	}

	// Check for objects in left hand
	void CheckLeftHand(Hand hand) {
		// Holding shotgun
		if (hand.carryObject.name == "Shotgun") {
			isThreateningLeft = true;
			// Holding opened beerbottle
		} else if (hand.carryObject.name == "BeerBottleOpen") {
			isHoldingBoozeLeft = true;
		}
	}

	// Check for objects in left hand

	void CheckRightHand(Hand hand) {
		// Holding shotgun
		if (hand.carryObject.name == "Shotgun") {
			isThreateningRight = true;
			// Holding opened beerbottle
		} else if (hand.carryObject.name == "BeerBottleOpen") {
			isHoldingBoozeRight = true;
		}
	}

	// Check which hand is empty
	void SetPlayerStatus() {
		// Set player isThreatening
		if (isThreateningLeft || isThreateningRight) {
			isThreatening = true;
		} else {
			isThreatening = false;
		}

		// Set player isHoldingBooze
		if (isHoldingBoozeLeft || isHoldingBoozeRight) {
			isHoldingBooze = true;
		} else {
			isHoldingBooze = false;
		}
	}

} // End
