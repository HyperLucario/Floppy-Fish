using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	private enum GameState {
		START,
		PAUSED,
		PLAYING,
		ENDED
	}

	public static GameStateManager Instance { get; private set; }

	public int Score { get; private set; }

	public int BestScore { get; private set; }

	private GameState currentState;

	private void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void OnLevelWasLoaded(int level) {
		currentState = GameState.START;

		BestScore = Mathf.Max(BestScore, Score);
		Score = 0;
	}

	private void Update() {
		if (currentState == GameState.START) {
			if (GetJumpKey()) {
				GameObject.FindGameObjectWithTag("Obstacle Pool").transform.parent.GetComponent<ObstacleGenerator>().StartGeneration();
				GameObject.FindGameObjectWithTag("BackgroundMover").GetComponent<BackgroundMover>().StartMovement();
				currentState = GameState.PLAYING;
			}
		}
	}

	public bool GetJumpKey() {
		if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) {
			return true;
		}

		return false;
	}

	public void IncrementScore(int score) {
		Score += score;
		BestScore = Mathf.Max(BestScore, Score);
	}

	public void EndAttempt() {
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Die();
		GameObject.FindGameObjectWithTag("Obstacle Pool").transform.parent.GetComponent<ObstacleGenerator>().StopObstacles();
		GameObject.FindGameObjectWithTag("BackgroundMover").GetComponent<BackgroundMover>().Stop();
		GameObject.FindGameObjectWithTag("MusicManager").GetComponent<AudioSource>().Stop();
	}
}
