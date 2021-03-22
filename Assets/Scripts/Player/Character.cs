using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [HideInInspector]
    public ScriptableItem currentItem = null;

    [SerializeField]
    private float speed = 1f, jumpForce = 1f, runSpeedMultiplier = 1f;

    private PlayerState currentState = PlayerState.NORMAL;

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

	private void Update()
	{
		switch (currentState)
		{
			case PlayerState.NORMAL:
				break;
			case PlayerState.HIDDEN:
				break;
			case PlayerState.GAMEOVER:
				break;
			default:
				break;
		}
	}
}
