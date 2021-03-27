using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButcherReseter : Reseter
{
	private ButcherAI butcherRef = null;

	private void Start()
	{
		butcherRef = GetComponent<ButcherAI>();
		if (butcherRef == null)
			Debug.LogError("No butcher AI found by the butcher reseter");
	}

	public override void CustomReset()
	{
		if(gameObject.activeSelf)
		{
			base.CustomReset();
			butcherRef.enabled = true;
			butcherRef.Restart();
		}
	}
}
