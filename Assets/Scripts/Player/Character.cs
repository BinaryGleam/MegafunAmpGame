using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [HideInInspector]
    public bool charTouchGround = false;

    //I use this speed multiplier to remember at all time the actual basic speed
    [HideInInspector]
    public float speedMultiplier = 1f;

    [SerializeField]
    private float speed = 1f, jumpForce = 1f, runSpeedMultiplier = 1f;


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
}
