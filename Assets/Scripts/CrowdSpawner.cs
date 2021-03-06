﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSpawner : MonoBehaviour
{
	//Config parameters
	[SerializeField] float spawnPosX;
	[SerializeField] float spawnPosY;
	[SerializeField] float intervalMin;
	[SerializeField] float intervalMax;
	[SerializeField] GameObject personPrefab;

	//Cache
	bool isGameRunning = true;
	IEnumerator Start()
	{
		do
		{
			yield return StartCoroutine(SpawnCrowd());
		}
		while (isGameRunning);
	}

	private IEnumerator SpawnCrowd()
	{
		Vector2 spawnPosition = new Vector2(transform.position.x + spawnPosX, spawnPosY);
		GameObject spawnedPerson = Instantiate(personPrefab, spawnPosition, Quaternion.identity);

		float spawnInterval = Random.Range(intervalMin, intervalMax);
		yield return new WaitForSeconds(spawnInterval);
	}

}
