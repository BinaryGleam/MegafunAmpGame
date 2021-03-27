using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TriggerEvent : MonoBehaviour
{
	[SerializeField]
	[Header("Trigger varialbes")]
	protected UnityEvent OnEnter;
	[SerializeField]
	protected UnityEvent OnExit;
	[SerializeField]
	protected string activatorTag = "Player";

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == activatorTag)
		{
			OnEnter.SafeCall();
		}
	}

	protected void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == activatorTag)
		{
			OnExit.SafeCall();
		}
	}
}
