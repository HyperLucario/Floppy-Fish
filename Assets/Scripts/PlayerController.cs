using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private float force;

	[SerializeField]
	private float maxSpeed;

	private Rigidbody2D rb;
	private AudioSource deathSound;

	private float previousGravityScale;

	private bool isDead = false;

	[Header("Gyroscope")]
	private GyroManager gyroManager;
	private bool hasJumped = false;
	[SerializeField] float jumpActivationThreshold = 0.7f;
	[SerializeField] float jumpDeactivationThreshold = 0.4f;

	private enum PlayerState {
		IDLE,
		MOVING,
		DEAD
	}

	private PlayerState currentState;

	void Awake()
	{
		gyroManager = GameObject.FindGameObjectWithTag("GyroManager").GetComponent<GyroManager>();

		rb = GetComponent<Rigidbody2D>();
		deathSound = GetComponent<AudioSource>();

		previousGravityScale = rb.gravityScale;

		currentState = PlayerState.IDLE;
		rb.gravityScale = 0.0f;
	}

	void Update() {
		switch (currentState) {
			case PlayerState.IDLE:
				Idle();
				break;

			case PlayerState.MOVING:
				Moving();
				break;

			case PlayerState.DEAD:
				Dead();
				break;
		}
	}

	private void Idle() {
		if (GameStateManager.Instance.GetJumpKey()) {
			rb.gravityScale = previousGravityScale;
			Jump();
			currentState = PlayerState.MOVING;
		}
	}

	private void Moving() {
		// Get the acceleration
		Vector3 gyroAcc = gyroManager.Gyro.userAcceleration;
		Vector3 inverseAcc = -gyroAcc;
		if (inverseAcc.z > jumpActivationThreshold && !hasJumped) {
			hasJumped = true;
			Jump();
		} else if (inverseAcc.z < jumpDeactivationThreshold && hasJumped) {
			hasJumped = false;
		}

		//if (GameStateManager.Instance.GetJumpKey()) {
		//	Jump();
		//}

		transform.GetChild(0).right = new Vector2(30.0f, rb.velocity.y);
	}

	private void Dead() {
		if (Input.GetKeyDown(KeyCode.F5) || GameStateManager.Instance.GetJumpKey()) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void Jump() {
		// Cancel momentum
		rb.velocity = Vector3.zero;
		rb.angularVelocity = 0.0f;

		// Make the player jump
		rb.AddForce(Vector2.up * force);
    }

	public void Die() {
		if (isDead) {
			return;
		}

		currentState = PlayerState.DEAD;

		isDead = true;

		deathSound.Play();

		maxSpeed *= 2;
		rb.velocity = new Vector2(-5f, 7.5f);
		// rb.AddForce(new Vector2(-100.0f, 150.0f));
		rb.angularVelocity = 250.0f;
	}

    private void LateUpdate()
    {
        if (Mathf.Abs(rb.velocity.y) > maxSpeed) {
			rb.velocity = new Vector2(
				rb.velocity.x, 
				rb.velocity.y < 0.0f ? -maxSpeed : maxSpeed
			);
        }
    }
}
