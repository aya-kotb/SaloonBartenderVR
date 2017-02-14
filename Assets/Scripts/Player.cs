using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Hand handLeft;
	public Hand handRight;

	public bool isThreatening;
	public bool isHoldingBooze;

	public bool isThreateningLeft;
	public bool isThreateningRight;

	public bool isHoldingBoozeLeft;
	public bool isHoldingBoozeRight;
	
	void Start() {
		handRight = GameObject.FindGameObjectWithTag ("HandRight").GetComponent<Hand>();
		handLeft = GameObject.FindGameObjectWithTag ("HandLeft").GetComponent<Hand>();
	}

	public void SetPlayerStatus(Hand hand) {
		// Check if hand is empty
		if (hand.carryObject == null) {
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
		// Hand is holding an object
		else {
			// Left holding object
			if (hand == handLeft) {
				// Holding shotgun
				if (hand.carryObject.name == "Shotgun") {
					isThreateningLeft = true;
				// Holding opened beerbottle
				} else if (hand.carryObject.name == "BeerBottleOpen") {
					isHoldingBoozeLeft = true;
				}
			} 

			// Right holding object
			else {
				// Holding shotgun
				if (hand.carryObject.name == "Shotgun") {
					isThreateningRight = true;
					// Holding opened beerbottle
				} else if (hand.carryObject.name == "BeerBottleOpen") {
					isHoldingBoozeRight = true;
				}
			}
		}
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
	
	
	
	
	
}
