using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float timeToDestroy;
	[SerializeField] private int score;

	private bool isFrozen;

	private float timeAlive = 0.0f;

	private Transform player;

	private float playerXPos;
	private bool hasBeenPassed;

	private void Awake() {
		player = GameObject.FindWithTag("Player").transform;
		playerXPos = player.position.x;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("Player")) {
			GameStateManager.Instance.EndAttempt();
		}
	}

	private void Update() {
		if (isFrozen) {
			return;
		}

		if (transform.position.x < playerXPos && !hasBeenPassed) {
			hasBeenPassed = true;
			GameStateManager.Instance.IncrementScore(score);
		}

		timeAlive += Time.deltaTime;
		if (timeAlive > timeToDestroy) {
			Destroy(gameObject);
		}
	}

	private void FixedUpdate() {
		if (isFrozen) {
			return;
		}

		transform.Translate(Vector2.left * speed);
	}
	public void Freeze() {
		isFrozen = true;
	}
}
