using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public enum PlayerMode {Server, Client};

    //public bool DebugMode = false;
    [Header("Game Settings")]
	public PlayerMode playerMode;

    public static GameManager Instance;

    void Start()
    {

        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;        
        DontDestroyOnLoad(this);
    }
}
