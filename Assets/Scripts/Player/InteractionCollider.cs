using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCollider : MonoBehaviour
{
	public AgentPlayer player;

	private void OnTriggerEnter(Collider other)
	{
		InteractiveObject target = null;
		if (target = other.GetComponent<InteractiveObject>())
			player.SetInteractiveObject(target);
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<InteractiveObject>())
			player.SetInteractiveObject(null);
	}
}
