using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	public enum PlayerMode {Server, Client};

    //public bool DebugMode = false;
    [Header("Game Settings")]
	public PlayerMode playerMode;
}
