using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
	//Config parameters
	[SerializeField] public float gameTimeAtStart;

	//Cache
	float gameTime;
	AgeStateController asc;
	
	void Start()
	{
		gameTime = gameTimeAtStart;
		asc = FindObjectOfType<AgeStateController>();
	}

	void Update()
	{
		CountDown();
	}

	private void CountDown()
	{
		gameTime -= Time.deltaTime;

		if(gameTime <= 0)
		{
			asc.SetAgeStateToNext();
			gameTime = gameTimeAtStart;
		}
	}

	public void AddTime(float timeToAdd)
	{
		gameTime += timeToAdd;
	}

	public float FetchGameTime()
	{
		return gameTime;
	}
}
