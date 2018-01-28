using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DocStartup : MonoBehaviour {

	public TextMeshProUGUI codeTxt;
	NetManager netManager;
	
	// Use this for initialization
	void Start () {

		netManager = GameObject.FindObjectOfType<NetManager>();
		GameManager.Instance.playerMode = GameManager.PlayerMode.Server;
		netManager.CreateNewRoom();

		StartCoroutine(GetRoomName());
	}
	

	IEnumerator GetRoomName()
	{
		while(string.IsNullOrEmpty(netManager.RoomName))
		{
			yield return new WaitForSeconds(1);
		}

		codeTxt.SetText(netManager.RoomName);
	}
}
