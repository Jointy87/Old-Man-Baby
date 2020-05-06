using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//Config parameters
	[Header("Running")]
	[Tooltip("Velocity added until max velocity reached")]
	[SerializeField] float velocityBuildUp;
	[Tooltip("The max velocity of the player")]
	[SerializeField] float maxVelocity;
	
	[Header("Stumbling")]
	[Tooltip("Counter velocity added upon stumbling")]
	[SerializeField] float stumbleVelocity;
	[Tooltip("Below this velocity stumbleStoppingForce will not be added. To prevent player rolling backwards")]
	[SerializeField] float minVelocity;
	
	[Header("Face Jump")]
	[Tooltip("Forward velocity upon jumping")]
	[SerializeField] float faceJumpVelocityX;
	[Tooltip("Upwards velocity upon jumping")]
	[SerializeField] float faceJumpVelocityY;
	[Tooltip("The duration the facejump lasts")]
	[SerializeField] float faceJumpDuration;

	[Header("Misc")]
	[Tooltip("The collider used to check if the player is touching the ground or not")]
	[SerializeField] BoxCollider2D feetCollider;
	 
	//States
	bool isStumbling = false;
	bool isFaceJumping = false;

	//Cache
	Rigidbody2D rb;
	float faceJumpTimeLeft;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		Run();
		AttemptFaceJump();
		FaceJump();	
	}

	private void Run()
	{
		if (!isStumbling || !isFaceJumping)
		{
			if (rb.velocity.x <= maxVelocity)
			{
				rb.velocity = new Vector2(rb.velocity.x + velocityBuildUp, rb.velocity.y);
				print(rb.velocity);
			}
			else if (rb.velocity.x > maxVelocity)
			{
				rb.velocity = new Vector2(maxVelocity, rb.velocity.y);
			}
		}
	}
	private void AttemptFaceJump()
	{
		if (Input.GetButtonDown("Jump") && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
		{
			isFaceJumping = true;
			faceJumpTimeLeft = faceJumpDuration;
		}
	}
	private void FaceJump()
	{
		if(faceJumpTimeLeft > 0)
		{
			Vector2 JumpVelocity = new Vector2(faceJumpVelocityX, faceJumpVelocityY);
			rb.velocity = JumpVelocity;
			faceJumpTimeLeft -= Time.deltaTime;
		}
		else if(faceJumpTimeLeft <= 0)
		{
			isFaceJumping = false;
		}
	}
	public void Stumble()
	{
		isStumbling = true;
		if (rb.velocity.x > minVelocity)
		{
			rb.velocity = new Vector2(rb.velocity.x - stumbleVelocity, rb.velocity.y);
		}
	}

	public void StumbleRecover()
	{
		isStumbling = false;
	}
}

