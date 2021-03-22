using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool charTouchGround = false;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float jumpForce = 1f;

    public float Speed
	{
        get { return speed; }
	}

    public float JumpForce
	{
        get { return jumpForce; }
	}
}
