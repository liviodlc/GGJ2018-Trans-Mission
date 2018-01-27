using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetLocalDiscovery : NetworkDiscovery 
{
	//public string address;

	private static NetLocalDiscovery instance;
	private NetManager netManager;

	void Start()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		Initialize();
		DontDestroyOnLoad(this);
		netManager = GameObject.FindObjectOfType<NetManager>();
	}

	public void InitAsServer()
	{
		StartAsServer();
	}

	public void InitAsClient()
	{
		StartAsClient();
	}

	public void StopBroadcasting()
	{
		StopBroadcast();
	}

	public override void OnReceivedBroadcast(string fromAddress, string data)
    {
    	Debug.Log("Received Message from: " + fromAddress + " data: " + data);
    	netManager.SetNetworkJoinAddress(fromAddress);
    }
}