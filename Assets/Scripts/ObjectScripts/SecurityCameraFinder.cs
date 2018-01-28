using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraFinder : Singleton<MonoBehaviour> {

	public RenderTexture securityCameraRenderTexture;
	private SecurityCamera[] cameraArray;
	
	private int cameraArraySize;

	private int currentCameraFeed = 0;

	// Use this for initialization
	void Start ()
	{
		GetAllCameras();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			NextCamera();
		}

		if(Input.GetKeyDown(KeyCode.S))
		{
			PreviousCamera();
		}
	}
	
	public void GetAllCameras()
	{
		// gets list of all cameras
		var list = GameObject.FindObjectsOfType<SecurityCamera>();

		// creates dictinary of cameras based on id
		// so we can organize it
		var dic = new Dictionary<int, SecurityCamera>();

		cameraArraySize = list.Length;

		// ordered camera id list 
		int[] cameraIds = new int[cameraArraySize];

		int k = 0;
		// build dictonary of cameras with ids as keys
		foreach(var cam in list)
		{
			dic.Add(cam.Id, cam);
			cameraIds[k++] = cam.Id;
		}

		// sort cameras by ids
		Array.Sort(cameraIds);



		// init array 
		cameraArray = new SecurityCamera[cameraArraySize];
		for(int i = 0; i < cameraArraySize; i++)
		{
			cameraArray[i] = dic[cameraIds[i]];
		}

		currentCameraFeed = 0;
		
		cameraArray[0].SetCameraTargetTexture(securityCameraRenderTexture);
	}

	public int QuantityOfCameras()
	{
		return cameraArraySize;
	}

	public RenderTexture GetCameraFeedRenderTexture()
	{
		return securityCameraRenderTexture;
	}

	public void NextCamera()
	{
		cameraArray[currentCameraFeed].SetCameraTargetTexture(null);

		currentCameraFeed++;
		if(currentCameraFeed >= cameraArraySize)
		{
			currentCameraFeed = 0;
		}

		cameraArray[currentCameraFeed].SetCameraTargetTexture(securityCameraRenderTexture);
	}

	public void PreviousCamera()
	{
		cameraArray[currentCameraFeed].SetCameraTargetTexture(null);
		
		currentCameraFeed--;
		if(currentCameraFeed < 0)
		{
			currentCameraFeed = cameraArraySize - 1;
		}

		cameraArray[currentCameraFeed].SetCameraTargetTexture(securityCameraRenderTexture);
	}

}
