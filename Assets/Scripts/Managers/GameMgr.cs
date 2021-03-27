using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    [SerializeField]
    private Text subBox = null;
    [SerializeField]
    private Text hintBox = null;
    [SerializeField]
    private Text lifeCount = null;

    public int nbOfLife = 5;

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
        if (hintBox == null)
            Debug.LogError("hintBox not referenced inside the editor for " + ToString());
        if(lifeCount == null)
            Debug.LogError("lifecount not referenced inside the editor for " + ToString());
        lifeCount.text = "X " + nbOfLife;
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void TesAccess()
	{
        Debug.Log("Accessed the GameMgr");
	}

    public void Restart()
	{
        if(nbOfLife > 0)
		{
            nbOfLife -= 1;
            lifeCount.text = "X " + nbOfLife;
            if( nbOfLife == 0)
            {
                GameOver();
                return;
			}

            foreach (Reseter rst in FindObjectsOfType<Reseter>())
            {
                rst.CustomReset();
            }
        }
	}

    private void GameOver()
	{
        SceneManager.LoadScene("GameoverScreen", LoadSceneMode.Single);
	}
}
