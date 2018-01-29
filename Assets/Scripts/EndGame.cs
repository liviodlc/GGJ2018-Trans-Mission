using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventManager.StartListening(GameEvent.GameOver, GameOver);
		EventManager.StartListening(GameEvent.GameWon, GameWon);
	}
	
	public void GameWon()
	{
		SceneManager.LoadScene("gameWon");
	}

	public void GameOver()
	{
		SceneManager.LoadScene("gameOver");
	}
}
