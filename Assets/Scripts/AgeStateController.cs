using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeStateController : MonoBehaviour
{
	//Config parameters
	[SerializeField] GameObject[] playerObjects;
	[SerializeField] Animator[] playerAnimators;
	[SerializeField] Rigidbody2D[] playerRigidBodies;

	//Cache
	AgeState currentAge;
	Animator currentAnimator;
	int currentAnimatorIndex;
	PlayerController pc;
	Rigidbody2D currentRB;
	int currentRBIndex;
	GameObject activeObject;
	GameObject previousObject;
	int objectIndex;

	//States
	public enum AgeState { baby, youngster, adult, elder};


	void Awake()
	{
		currentAge = AgeState.baby;
		currentAnimator = playerAnimators[0];
		currentAnimatorIndex = 0;
		pc = GetComponent<PlayerController>();
		currentRB = playerRigidBodies[0];
		currentRBIndex = 0;
		activeObject = playerObjects[0];
		objectIndex = 0;
	}

	public void SetAgeStateToNext()
	{
		int stateIndex = (int)currentAge;
		AgeState nextAgeState = (AgeState)stateIndex + 1;
		currentAge = nextAgeState;

		currentAnimator = playerAnimators[currentAnimatorIndex + 1];

		previousObject = activeObject;
		previousObject.SetActive(false);
		activeObject = playerObjects[objectIndex + 1];
		activeObject.SetActive(true);
		activeObject.transform.position = new Vector2(previousObject.transform.position.x, previousObject.transform.position.y + 1);
		

		pc.SetCurrentAnimator(currentAnimator);
		pc.SetCurrentRigidbody(currentRB);
	}

	public AgeState FetchAgeState()
	{
		return currentAge;
	}

	public Animator FetchAnimatorController()
	{
		return currentAnimator;
	}

	public Rigidbody2D FetchRigidbody()
	{
		return currentRB;
	}
}
