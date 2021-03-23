using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitleSender : MonoBehaviour
{
    [SerializeField][Header("Time")]

    private Text subBoxRef = null;

    // Start is called before the first frame update
    void Start()
    {
        subBoxRef = GameMgr.Instance.SubBox;

        if (subBoxRef == null)
            Debug.LogError("Problem when asigning subBoxRef inside " + ToString());
    }
}
