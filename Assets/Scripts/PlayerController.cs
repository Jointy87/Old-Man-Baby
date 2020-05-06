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
	[SerializeField] float fallVelocity;

	[Header("Misc")]
	[Tooltip("The collider used to check if the player is touching the ground or not")]
	[SerializeField] BoxCollider2D feetCollider;

	//States
	bool isRunning = false;
	bool isStumbling = false;
	bool isFaceJumping = false;
	bool isFalling = false;

	//Cache
	Rigidbody2D rb;
	Animator animator;
	float faceJumpTimeLeft;
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		isRunning = true;
	}

	void Update()
	{
		Run();
		AttemptFaceJump();
		FaceJump();
		Fall();
	}

	private void Run()
	{
		if (isRunning)
		{
			isStumbling = false;
			isFaceJumping = false;
			isFalling = false;

			animator.SetBool("isRunning", true);

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
			isRunning = false;
			isStumbling = false;
			isFalling = false;

			isFaceJumping = true;
			animator.SetBool("isRunning", false);
			animator.SetBool("isFaceJumping", true);
			faceJumpTimeLeft = faceJumpDuration;
		}
	}
	private void FaceJump()
	{
		if(isFaceJumping)
		{
			if (faceJumpTimeLeft > 0)
			{
				rb.velocity = new Vector2(faceJumpVelocityX, faceJumpVelocityY);
				faceJumpTimeLeft -= Time.deltaTime;
			}
			else if (faceJumpTimeLeft <= 0)
			{
				isFaceJumping = false;
				animator.SetBool("isFaceJumping", false);
				isFalling = true;
			}
		}
		
	}
	private void Fall()
	{
		if(isFalling)
		{
			animator.SetBool("isFalling", true);
			rb.velocity = new Vector2(fallVelocity, rb.velocity.y);

			if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
			{
				isFalling = false;
				isRunning = true;

				animator.SetBool("isFalling", false);
			}
		}
	}
	public void Stumble()
	{
		isRunning = false;
		isFaceJumping = false;
		isFalling = false;

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

	public bool FetchFaceJumpStatus()
	{
		return isFaceJumping;
	}
}

