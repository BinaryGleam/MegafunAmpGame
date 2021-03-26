using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReseter : Reseter
{
	private TriggerEvent trigger = null;
	private bool enabledOnGameStart;

	protected override void Awake()
	{
		trigger = GetComponent<TriggerEvent>();
		if (trigger == null)
			Debug.LogError("There was an issue for the reseter and no trigger event were found");

		enabledOnGameStart = trigger.gameObject.activeInHierarchy;
	}

	public override void CustomReset()
	{
		trigger.gameObject.SetActive(enabledOnGameStart);
	}
}
