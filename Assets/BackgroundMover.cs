using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
	[SerializeField] float speed;
	private List<Transform> backgrounds;
	private float length;

	private int currentIndex;
	private int nextIndex = 1;
	private float initialXPos;

	private bool isStopped;

	private void Awake() {
		backgrounds = new List<Transform>();
		foreach (Transform child in transform) {
			backgrounds.Add(child);
		}

		initialXPos = backgrounds[0].position.x;
		length = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
		Stop();
	}

	void Update()
    {
        if (backgrounds[nextIndex].position.x <= initialXPos) {
			backgrounds[currentIndex].position = new Vector2(
				backgrounds[currentIndex].position.x + length * backgrounds.Count,
				backgrounds[currentIndex].position.y
			);

			currentIndex = (currentIndex + 1) % backgrounds.Count;
			nextIndex = (nextIndex + 1) % backgrounds.Count;
		}
    }

	private void FixedUpdate() {
		if (isStopped) {
			return;
		}

		foreach (Transform bg in backgrounds) {
			bg.Translate(Vector2.left * speed);
		}
	}

	public void StartMovement() {
		isStopped = false;
	}

	public void Stop() {
		isStopped = true;
	}
}
