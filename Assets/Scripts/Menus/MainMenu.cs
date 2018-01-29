using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void char_select()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
	}

	public void credits()
	{
		SceneManager.LoadScene (7);	
	}

	public void start_screen()
	{
		SceneManager.LoadScene (0);
	}
		

	public void quit_game()
	{
		Application.Quit ();
	}

}
