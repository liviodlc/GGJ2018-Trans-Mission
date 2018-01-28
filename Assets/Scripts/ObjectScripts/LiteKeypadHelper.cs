using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Lighter version of VaultPasswordGenerator.cs
public class LiteKeypadHelper : MonoBehaviour {

    List<char> symbols;

    public LockedDoor doorRef;
    //The keypad, for key generation.
    public GameObject keypadRef;

    //File contains all characters that will be used within the password.
    public TextAsset symbolList;

    private void Awake()
    {
        if (keypadRef == null)
            keypadRef = gameObject;

        symbols = new List<char>();

        //Populate list
        foreach (char c in symbolList.text)
        {
            symbols.Add(c);
        }

        Keypad[] keys = keypadRef.GetComponentsInChildren<Keypad>();
        Text[] texts = keypadRef.GetComponentsInChildren<Text>();

        //Make keys
        for (int i = 0; i < symbols.Count; i++)
        {
            keys[i].symbol = symbols[i];
            keys[i].doorRef = doorRef;
            texts[i].text = char.ToString(symbols[i]);
        }
    }

    private void Update()
    {
        if (keypadRef.GetComponentInChildren<Canvas>().worldCamera == null)
        {
            //Set canvas event camera
            if(FindObjectsOfType<AgentPlayer>().Length > 0)
                keypadRef.GetComponentInChildren<Canvas>().worldCamera = FindObjectsOfType<AgentPlayer>()[0].GetComponentInChildren<Camera>(); //NetManager.Instance.ClientPlayer.GetComponentInChildren<Camera>();
        }
    }
}
