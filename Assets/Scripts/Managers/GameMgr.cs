using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance = null;
    public static GameMgr Instance
	{
		get
		{
            if (instance == null)
                instance = FindObjectOfType<GameMgr>();
            return instance;
		}
	}

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TesAccess()
	{
        Debug.Log("Accessed the GameMgr");
	}
}
