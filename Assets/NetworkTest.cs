using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkTest : MonoBehaviour {

	public InputField inputField;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		// starts game as server
		if(Input.GetKeyDown(KeyCode.S))
		{
			GameManager.Instance.playerMode = GameManager.PlayerMode.Server;
			//netManager.StartNetworkSession();
			NetManager.Instance.CreateNewRoom();

			var roomName = NetManager.Instance.RoomName;
		}


		//starts game as client
		if(Input.GetKeyDown(KeyCode.C))
		{
			GameManager.Instance.playerMode = GameManager.PlayerMode.Client;
			//StartCoroutine(netManager.JoinLocalBroadcast());
			NetManager.Instance.JoinExistingRoom(inputField.text);
		}
	}
}
