using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    [SerializeField]
    private Text subBox = null;
    [SerializeField]
    private Text hintBox = null;

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

    public Text SubBox
	{
		get { return subBox; }
    }

    public Text HintBox
    {
        get { return hintBox; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (subBox == null)
            Debug.LogError("subBox not referenced inside the editor for " + ToString());
        if (subBox == null)
            Debug.LogError("hintBox not referenced inside the editor for " + ToString());
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TesAccess()
	{
        Debug.Log("Accessed the GameMgr");
	}
}
