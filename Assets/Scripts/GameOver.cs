using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public GameObject button;
	void Start()
	{
		if(GameManager.Instance.playerMode == GameManager.PlayerMode.Client)
		{
			button.SetActive(false);
		}
	}
	public void Restart()
	{
		if(NetManager.Instance.isNetworkActive)
		{
			Debug.Log("Disconnect");
			NetManager.Instance.StopHost();
			// NetManager.Instance.client.Disconnect();
		}
		else 
		{
			Debug.Log("LoadScene");
			SceneManager.LoadScene("Menu");
		}
	}
}
