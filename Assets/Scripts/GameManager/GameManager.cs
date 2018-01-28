using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public enum PlayerMode {Server, Client};

    //public bool DebugMode = false;
    [Header("Game Settings")]
	public PlayerMode playerMode;

    public static GameManager Instance
    {
        get 
        {
            if(_instance == null)
            {
                // GameObject singleton = new GameObject();
				// _instance = singleton.AddComponent<GameManager>();
				// singleton.name = "(singleton) "+ typeof(GameManager).ToString();
                Debug.LogError("GameManager is null");
            }

            return _instance;
        }
    }

    private static GameManager _instance;

    void Start()
    {

        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;        
        DontDestroyOnLoad(this);
    }
}
