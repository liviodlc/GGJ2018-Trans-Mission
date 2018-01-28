using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoctorUI : MonoBehaviour {


	public Text missionLabel;
	[Header("Left Panel")]
	public Button terminalBtn;
	public Button missionBtn;
	public Button notesBtn;
	public GameObject terminalPanel;
	public GameObject missionPanel;
	public GameObject notesPanel;
	
	[Header("Camera Panel")]
	public GameObject noCameraFeedObj;
	public Button nextCameraBtn;
	public Button previousCameraBtn;
	public RawImage cameraImage;

	// Use this for initialization
	void Start () {
		if(GameManager.Instance.playerMode == GameManager.PlayerMode.Server)
		{
			//turn off the spy game objects - aka the client

			StartCoroutine(SetMissionText());

			terminalBtn.onClick.AddListener(PressTerminalBtn);
			missionBtn.onClick.AddListener(PressMissionBtn);
			notesBtn.onClick.AddListener(PressNotesBtn);

			missionBtn.Select();
		}
	}

	public void PressTerminalBtn()
	{
		terminalPanel.SetActive(true);
		missionPanel.SetActive(false);
		notesPanel.SetActive(false);
	}

	public void PressMissionBtn()
	{
		terminalPanel.SetActive(false);
		missionPanel.SetActive(true);
		notesPanel.SetActive(false);
	}

	public void PressNotesBtn()
	{
		terminalPanel.SetActive(false);
		missionPanel.SetActive(false);
		notesPanel.SetActive(true);
	}

	IEnumerator SetMissionText()
	{
		while(string.IsNullOrEmpty(NetManager.Instance.RoomName))
		{
			yield return new WaitForSeconds(1);
		}

		missionLabel.text = "MISSION CODE : " + NetManager.Instance.RoomName;
	}
}
