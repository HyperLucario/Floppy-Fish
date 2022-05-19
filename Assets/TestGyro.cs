using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGyro : MonoBehaviour
{
	List<Vector3> positions = new List<Vector3>();

	private void OnDrawGizmos() {
		Vector3 gyroAcc = Input.gyro.userAcceleration;

		Gizmos.color = Color.white;
		// Gizmos.DrawLine(transform.position, transform.position + gyroAcc * 20.0f);

		positions.Add(gyroAcc);
		if (positions.Count >= 10) {
			Gizmos.color = Color.white;

			Vector3 res = Vector3.zero;
			foreach (Vector3 dir in positions) {
				res += dir;
			}

			res /= 5;
			Gizmos.DrawLine(transform.position, transform.position + res * 20.0f);

			positions.Clear();
		}
	}
}
