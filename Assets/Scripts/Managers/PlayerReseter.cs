using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReseter : Reseter
{
	private Character piggy = null;

	private void Start()
	{
		piggy = GetComponent<Character>();
		if (piggy == null)
			Debug.LogError("Player reseter cant find piggy character");
	}

	public void Checkpoint()
	{
		startPosition = transform.localPosition;
		startRotation = transform.localRotation;
		startScale = transform.localScale;
	}

	public override void CustomReset() 
	{
		base.CustomReset();
		piggy.CharReset();
		GetComponent<Controls>().StopMovements();
	}

}
