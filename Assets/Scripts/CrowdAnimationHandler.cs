using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAnimationHandler : MonoBehaviour
{
	[SerializeField] GameObject torso;
	[SerializeField] GameObject legs;
	[SerializeField] GameObject high5;
	[SerializeField] Transform syncTransform;
	[SerializeField] float syncOffset = -0.15f;

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

	public void SetHigh5Location()
	{
		high5.transform.position = syncTransform.transform.position;
	}
}
