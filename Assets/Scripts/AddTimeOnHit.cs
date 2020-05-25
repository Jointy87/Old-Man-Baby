using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeOnHit : MonoBehaviour
{
	//Cache
	TimeKeeper timeKeeper;
	float timeToAddOnHit;
	void Awake()
	{
		timeKeeper = FindObjectOfType<TimeKeeper>();
		timeToAddOnHit = timeKeeper.timeToAddOnHit;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "BabyPlayer" || other.tag == "YoungsterPlayer" || other.tag == "AdultPlayer")
		timeKeeper.AddTime(timeToAddOnHit);
	}
}
