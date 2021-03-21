using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DebugMgr : MonoBehaviour
{
	#if UNITY_EDITOR || DEVELOPMENT_BUILD
	private bool debugIsOn = false;

	[SerializeField]
	private GameObject[] debugObjects = null;
	#endif

	public void ToggleDebug()
	{
		#if UNITY_EDITOR || DEVELOPMENT_BUILD
		debugIsOn = !debugIsOn;
		foreach (GameObject go in debugObjects)
		{
			go.SetActive(debugIsOn);
		}

			Debug.Log("Debug is now: " + (debugIsOn ? "on" : "off"));
		#endif
	}

	#if UNITY_EDITOR || DEVELOPMENT_BUILD
	private void Update()
	{
		if(debugIsOn)
		{
			Debug.DrawLine(Vector3.zero, Vector3.up * 10f, Color.cyan);
		}
	}
	#endif

}

