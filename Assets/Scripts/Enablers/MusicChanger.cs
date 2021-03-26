using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChanger : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioPlayer = null;
	[SerializeField]
	private AudioClip newClip = null;

	private void Awake()
	{
		if (audioPlayer == null)
			Debug.LogError("No audio player referenced for the music changer script in " + gameObject.name);
		if(newClip == null)
			Debug.LogError("No audio clip referenced for the music changer script in " + gameObject.name);
	}

	private void OnEnable()
	{
		audioPlayer.Stop();
		audioPlayer.time = 0f;
		audioPlayer.clip = newClip;	
	}
}
