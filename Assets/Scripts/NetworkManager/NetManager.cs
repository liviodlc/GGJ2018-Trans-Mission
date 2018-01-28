using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;

public class NetManager : NetworkManager 
{
	[Header("Player Prefabs")]
	public GameObject ServerPlayer;
	public GameObject ClientPlayer;

	[Header("Match Making Settings")]
	
	public int maxJoinAttempts;
	public uint maxPlayers;
	public uint roomNameLength;

	private int joinAttemptCount;
	public string roomName;
	private bool hasCreatedRoom;
	private MatchInfo newMatchInfo;

	public string RoomName
	{
		get 
		{
			if(!hasCreatedRoom)
				return "";

			return roomName;
		}
	}

	private List<string> previousRoomNameAttempts;

	void Start () {
		previousRoomNameAttempts = new List<string>();
		joinAttemptCount = 0;

		if(maxJoinAttempts <= 0)
		{
			Debug.LogWarning("MaxJoinAttempts in CustomNetworkManager was invalid. Setting it to 5");
			maxJoinAttempts = 5;
		}

		if(maxPlayers <= 1)
		{
			Debug.LogWarning("MaxNumberOfPlayers in CustomNetworkManager was invalid. Setting it to 5");
			maxPlayers = 2;
		}

		if(roomNameLength <= 3)
		{
			Debug.LogWarning("roomNameLength in CustomNetworkManager was invalid. Setting it to 4");
			roomNameLength = 4;
		}

		ClientScene.RegisterPrefab(ServerPlayer);
		ClientScene.RegisterPrefab(ClientPlayer);
	}

	public void CreateNewRoom()
	{
		if(NetworkManager.singleton.matchMaker == null)
		{
			NetworkManager.singleton.StartMatchMaker();
		}		

		joinAttemptCount++;

		if(joinAttemptCount > maxJoinAttempts)
		{
			Debug.LogError("Unable to find an available room name");
			return;
		}
		
		// generate match name
		roomName = GenerateMatchName(roomNameLength);

		// search if match name already exists
		// Callback function takes care of creating match
		// or calling this function again in case a match with the same name already exists
		NetworkManager.singleton.matchMaker.ListMatches(0, 10, roomName, false, 0,0, CreateNewMatchIfNotExists);
	}

	public void JoinHostRoom()
	{
        MatchInfo hostInfo = newMatchInfo;
        NetworkServer.Listen(hostInfo, 9000);
        NetworkManager.singleton.StartHost(hostInfo);
	}

	public void JoinExistingRoom(string existingRoom)
	{
		if(NetworkManager.singleton.matchMaker == null)
		{
			NetworkManager.singleton.StartMatchMaker();
		}	

		roomName = existingRoom;
		Debug.Log("Trying to join match :" + existingRoom);
		// search for match with specific name
		 NetworkManager.singleton.matchMaker.ListMatches(0, 10, existingRoom, true, 0, 0, JoinMatchIfExists);

	}

	#region Create New Match Functions
	private void CreateNewMatchIfNotExists(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {

		if(success)
		{
			if(matches.Count == 0)
			{
				Debug.Log("Creating match");
				NetworkManager.singleton.matchMaker.CreateMatch(roomName, maxPlayers, true, "", "", "", 0, 0, OnInternetMatchCreate);
			}
			else
			{
				Debug.Log("MatchName already in use : " + roomName + ". Trying a new one...");

				string matchList = "";

				foreach(var m in matches)
				{
					matchList += m.name + " | ";
				}
				previousRoomNameAttempts.Add(roomName);
				Debug.Log("AllMatchesFound: " + matchList);
				CreateNewRoom();
			}
		}
		else 
		{
			Debug.LogError("Couldn't connect to match maker");
		}
    }

	private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Create match succeeded");
			hasCreatedRoom = true;
			newMatchInfo = matchInfo;

			JoinHostRoom();
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

	private string GenerateMatchName(uint size)
	{
		StringBuilder value;
		do {
			value = new StringBuilder();
			 // use stringbuilder if unity supports it
			for(int i = 0; i < size; i++)
			{
				value.Append( System.Convert.ToChar(Random.Range(0, 24) + 65));
			}
			
		} while(previousRoomNameAttempts.Contains(value.ToString()));

		return value.ToString();

	}
	#endregion Create New Match Functions

	#region Join Existing Match
	private void JoinMatchIfExists(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (success) 
        {
            if (matches.Count != 0)
            {
                Debug.Log("A list of matches was returned");

                //join the last server (just in case there are two...)
                NetworkManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
            }
            else
            {
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

	private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            Debug.Log("Able to join a match");
            MatchInfo hostInfo = matchInfo;
            NetworkManager.singleton.StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }
	#endregion Join Existing Match

	#region Spawn Network Player
	public override void OnClientConnect(NetworkConnection conn)
	{
		//VRMonitorHUD.SetRoomName(roomName);

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

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		string msg = extraMessageReader.ReadMessage<StringMessage>().value;
		
		if(msg == GameManager.PlayerMode.Server.ToString())
		{
			var player = (GameObject)GameObject.Instantiate(ServerPlayer, Vector3.zero, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
		else if(msg == GameManager.PlayerMode.Client.ToString()) {
			var player = (GameObject)GameObject.Instantiate(ClientPlayer
	, Vector3.zero, Quaternion.identity);
			NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		}
		else 
		{
			Debug.Log("Invalid PlayerMode : " + GameManager.Instance.playerMode);
		}
	}
	#endregion Spawn Network Player
}