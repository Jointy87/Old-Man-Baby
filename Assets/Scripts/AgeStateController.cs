using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeStateController : MonoBehaviour
{
	//Config parameters
	[SerializeField] Animator[] playerAnimators;

	//Cache
	AgeState currentAge;

	//States
	public enum AgeState { baby, youngster, adult, elder};

	void Start()
	{
		currentAge = AgeState.baby;
	}

	public void SetAgeStateToNext()
	{
		int stateIndex = (int)currentAge;
		AgeState nextAgeState = (AgeState)stateIndex + 1;
		currentAge = nextAgeState;
	}

	public AgeState FetchAgeState()
	{
		return currentAge;
	}
}
