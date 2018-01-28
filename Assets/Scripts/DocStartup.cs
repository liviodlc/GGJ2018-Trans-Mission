using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class DocStartup : MonoBehaviour {

	public TextMeshProUGUI codeTxt;
	bool hasAccepted;
	
	// Use this for initialization
	void Start () {

		GameManager.Instance.playerMode = GameManager.PlayerMode.Server;
		

		hasAccepted = false;
	}
	

	public void AcceptMissionBtn()
	{
		if(hasAccepted)
		{
			return;
		}
		hasAccepted = true;

		NetManager.Instance.CreateNewRoom();
	}

	public void RejectMissionBtn()
	{
		Debug.Log("show game over screen and then go back to char selection");
		SceneManager.LoadScene("GameOver");
	}
}
