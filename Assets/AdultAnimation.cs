using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultAnimation : MonoBehaviour
{
	[SerializeField] GameObject legs;
	[SerializeField] GameObject torso;
	[SerializeField] GameObject high5;

	public void EnableHigh5()
	{
		high5.SetActive(true);
		torso.SetActive(false);
	}
	public void DisableHigh5()
	{
		high5.SetActive(false);
		torso.SetActive(true);
	}

}
