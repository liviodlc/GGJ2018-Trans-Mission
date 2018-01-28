using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AgentStartup : MonoBehaviour {

	public TMP_InputField inputField;	
	// Use this for initialization
	void Start () 
	{
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
		NetManager.Instance.JoinExistingRoom(inputField.text);
	}
}
