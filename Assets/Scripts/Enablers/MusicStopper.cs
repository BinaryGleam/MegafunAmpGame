using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStopper : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioPlayer = null;

	private void Awake()
	{
		if (audioPlayer == null)
			Debug.LogError("No audio player referenced for the music changer script in " + gameObject.name);
	}

	private void OnEnable()
	{
		audioPlayer.Stop();
		audioPlayer.time = 0f;
	}
}
