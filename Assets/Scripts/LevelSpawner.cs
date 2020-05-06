using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
	//Config Parameters
	[SerializeField] GameObject[] levelChunks;
	[SerializeField] Transform chunkParent;

	//Cache
	GameObject chunkToSpawn;
	GameObject previousChunk;
	GameObject currentChunk;

	void Start()
	{
		SpawnFirstChunk();
	}

	private void SpawnFirstChunk()
	{
		GameObject firstChunk = levelChunks[Random.Range(0, levelChunks.Length)];
		currentChunk = Instantiate(firstChunk, transform);
		currentChunk.transform.parent = chunkParent;
		previousChunk = currentChunk;
	}

	public void SpawnChunk()
	{
		chunkToSpawn = levelChunks[Random.Range(0, levelChunks.Length)];
		currentChunk = Instantiate(chunkToSpawn, previousChunk.GetComponentInChildren<LevelChunk>().spawnPoint.transform);
		currentChunk.transform.parent = chunkParent;
		previousChunk = currentChunk;
	}
}
