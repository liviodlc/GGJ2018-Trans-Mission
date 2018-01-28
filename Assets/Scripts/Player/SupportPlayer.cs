using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SupportPlayer : NetworkBehaviour {

	SecurityCameraFinder securityCameraFinder;
	DoctorUI doctorUI;

	[Header("Sound")]
	public AudioClip staticNoise;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () 
	{
		doctorUI = GetComponentInChildren<DoctorUI>();
		if(GameManager.Instance.playerMode == GameManager.PlayerMode.Server)
		{
			// disable support player camera
			var camera = transform.GetComponent<Camera>();
			camera.enabled = true;

			StartCoroutine(GetSecurityCameraFinder());

			doctorUI.cameraImage.gameObject.SetActive(false);
			Debug.Log("Listening for event HackPanel");
			EventManager.StartListening(GameEvent.HackPanel, PanelWasHacked);

		}
		else
		{
			doctorUI.gameObject.SetActive(false);
		}
		
	}

	public void GetCameraAccess()
	{
		if(securityCameraFinder == null)
		{
			Debug.LogError("No Camera Security Finder Found on Scene");
			return;
		}

		Debug.Log("Accessing cameras");
		doctorUI.noCameraFeedObj.SetActive(false);
		doctorUI.cameraImage.gameObject.SetActive(true);
		//doctorUI.cameraImage.texture = securityCameraFinder.securityCameraRenderTexture;
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

		doctorUI.previousCameraBtn.onClick.AddListener(PlayStaticNoise);
		doctorUI.nextCameraBtn.onClick.AddListener(PlayStaticNoise);		
	}

	private void PlayStaticNoise()
	{
		Debug.Log("Static Noise");
		audioSource.PlayOneShot(staticNoise);
	}
	public void PanelWasHacked()
	{
		EventManager.StopListening(GameEvent.HackPanel, PanelWasHacked);



		Debug.Log("Panel was hacked event");
		GetCameraAccess();
	}
}
