using UnityEngine;

[CreateAssetMenu(fileName = "New Theme", menuName = "FloppyFish/Theme", order = 1)]
public class ThemeAssets : ScriptableObject
{
	[Header("Player")]
	public Sprite player;
	public AudioClip deathSound;

	[Header("Background")]
	public Sprite background;
	public bool isSliding;
	public AudioClip backgroundMusic;
}
