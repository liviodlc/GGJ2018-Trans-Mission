using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AgentStartup : MonoBehaviour {

	public TMP_InputField inputField;
	NetManager netManager;
	
	// Use this for initialization
	void Start () {

		netManager = GameObject.FindObjectOfType<NetManager>();
		GameManager.Instance.playerMode = GameManager.PlayerMode.Client;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Return))
		{
			EnterGame();
		}
	}
	public void EnterGame()
	{
		netManager.JoinExistingRoom(inputField.text);
	}
}
