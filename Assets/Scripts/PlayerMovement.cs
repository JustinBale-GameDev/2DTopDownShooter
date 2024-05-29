using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[Header("References")]
	[SerializeField]
	private InputActionAsset inputActions;
	[SerializeField]
	private Rigidbody2D rb;
	[SerializeField]
	private Animator anim;

	private InputAction moveAction;
	private Vector2 moveValue;
	public int facingDirection = 1;

	[Header("Changable Values")]
	public float speed = 5;


	private void Awake()
	{
		moveAction = inputActions.FindActionMap("Gameplay").FindAction("Movement");
	}

	// FixedUpdate is called 50x frame
    void FixedUpdate()
    {
		// Capture the input value
        moveValue = moveAction.ReadValue<Vector2>();

		if (moveValue.x > 0 && transform.localScale.x < 0 ||
			moveValue.x < 0 && transform.localScale.x > 0)
		{
			Flip();
		}

		anim.SetFloat("Horizontal", Mathf.Abs(moveValue.x)); // Convert x value into postive
		anim.SetFloat("Vertical", Mathf.Abs(moveValue.y));// Convert y value into postive

		// Move the rigidbody using the moveValue
		//rb.velocity = new Vector2(moveValue.x, moveValue.y) * speed;
		rb.MovePosition(rb.position +  moveValue * speed * Time.fixedDeltaTime);
    }

	void Flip()
	{
		facingDirection *= -1;
		transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
	}

	private void OnEnable()
	{
		moveAction.Enable();
	}
	private void OnDisable()
	{
		moveAction.Disable();
	}
}
