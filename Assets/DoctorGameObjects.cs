using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorGameObjects : MonoBehaviour {

	public Text missionCode;

	NetManager netManager;
	// Use this for initialization
	void Start () {

		// if this instance is the doctor's (aka the server)
		if(GameManager.Instance.playerMode == GameManager.PlayerMode.Server)
		{
			//turn off the spy game objects - aka the client
			netManager = GameObject.FindObjectOfType<NetManager>();

			if(netManager == null)
			{
				Debug.LogError("netManager is null");
			}
			StartCoroutine(SetMissionText());
			
		}
	}

	IEnumerator SetMissionText()
	{
		while(string.IsNullOrEmpty(netManager.RoomName))
		{
			yield return new WaitForSeconds(1);
		}

		missionCode.text = "Mission Id: " + netManager.RoomName;
	}
}
