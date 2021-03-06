﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour {

	[Header("Camera Info")]
	public int Id;

	[Header("Positions")]
	public Vector3 startRotation;
	public Vector3 endRotation;
	
	[Header("Rotation Times")]
	public float duration = 2;
	public float delay = 1;

	private bool isReverse;
	private Transform rotationBody;

	private Camera myCam;

	void Awake()
	{
		rotationBody = transform.Find("CameraBody");
		if(rotationBody == null)
		{
			Debug.LogError("No object found with the name 'CameraBody'");
			return;
		}

		StartMovement();

		myCam = transform.GetComponentInChildren<Camera>();
	}
	
	public void StartMovement()
	{
		StartCoroutine(MoveCamera());
	}

	private IEnumerator MoveCamera()
    {
        float t = 0.0f;
		var start = (!isReverse)  ? startRotation : endRotation;
		var end =   (isReverse) ? startRotation : endRotation;


        while(t <= 1)
        {
            t += Time.deltaTime/duration;
            rotationBody.localRotation = Quaternion.Euler(Vector3.Lerp(start, end, Mathf.SmoothStep(0,1,t)));
            yield return new WaitForFixedUpdate();
        }

		yield return new WaitForSeconds(delay);
		isReverse = !isReverse;
		StartCoroutine(MoveCamera());
    }

	public void SetCameraTargetTexture(RenderTexture target)
	{
		myCam.targetTexture = target;
	}
}
