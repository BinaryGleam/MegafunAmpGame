using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField]
    private Transform lookAtTransform = null;

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

        if (lookAtTransform == null)
            Debug.LogError("The look at transform wasn't set in editor inside the controller component of piggy");
    }

    //All these functions are set through the editor to the Player InputWrapper
    public void FlipSpriteLeft()
	{
        //Make the character sprite shift to the left
        mySpriteRenderer.flipX = true;
        float lookAtX = -Mathf.Abs(lookAtTransform.localPosition.x);
        //lookAtTransform.transform.position.Set(lookAtX, lookAtTransform.transform.position.y, lookAtTransform.transform.position.z);
        lookAtTransform.localPosition = new Vector3(lookAtX, lookAtTransform.transform.localPosition.y, 0f);

        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (GameMgr.Instance.GetComponentInChildren<DebugMgr>().DebugIsOn)
        {
            Debug.Log("Flipped is " + mySpriteRenderer.flipX
                + " and lookAtX is " + lookAtX
                + " and finaly the actual transform x is " + lookAtTransform.transform.localPosition.x);
        }
        #endif
    }

    public void FlipSpriteRight()
	{
        //Make the character sprite shift to its basic position
        mySpriteRenderer.flipX = false;
        float lookAtX = Mathf.Abs(lookAtTransform.localPosition.x);
        //lookAtTransform.transform.position.Set(lookAtX, lookAtTransform.transform.position.y, lookAtTransform.transform.position.z);
        lookAtTransform.localPosition = new Vector3(lookAtX, lookAtTransform.transform.localPosition.y, 0f);

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (GameMgr.Instance.GetComponentInChildren<DebugMgr>().DebugIsOn)
        {
            Debug.Log("Flipped is " + mySpriteRenderer.flipX 
                + " and lookAtX is " + lookAtX 
                + " and finaly the actual transform x is " + lookAtTransform.transform.localPosition.x);
        }
        #endif
    }

    public void HorizontalMovement()
	{
        float horizontal = Input.GetAxisRaw("Horizontal");

        //Get axis direction, multiply with a lot to manage speed, keep the y to not override falling speed
        myRigidbody.velocity = new Vector2(horizontal * characterRef.Speed * characterRef.speedMultiplier, myRigidbody.velocity.y);
    }

    public void StartRun()
	{
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (GameMgr.Instance.GetComponentInChildren<DebugMgr>().DebugIsOn)
		{
            Debug.Log("Start run");
		}
        #endif

        characterRef.speedMultiplier = characterRef.RunSpeedMultiplier;
	}

    public void StopRun()
	{
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (GameMgr.Instance.GetComponentInChildren<DebugMgr>().DebugIsOn)
        {
            Debug.Log("Stop run");
        }
        #endif

        characterRef.speedMultiplier = 1f;
    }

    public void Jump()
	{
        if(characterRef.charTouchGround)
		{
            myRigidbody.AddForce(transform.up * characterRef.JumpForce, ForceMode2D.Impulse);
		}
	}
}
