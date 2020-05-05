using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunk : MonoBehaviour
{
	//Config
	[SerializeField] public Transform spawnPoint;

	//Cache
	LevelSpawner levelSpawner;

	private void Awake()
	{
		levelSpawner = FindObjectOfType<LevelSpawner>();
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "SpawnCollider")
		{
			levelSpawner.SpawnChunk();
		}
		else if(other.gameObject.tag == "ShredCollider")
		{
			Destroy(transform.parent.gameObject);
		}
	}
}
