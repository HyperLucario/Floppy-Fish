using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestGyro : MonoBehaviour
{
	List<Vector3> positions = new List<Vector3>();

	[SerializeField] float thickness = 50.0f;
	[SerializeField] float waitTime = 0.01f;
	Texture2D texture;

	float currentWaitTime = 0.0f;

	Vector3 p1, p2;

	private void Awake() {
		texture = new Texture2D(128, 128);
	}

	private void OnDrawGizmos() {
		Vector3 gyroAcc = Input.gyro.userAcceleration;

		Gizmos.color = Color.white;
		// Gizmos.DrawLine(transform.position, transform.position + gyroAcc * 20.0f);

		if (currentWaitTime >= waitTime) {
			positions.Add(gyroAcc);
			if (positions.Count >= 10) {


				Gizmos.color = Color.white;

				Vector3 res = Vector3.zero;
				foreach (Vector3 dir in positions) {
					res += dir;
				}

				res /= 5;

				currentWaitTime = 0.0f;

				p1 = transform.position;
				p2 = transform.position + res * 20.0f;

				// Gizmos.DrawLine(transform.position, transform.position + res * 20.0f);

				positions.Clear();
			}
		} else {
			currentWaitTime += Time.deltaTime;
		}

		Handles.DrawBezier(p1, p2, p1, p2, Color.white, texture, thickness);
	}
}
