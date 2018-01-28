using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Keypad : MonoBehaviour {

    Button _button;
    //number that this button represents.  0-9.
    public char symbol;

    //Reference to door that the keypad unlocks
    public LockedDoor doorRef;

	// Use this for initialization
	void Start () {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(_onClick);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void _onClick()
    {
        //Do nothing if door is unlocked
        if (!doorRef.isLocked)
            return;

        doorRef.numberInput(symbol);
    }

}
