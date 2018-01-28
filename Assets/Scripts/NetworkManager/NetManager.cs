using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

public class NetManager : NetworkManager 
{
	public GameObject AgentPlayer;
	public GameObject SupportPlayer;

    private NetLocalDiscovery localDiscovery;

	private bool foundLocalGame;
	private static NetManager instance;

	void Start()
	{
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad(this);

		foundLocalGame = false;

		ClientScene.RegisterPrefab(AgentPlayer);
		ClientScene.RegisterPrefab(SupportPlayer);

		StartCoroutine(RetrieveNetLocalDiscovery());
	}

	public void StartNetworkSession()
	{
		Debug.Log("Starting Network session...");
		StartCoroutine(StartNetworkSessionRoutine());
	}

	private IEnumerator StartNetworkSessionRoutine()
	{
		while(localDiscovery == null)
		{
			Debug.Log("StartNetworkSession - Waiting for local net discovery");
			yield return new WaitForSeconds(1f);
		}

		Debug.Log("Init as as Server");
		localDiscovery.InitAsServer();
		NetworkManager.singleton.StartHost();
	}

	public void JoinNetworkSession()
	{
		Debug.Log("Joining game at... [" + NetworkManager.singleton.networkAddress+"]");
		NetworkManager.singleton.StartClient();	
	}
	
	private IEnumerator RetrieveNetLocalDiscovery()
	{
		while(localDiscovery == null)
		{
			localDiscovery = GameObject.FindObjectOfType<NetLocalDiscovery>();
			yield return new WaitForSeconds(1f);
		}
	}

	public IEnumerator JoinLocalBroadcast()
	{
		while(localDiscovery == null)
		{
			Debug.Log("JoinLocalBroadcast - Waiting for local net discovery");
			yield return new WaitForSeconds(1f);
		}

		localDiscovery.InitAsClient();

		while(!foundLocalGame)
		{
			Debug.Log("JoinLocalBroadcast - listening for broadcast...");
			yield return new WaitForSeconds(1f);
		}

		JoinNetworkSession();
	}

	public void SetNetworkJoinAddress(string address)
	{
		networkAddress = address;
		foundLocalGame = true;
	}

	#region Spawn Network Player
	public override void OnClientConnect(NetworkConnection conn)
	{
		if (!clientLoadedScene)
        {
			StringMessage msg = new StringMessage(GameManager.Instance.playerMode.ToString());
        	ClientScene.AddPlayer(conn, 0, msg);
		}
    }

	public override void OnClientSceneChanged(NetworkConnection conn)
    {
        bool addPlayer = (ClientScene.localPlayers.Count == 0);
        bool foundPlayer = false;
        foreach (var playerController in ClientScene.localPlayers)
        {
            if (playerController.gameObject != null)
            {
                foundPlayer = true;
                break;
            }
        }
        if (!foundPlayer)
        {
            // there are players, but their game objects have all been deleted
            addPlayer = true;
        }
        if (addPlayer)
        {
			StringMessage msg = new StringMessage(GameManager.Instance.playerMode.ToString());
            ClientScene.AddPlayer(conn, 0, msg);
        }
    }

	void OnConnectedToServer() 
	{
        Debug.Log("Connected to server");
    }

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		string msg = extraMessageReader.ReadMessage<StringMessage>().value;

		var playerPrefab = GetNetworkPlayer(msg);
		var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	public GameObject GetNetworkPlayer(string playerModeStr)
	{
		return (playerModeStr == GameManager.PlayerMode.Server.ToString()) ? AgentPlayer : SupportPlayer;
	}
	#endregion Spawn Network Player (VR or Mobile)
}