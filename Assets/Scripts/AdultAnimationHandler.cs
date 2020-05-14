using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdultAnimationHandler : MonoBehaviour
{
	[SerializeField] GameObject legs;
	[SerializeField] GameObject torso;
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

	public void SetSyncTransform(float yInput)
	{
		syncTransform.transform.localPosition = new Vector2(0, yInput + syncOffset);
	}

	public void SetHigh5Location()
	{
		high5.transform.position = syncTransform.transform.position;
	}

}
