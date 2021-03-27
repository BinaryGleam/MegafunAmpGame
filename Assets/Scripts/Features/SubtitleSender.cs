using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

enum ChronoPhases
{
    START = 0x0,
    STAY,
    EXIT,
    NONE
}

public class SubtitleSender : MonoBehaviour
{
    [SerializeField]
    [Header("Event")]
    private UnityEvent onSubtitleEnd;
    [SerializeField]
    [Header("Time")]
    private float fadeInTime = 1f;
    [SerializeField]
    private float stayTime = 2f;
    [SerializeField]
    private float fadeOutTime = 1f;

    [Header("Text")]
    [SerializeField]
    [TextArea]
    private string msg = "Empty";
    [SerializeField]
    private Color textColor = Color.white;

    [Header("Options")]
    [SerializeField]
    bool destroysWhenDone = false;
    [SerializeField]
    bool isHint = false;

    private ChronoPhases currentPhase = ChronoPhases.NONE;
    private float chrono = 0.0f;

    private Text textBoxRef = null;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        textBoxRef = isHint ? GameMgr.Instance.HintBox : GameMgr.Instance.SubBox;

        if (textBoxRef == null)
            Debug.LogError("Problem when asigning subBoxRef inside " + ToString());
    }

	private void Update()
	{
        if(active)
		{
			switch (currentPhase)
			{
				case ChronoPhases.START:
                    if(chrono <= fadeInTime)
					{
                        Color startColor = textColor;
                        startColor.a = 0f;

                        textBoxRef.color = Color.Lerp(startColor, textColor, chrono);
                        chrono += Time.deltaTime;
					}
                    else if(chrono > fadeInTime)
					{
                        chrono = 0f;
                        currentPhase = ChronoPhases.STAY;
                        textBoxRef.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
                    }
                    break;
				case ChronoPhases.STAY:
                    if (chrono <= stayTime)
                    {
                        chrono += Time.deltaTime;
                    }
                    else if (chrono > stayTime)
                    {
                        chrono = 0f;
                        currentPhase = ChronoPhases.EXIT;
                    }
                    break;
				case ChronoPhases.EXIT:
                    if (chrono <= fadeOutTime)
                    {
                        Color endColor = textColor;
                        endColor.a = 0f;

                        textBoxRef.color = Color.Lerp(textColor, endColor, chrono);
                        chrono += Time.deltaTime;
                    }
                    else if (chrono > fadeOutTime)
                    {
                        if (onSubtitleEnd != null)
                        {
                            Debug.Log("on end");
                            onSubtitleEnd.Invoke();
                        }

                        textBoxRef.color = new Color(textColor.r, textColor.g, textColor.b, 0f);
                        
                        if (destroysWhenDone)
                            Destroy(this);

                        chrono = 0f;
                        currentPhase = ChronoPhases.NONE;
                    }
                    break;
				case ChronoPhases.NONE:
					break;
				default:
					break;
			}
		}
	}

    public void Activate()
	{
        if(currentPhase == ChronoPhases.NONE)
		{
            active = true;
            textBoxRef.color = textColor;
            textBoxRef.text = msg;
            currentPhase = ChronoPhases.START;
		}
	}
}
