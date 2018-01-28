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
			player.SelectInteractiveObject(target);
	}

	private void OnTriggerExit(Collider other)
	{
		InteractiveObject target = null;
		if (target = other.GetComponent<InteractiveObject>())
			player.DeselectInteractiveObject(target);
	}
}
