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
	enum State { isRunning, isFaceJumping, isFalling, isStumbling };

	//Cache
	Rigidbody2D rb;
	Animator animator;
	float faceJumpTimeLeft;
	State currentState;
	AgeStateController asc;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		asc = GetComponent<AgeStateController>();

		currentState = State.isRunning;
	}

	void Update()
	{
		Run();
		if(asc.FetchAgeState() == AgeStateController.AgeState.baby)
		{
			AttemptFaceJump();
			FaceJump();
			Fall();
		}
	}

	private void Run()
	{
		if (currentState == State.isRunning)
		{
			animator.SetBool("isRunning", true);

			if (rb.velocity.x <= maxVelocity)
			{
				rb.velocity = new Vector2(rb.velocity.x + velocityBuildUp, rb.velocity.y);
				//print(rb.velocity);
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
			currentState = State.isFaceJumping;
			animator.SetBool("isRunning", false);
			animator.SetBool("isFaceJumping", true);
			faceJumpTimeLeft = faceJumpDuration;
		}
	}
	private void FaceJump()
	{
		if(currentState == State.isFaceJumping)
		{
			if (faceJumpTimeLeft > 0)
			{
				rb.velocity = new Vector2(faceJumpVelocityX, faceJumpVelocityY);
				faceJumpTimeLeft -= Time.deltaTime;
			}
			else if (faceJumpTimeLeft <= 0)
			{
				currentState = State.isFalling;
				animator.SetBool("isFaceJumping", false);
			}
		}
		
	}
	private void Fall()
	{
		if(currentState == State.isFalling)
		{
			animator.SetBool("isFalling", true);

			rb.velocity = new Vector2(fallVelocity, rb.velocity.y);

			if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
			{
				currentState = State.isRunning;

				animator.SetBool("isFalling", false);
				animator.SetTrigger("isStumbling");
			}
		}
	}
	public void Stumble()
	{
		currentState = State.isStumbling;

		if (rb.velocity.x > minVelocity)
		{
			rb.velocity = new Vector2(rb.velocity.x - stumbleVelocity, rb.velocity.y);
		}
	}

	public void StumbleRecover()
	{
		currentState = State.isRunning;
	}
}

