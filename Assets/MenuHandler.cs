using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Rendering.Universal;

public class MenuHandler : MonoBehaviour
{
	[SerializeField] ThemeSelector themeSelector1;
	[SerializeField] ThemeSelector themeSelector2;

	[SerializeField] TMP_Text scoreText;
	[SerializeField] TMP_Text bestScoreText;

	[SerializeField] TMP_Text brightnessText;

	Light2D mainLight;

	private void Awake() {
		mainLight = GameObject.FindGameObjectWithTag("MainLight").GetComponent<Light2D>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.F9)) {
			themeSelector1.gameObject.SetActive(!themeSelector1.gameObject.activeSelf);
			themeSelector2.gameObject.SetActive(!themeSelector2.gameObject.activeSelf);
		}

		scoreText.text = GameStateManager.Instance.Score.ToString();
		bestScoreText.text = "Best score: " + GameStateManager.Instance.BestScore.ToString();

		SetBrightness();
	}

	private void SetBrightness() {
		float br = Mathf.Pow(4, Screen.brightness) / 4;
		brightnessText.text = "Brightness: " + Screen.brightness + " / " + br;
		mainLight.intensity = Mathf.Lerp(mainLight.intensity, br, 0.5f);
	}
}
