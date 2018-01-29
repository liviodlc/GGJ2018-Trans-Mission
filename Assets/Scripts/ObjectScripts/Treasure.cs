﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : InteractiveObject
{
	public override void Interact()
	{
		throw new System.NotImplementedException();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<AgentPlayer>())
		{
			other.GetComponent<AgentPlayer>().HasTreasure = true;
			gameObject.SetActive(false);
		}
	}
}