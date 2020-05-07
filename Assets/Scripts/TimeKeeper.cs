using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
	//Config parameters
	[SerializeField] float gameTimeAtStart;

	//Cache
	float gameTime;
	
	void Start()
	{
		gameTime = gameTimeAtStart;
	}

	void Update()
	{
		CountDown();
	}

	private void CountDown()
	{
		gameTime -= Time.deltaTime;
	}

	public void AddTime(int timeToAdd)
	{
		gameTime += timeToAdd;
	}
}
