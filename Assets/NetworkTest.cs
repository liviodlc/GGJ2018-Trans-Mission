using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkTest : MonoBehaviour {

	NetManager netManager;
	// Use this for initialization
	void Start () {
		netManager = GameObject.FindObjectOfType<NetManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.S))
		{
			GameManager.Instance.playerMode = GameManager.PlayerMode.Server;
			netManager.StartNetworkSession();
		}

		if(Input.GetKeyDown(KeyCode.C))
		{
			GameManager.Instance.playerMode = GameManager.PlayerMode.Client;
			StartCoroutine(netManager.JoinLocalBroadcast());
		}
	}
}
