using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerInput : TriggerEvent
{
	[SerializeField][Header("Input varialbes")]
	public UnityEvent onKeyPressInTrigger;
	[SerializeField]
	public UnityEvent onKeyReleaseInTrigger;


	//[SerializeField]
	//protected string keyName = "Interact";

	//private void OnTriggerStay2D(Collider2D collision)
	//{
	//	if(collision.tag == activatorTag )
	//	{
	//		if(Input.GetButtonDown(keyName))
	//		{
	//			onKeyPressInTrigger.SafeCall();
	//		}
	//		else if(Input.GetButtonUp(keyName))
	//		{
	//			onKeyReleaseInTrigger.SafeCall();
	//		}
	//	}
	//}
}
