using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

enum PlayerState
{
    NORMAL = 0x0,
    HIDDEN,
    GAMEOVER
}

[System.Serializable]
struct DeathAnimations
{
	public string killerTag;
	public PlayableAsset killerAnim;
}

public class Character : MonoBehaviour
{
    [HideInInspector]
    public bool charTouchGround = false;

    //I use this speed multiplier to remember at all time the actual basic speed
    [HideInInspector]
    public float speedMultiplier = 1f;

    //[HideInInspector]
    //public ScriptableItem currentItem = null;

    [SerializeField]
    private float speed = 1f, jumpForce = 1f, runSpeedMultiplier = 1f;

	[SerializeField]
	private DeathAnimations[] piggyDeaths;

	private TriggerInput triggerInputRef = null;
	private PlayableDirector piggyDirector = null;
	private Animator piggyAnimator = null;

	//private PlayerState currentState = PlayerState.NORMAL;

    public float Speed
	{
        get { return speed; }
	}

    public float JumpForce
	{
        get { return jumpForce; }
	}

    public float RunSpeedMultiplier
	{
        get { return runSpeedMultiplier; }
	}

	public TriggerInput TriggerInputRef
	{
		get { return triggerInputRef; }
	}

	private void Awake()
	{
		piggyAnimator = GetComponent<Animator>();
		if (piggyAnimator == null)
		{
			Debug.LogError("Couldn't find the animator for piggy animations");
		}

		piggyDirector = GetComponent<PlayableDirector>();
		if(piggyDirector == null)
		{
			Debug.LogError("Couldn't find the piggy director to play timelines");
		}
	}

	private void Update()
	{
		//switch (currentState)
		//{
		//	case PlayerState.NORMAL:
		//		break;
		//	case PlayerState.HIDDEN:
		//		break;
		//	case PlayerState.GAMEOVER:
		//		break;
		//	default:
		//		break;
		//}
	}	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.GetComponent<TriggerInput>() != null)
		{
			#if UNITY_EDITOR || DEVELOPMENT_BUILD
			if (GameMgr.Instance.GetComponentInChildren<DebugMgr>().DebugIsOn)
			{
				Debug.Log("Found Trigger Input");
			}
			#endif
			triggerInputRef = collision.GetComponent<TriggerInput>();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<TriggerInput>() != null && triggerInputRef != null)
		{
			#if UNITY_EDITOR || DEVELOPMENT_BUILD
			if (GameMgr.Instance.GetComponentInChildren<DebugMgr>().DebugIsOn)
			{
				Debug.Log("Got rid of player input");
			}
			#endif

			triggerInputRef = null;
		}
	}

	public void CharReset()
	{
		#if UNITY_EDITOR || DEVELOPMENT_BUILD
		Debug.Log("Character should reset");
		#endif
	}
}
