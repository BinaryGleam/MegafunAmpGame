using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName = "Game";

    public void ChangeScene()
	{
		SceneManager.LoadScene(sceneName);
	}

	public void ChangeScene(string _sceneName)
	{
		SceneManager.LoadScene(_sceneName);
	}

	private void OnEnable()
	{
		ChangeScene();
	}
}
