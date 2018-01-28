using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SupportPlayer : NetworkBehaviour {

	SecurityCameraFinder securityCameraFinder;
	DoctorUI doctorUI;
	// Use this for initialization
	void Start () 
	{
		if(GameManager.Instance.playerMode == GameManager.PlayerMode.Server)
		{
			Debug.Log("enable camera");
			// disable support player camera
			var camera = transform.GetComponent<Camera>();
			camera.enabled = true;

			StartCoroutine(GetSecurityCameraFinder());

			doctorUI = GetComponentInChildren<DoctorUI>();
		}
		
	}

	public void GetCameraAccess()
	{
		if(securityCameraFinder == null)
		{
			Debug.LogError("No Camera Security Finder Found on Scene");
			return;
		}

		doctorUI.noCameraFeedObj.SetActive(false);
		doctorUI.cameraImage.texture = securityCameraFinder.securityCameraRenderTexture;
	}
	
	IEnumerator GetSecurityCameraFinder()
	{
		while(securityCameraFinder == null)
		{
			securityCameraFinder = GameObject.FindObjectOfType<SecurityCameraFinder>();
			yield return new WaitForSeconds(1);
		}

		doctorUI.previousCameraBtn.onClick.AddListener(securityCameraFinder.PreviousCamera);
		doctorUI.nextCameraBtn.onClick.AddListener(securityCameraFinder.NextCamera);		
	}
}
