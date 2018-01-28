using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelMap : MonoBehaviour
{
	public string sceneName = "Level1";

	private void Awake()
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
	}
}