using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorGameObjects : MonoBehaviour {

	public Text missionCode;

	// Use this for initialization
	void Start () {

		// if this instance is the doctor's (aka the server)
		if(GameManager.Instance.playerMode == GameManager.PlayerMode.Server)
		{
			StartCoroutine(SetMissionText());
			
		}
	}

	IEnumerator SetMissionText()
	{
		while(string.IsNullOrEmpty(NetManager.Instance.RoomName))
		{
			yield return new WaitForSeconds(1);
		}

		missionCode.text = "Mission Id: " + NetManager.Instance.RoomName;
	}
}
