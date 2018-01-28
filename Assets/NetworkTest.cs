using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkTest : MonoBehaviour {

	public InputField inputField;
	NetManager netManager;
	// Use this for initialization
	void Start () {
		netManager = GameObject.FindObjectOfType<NetManager>();
	}
	
	// Update is called once per frame
	void Update () {

		// starts game as server
		if(Input.GetKeyDown(KeyCode.S))
		{
			GameManager.Instance.playerMode = GameManager.PlayerMode.Server;
			//netManager.StartNetworkSession();
			netManager.CreateNewRoom();
		}


		//starts game as client
		if(Input.GetKeyDown(KeyCode.C))
		{
			GameManager.Instance.playerMode = GameManager.PlayerMode.Client;
			//StartCoroutine(netManager.JoinLocalBroadcast());
			netManager.JoinExistingRoom(inputField.text);
		}
	}
}
