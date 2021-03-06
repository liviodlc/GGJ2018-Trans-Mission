﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class onClickSound : MonoBehaviour 

{
	public AudioClip sound;



	private Button button { get { return GetComponent<Button>(); } }

	private AudioSource source { get { return GetComponent<AudioSource>(); } }


	void Start () 

	{

		gameObject.AddComponent<AudioSource>();

		source.clip = sound;

		source.playOnAwake = false;



		button.onClick.AddListener(() => PlaySoud());

	}



	// Update is called once per frame

	void PlaySoud ()

	{
		source.PlayOneShot (sound);

	}

}﻿
