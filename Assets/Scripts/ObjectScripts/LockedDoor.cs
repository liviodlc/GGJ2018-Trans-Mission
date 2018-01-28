using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Contains logic for receiving keypad input
public class LockedDoor : MonoBehaviour {

    //Numeric ID
    public int id;
    //Combinantion to unlock door
    public int combination;

    //How far the door should move when it opens
    public Vector3 openOffset;
    public float openSpeed;
    //Acceptable distance from end point.  Recommended to be >1;
    public float openThreshold = 10f;

    private Vector3 startPos;
    private float distToEnd { get { return Vector3.Distance(transform.position, openOffset); } }

    public bool isLocked = true;

    /*The combination that has been punched in.
     * Once it reaches the length of the combination,
     * it will either unlock the door or reset*/
    private string currentCombo = "";

    // Use this for initialization
    void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called whenever the player punches in a number on the keypad
    public void numberInput(int number)
    {
        //Add number to current combination
        currentCombo += number.ToString();
        Debug.Log("Door #" + id + ": " + currentCombo);
        //If the current combination has as many numbers as the door's combination...
        if (currentCombo.Length == combination.ToString().Length)
        {
            //If the combination is correct...
            if (int.Parse(currentCombo) == combination)
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

    private IEnumerator openDoor()
    {
        float t = 0.0f;

        while(t <= 1)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, startPos + openOffset, Mathf.SmoothStep(0,1,t));
            yield return new WaitForFixedUpdate();
        }
    }

    public int getId()
    { return id; }
    public void setId(int newId)
    { id = newId; }
}
