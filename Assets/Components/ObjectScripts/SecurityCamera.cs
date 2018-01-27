using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour {

    //Once the camera hits this angle, it will reverse its rotation
    public float maxAngle;
    //Speed at which camera rotates
    public float turnSpeed;
    
    //Master on/off switch
    public bool isActive = true;

    //Determines if camera is rotatin positively
    private bool isPos = true;
    //Simplifies angle and returns negative number if angle is greater than 180
    private float angle { get { return (transform.localRotation.eulerAngles.y < 180) ? 
                transform.localRotation.eulerAngles.y : 
                transform.localRotation.eulerAngles.y - 360; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!isActive)
            return;

        if (isPos)
        {
            if (angle < maxAngle)
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            else
                isPos = false;
        }
        else
        {
            if (angle > -maxAngle )
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            else
                isPos = true;
        }
	}
}
