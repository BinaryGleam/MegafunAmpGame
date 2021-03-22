using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private Rigidbody2D myRigidbody = null;
    private SpriteRenderer mySpriteRenderer = null;
    private Character characterRef = null;

    
    // Start is called before the first frame update
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        if (myRigidbody == null)
            Debug.LogError("Piggy doesn't have a Rigidbody2D");

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        if(mySpriteRenderer == null)
            Debug.LogError("Piggy doesn't have a SpriteRenderer, bad piggy!");

        characterRef = GetComponent<Character>();
        if(characterRef == null)
            Debug.LogError("Piggy doesn't have a character? But piggy is a character!");
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public void FlipSpriteLeft()
	{
        mySpriteRenderer.flipX = true;
	}

    public void FlipSpriteRight()
	{
        mySpriteRenderer.flipX = false;
    }

    public void HorizontalMovement()
	{
        float horizontal = Input.GetAxisRaw("Horizontal");

        myRigidbody.velocity = new Vector2(horizontal * characterRef.Speed, myRigidbody.velocity.y);
    }

    public void Jump()
	{
        if(characterRef.charTouchGround)
		{
            myRigidbody.AddForce(transform.up * characterRef.JumpForce, ForceMode2D.Impulse);
		}
	}
}
