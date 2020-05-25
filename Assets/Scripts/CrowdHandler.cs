using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdHandler : MonoBehaviour
{
	//Config parameters
	[SerializeField] float moveSpeed;
	[SerializeField] BoxCollider2D faceCollider;
	[SerializeField] BoxCollider2D groinCollider;
	[SerializeField] BoxCollider2D highFiveCollider;

	//Cache
	Rigidbody2D rb;
	Animator animator;
	AgeStateController asc;


	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		asc = FindObjectOfType<AgeStateController>();
	}


	void Update()
	{
		Walk();

		EnableCorrectColliders();
	}

	private void Walk()
	{
		rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
	}
	private void EnableCorrectColliders()
	{
		if (asc.FetchAgeState() == AgeStateController.AgeState.baby)
		{
			faceCollider.enabled = true; groinCollider.enabled = false; highFiveCollider.enabled = false;
		}
		else if (asc.FetchAgeState() == AgeStateController.AgeState.youngster)
		{
			faceCollider.enabled = false; groinCollider.enabled = true; highFiveCollider.enabled = false;
		}
		else if (asc.FetchAgeState() == AgeStateController.AgeState.adult)
		{
			faceCollider.enabled = false; groinCollider.enabled = false; highFiveCollider.enabled = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "ShredCollider")
		{
			Destroy(gameObject);
		}
		else if(other.tag == "AdultPlayer")
		{
			animator.SetTrigger("isHighFiving");
		}
	}
}
