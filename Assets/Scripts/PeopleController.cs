using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleController : MonoBehaviour
{
	//Cache
	[SerializeField] float timeToAdd; //TO DO : Will want to put this in another script eventually
	TimeKeeper timeKeeper;
	void Start()
	{
		timeKeeper = FindObjectOfType<TimeKeeper>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		//timeKeeper.AddTime(timeToAdd);
		if(other.tag == "Player")
		{
			timeKeeper.AddTime(timeToAdd);
		}
	}
}
