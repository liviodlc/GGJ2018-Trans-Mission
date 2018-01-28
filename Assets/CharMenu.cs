using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharMenu : MonoBehaviour {


	public void into_networking()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}
		

	public void previous_scene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
	}
		

}
