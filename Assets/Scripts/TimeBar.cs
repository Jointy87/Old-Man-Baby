using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
	//Config parameters
	[SerializeField] Image timeFill;

	//Cache
	TimeKeeper timeKeeper;

	void Start()
	{
		timeKeeper = GetComponent<TimeKeeper>();
	}

	void Update()
	{
		EmptyFill();
	}

	private void EmptyFill()
	{
		timeFill.fillAmount = timeKeeper.FetchGameTime() / timeKeeper.gameTimeAtStart;

		if (timeFill.fillAmount >= 1)
		{
			timeFill.fillAmount = 1;
		}
	}
}
