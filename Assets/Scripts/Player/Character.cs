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

public class Character : MonoBehaviour
{
    [HideInInspector]
    public bool charTouchGround = false;

    //I use this speed multiplier to remember at all time the actual basic speed
    [HideInInspector]
    public float speedMultiplier = 1f;

    [SerializeField]
    private float speed = 1f, jumpForce = 1f, runSpeedMultiplier = 1f;

	public AudioSource jumpSource, landSource, SquickSource, StepSource;

	private TriggerInput triggerInputRef = null;
	private PlayableDirector piggyDirector = null;
	private Animator piggyAnimator = null;
	private Rigidbody2D piggyRb = null;

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

		piggyRb = GetComponent<Rigidbody2D>();
		if(piggyRb == null)
		{
			Debug.LogError("Couldn't find a rigidbody for the character script of little piggy!");
		}
	}

	private void Update()
	{
		if(!charTouchGround)
		{
			PlayJump();
		}
		else
		{
			PlayHorizontalLocomotion();
		}
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

	public void PlayHorizontalLocomotion()
	{
		if (piggyRb.velocity == Vector2.zero)
		{
			piggyAnimator.Play("Idle");
		}
		else if (piggyRb.velocity != Vector2.zero)
		{
			if(speedMultiplier == 1f)
			{
				piggyAnimator.Play("Move");
			}
			else
			{
				piggyAnimator.Play("Run");
			}
		}
	}

	public void PlayJump()
	{
		if (piggyRb.velocity.y >= 0f)
		{
			piggyAnimator.Play("Jump");
		}
		else
		{
			piggyAnimator.Play("Falling");
		}
	}

	public void PlaySquickSound()
	{
		SquickSource.Play();
	}

	public void PlayStepSound()
	{
		StepSource.Play();
	}
}
