using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
	//Config Parameters
	[SerializeField] float lookAheadDistance;

	//Cache
	PlayerController player;
	void Start()
	{
		player = FindObjectOfType<PlayerController>();
	}


	void Update()
	{
		FollowPlayer();
	}

	private void FollowPlayer()
	{
		Vector2 newCamPos = new Vector2(player.transform.position.x + lookAheadDistance, transform.position.y);
		transform.position = new Vector3(newCamPos.x, newCamPos.y, transform.position.z);
	}
}
