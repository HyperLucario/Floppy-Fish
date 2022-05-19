using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThemeSelector : MonoBehaviour
{
	public enum Theme {
		DEEP_SEA,
		CLASSIC,
		UPB,
		ALLAHU
	}

	private TMP_Dropdown dropdown;

	private void Awake() {
		// Add references to objects
		dropdown = transform.GetChild(0).GetComponent<TMP_Dropdown>();
	}

	public void ChangeTheme() {
		Debug.Log("Changed to theme " + (Theme)dropdown.value + ".");
	}
}
