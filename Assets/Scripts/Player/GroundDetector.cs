using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField]
    private Character characterRef = null;
	//[SerializeField]
	//private float landThreshold = 0.5f;

	private void Awake()
	{
		if (characterRef == null)
			Debug.LogError("Character script reference not set in game");
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			characterRef.charTouchGround = true;
			//if(Mathf.Abs(GetComponentInParent<Rigidbody2D>().velocity.y) > landThreshold)
			//	characterRef.landSource.Play();
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			characterRef.charTouchGround = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			characterRef.charTouchGround = false;
		}
	}
}
