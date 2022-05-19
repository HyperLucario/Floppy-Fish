using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
	[SerializeField] private GameObject obstacle;
	[SerializeField] private GameObject obstacleSpecial;
	[SerializeField] private float specialSpawnChance;

	private Transform obstacleSpawnpoint;
	private Transform obstaclePool;
	private float timer = 0;
	[SerializeField] private float objectSpawnDelay;
	[SerializeField] private float offsetY;

	private System.Random random;

	private bool isStopped = false;

	private void Awake() {
		random = new System.Random();
		obstacleSpawnpoint = GameObject.FindGameObjectWithTag("Obstacle Spawnpoint").transform;
		obstaclePool = GameObject.FindGameObjectWithTag("Obstacle Pool").transform;
		isStopped = true;
		timer = objectSpawnDelay;
	}

	void Update() {
		timer += Time.deltaTime;
		if (timer >= objectSpawnDelay && !isStopped) {
			timer = 0;
			SpawnObject();
		}
	}

	private void SpawnObject() {
		bool spawnSpecial = false;
		if (obstacleSpecial != null &&  random.NextDouble() * 100.0d < specialSpawnChance) {
			spawnSpecial = true;
		}

		if (obstacle != null) {
			// Generate a random floating number between (-offsetY, offsetY)
			float randomYOffset = ((float)random.NextDouble()) * offsetY * 2.0f - offsetY;

			// Create the object
			Instantiate(
				spawnSpecial ? obstacleSpecial : obstacle, 
				new Vector2(obstacleSpawnpoint.position.x, obstacleSpawnpoint.position.y + randomYOffset), 
				Quaternion.identity, 
				obstaclePool
			);
		}
	}

	public void StartGeneration() {
		isStopped = false;
	}

	public void StopObstacles() {
		isStopped = true;
		foreach (Transform child in transform.GetChild(1)) {
			child.GetComponent<Obstacle>().Freeze();
		}
	}
}
