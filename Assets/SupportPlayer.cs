using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SupportPlayer : NetworkBehaviour {

	// Use this for initialization
	void Start () 
	{
		if(GameManager.Instance.playerMode == GameManager.PlayerMode.Server)
		{
			Debug.Log("enable camera");
			// disable support player camera
			var camera = transform.GetComponent<Camera>();
			camera.enabled = true;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
