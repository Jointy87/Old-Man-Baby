using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AgeStateController : MonoBehaviour
{
	//Config parameters
	[SerializeField] GameObject[] playerObjects;
	[SerializeField] Animator[] playerAnimators;
	[SerializeField] Rigidbody2D[] playerRigidBodies;
	[SerializeField] CinemachineVirtualCamera followCam;

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

	public void HandleAgeStateChange()
	{
		SetNextAgeState();
		AssignCorrectAnimator();
		AssignCorrectRigidbody();
		ActivateCorrectGameObject();
		SetFollowCamToCorrectTransform();
	}

	private void SetFollowCamToCorrectTransform()
	{
		followCam.Follow = activeObject.transform;
	}

	private void ActivateCorrectGameObject()
	{
		previousObject = activeObject;
		previousObject.SetActive(false);
		activeObject = playerObjects[objectIndex + 1];
		activeObject.SetActive(true);
		activeObject.transform.position =
			new Vector2(previousObject.transform.position.x, previousObject.transform.position.y + 1);
	}

	private void AssignCorrectRigidbody()
	{
		currentRB = playerRigidBodies[currentRBIndex + 1];
		pc.SetCurrentRigidbody(currentRB);
	}

	private void AssignCorrectAnimator()
	{
		currentAnimator = playerAnimators[currentAnimatorIndex + 1];
		pc.SetCurrentAnimator(currentAnimator);
	}

	private void SetNextAgeState()
	{
		int stateIndex = (int)currentAge;
		AgeState nextAgeState = (AgeState)stateIndex + 1;
		currentAge = nextAgeState;
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
