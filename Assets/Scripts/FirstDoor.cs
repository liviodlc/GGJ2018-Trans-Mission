using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstDoor : MonoBehaviour {

	// Use this for initialization
	LockedDoor lockedDoor;
	void Start () {
		lockedDoor = transform.GetComponent<LockedDoor>();
		EventManager.StartListening(GameEvent.OpenFirstDoor, OpenDoor);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OpenDoor()
	{
		StartCoroutine(lockedDoor.openDoor());
		EventManager.StopListening(GameEvent.OpenFirstDoor, OpenDoor);
	}
}
