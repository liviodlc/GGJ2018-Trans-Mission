using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillTrig : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if ("DogTrig" == other.name)
			EventManager.TriggerEvent(GameEvent.GameOver);
	}
}