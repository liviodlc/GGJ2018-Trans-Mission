﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains logic for receiving keypad input
public class LockedDoor : MonoBehaviour {

    //Numeric ID
    public int doorEventId;
    public int id;
    //Combinantion to unlock door
    public string combination;

    //How far the door should move when it opens
    public Vector3 openOffset;
    public float openSpeed;
    //Acceptable distance from end point.  Recommended to be >1;
    public float openThreshold = 10f;

    private Vector3 startPos;
    private float distToEnd { get { return Vector3.Distance(transform.position, openOffset); } }

    public bool isLocked = true;

    public AudioClip audioClip;
    public AudioSource audioSource;

    /*The combination that has been punched in.
     * Once it reaches the length of the combination,
     * it will either unlock the door or reset*/
    private string currentCombo = "";

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        EventManager.StartListening(GameEvent.OpenDoor+doorEventId, OpenDoorEvent);

        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called whenever the player punches in a number on the keypad
    public void numberInput(char symbol)
    {
        //Add number to current combination
        currentCombo += symbol;
        Debug.Log("Door #" + id + ": " + currentCombo);
        //If the current combination has as many numbers as the door's combination...
        if (currentCombo.Length == combination.ToString().Length)
        {
            //If the combination is correct...
            if (currentCombo == combination)
            {
                //Unlock the door
                isLocked = false;
                Debug.Log("Combination Correct!  Door #" + getId() + " unlocked.");
                StartCoroutine(openDoor());
            }
            //If the combination is incorrect...
            else
            {
                Debug.Log("Combination Incorrect.");
            }
            //Regardless, reset the current combination
            currentCombo = "";
        }
    }

    public IEnumerator openDoor()
    {
        float t = 0.0f;

        while(t <= 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, startPos + openOffset, Mathf.SmoothStep(0,1,t));
            yield return new WaitForFixedUpdate();
        }
    }

    void OpenDoorEvent()
	{
        audioSource.PlayOneShot(audioClip);
        Debug.Log("OPEN DOOR " + doorEventId);
		StartCoroutine(openDoor());
		EventManager.StopListening(GameEvent.OpenDoor+doorEventId, OpenDoorEvent);
	}

    public int getId()
    { return id; }
    public void setId(int newId)
    { id = newId; }
}
