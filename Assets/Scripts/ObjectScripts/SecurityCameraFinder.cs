using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraFinder : Singleton<MonoBehaviour> {


	private Dictionary<int, SecurityCamera> cameraList; 

	// Use this for initialization
	void Start ()
	{
		// GetAllCameras();
		// foreach( var cam in cameraList.Keys)
		// {
		// 	Debug.Log("Camera id : " + cameraList[cam].Id);
		// }

		// Debug.Log("Quantity of cameras:" + QuantityOfCameras());
	}
	
	public void GetAllCameras()
	{
		cameraList = new Dictionary<int, SecurityCamera>();
		var list = GameObject.FindObjectsOfType<SecurityCamera>();

		foreach( var cam in list)
		{
			cameraList.Add(cam.Id, cam);
		}
	}

	public int QuantityOfCameras()
	{
		return cameraList.Count;
	}

	public int GetCameraFeed(int id)
	{
		if(cameraList.ContainsKey(id))
		{
			Debug.LogError("No camera with Id:" + id);
		}

		return cameraList[id].Id; 
	}
}
