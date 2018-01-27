using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour {

	NetManager netManager;
	// Use this for initialization
	void Start () {
		netManager = GameObject.FindObjectOfType<NetManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			netManager.StartNetworkSession();
		}
	}
}
