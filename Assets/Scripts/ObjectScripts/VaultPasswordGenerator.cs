using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Generates a big string from a list of available characters and then returns the password in chunks, hidden within a giant wall of text
public class VaultPasswordGenerator : MonoBehaviour {

    List<char> symbols;

    public LockedDoor doorRef;
    //The keypad, for key generation.
    public GameObject keypadRef;
    
    //File contains all characters that will be used within the password.
    public TextAsset symbolList;
    //File contains strings that will be used to generate giant text spam
    //public TextAsset spamList;
    public int passwordLength = 20;
    //How long each chunk of the password is.
    public int chunkLength = 4;
    public int spamLength = 100;

    string password = "";
    string spam = "";


    public void Awake()
    {
        symbols = new List<char>();

        //Populate list
        foreach (char c in symbolList.text)
        {
            symbols.Add(c);
        }

        //Generate password
        for (int i = 0; i < passwordLength; i++)
            password += symbols[Random.Range(0, symbols.Count)];
        Debug.Log(password);

        //Generate Spam
        int count = 0;
        for(int i = 0; i < spamLength; i++)
        {
            spam += symbols[Random.Range(0, symbols.Count)];
            if (Random.Range(0, 5) == 0)
                spam += ", ";
            if (i % Mathf.RoundToInt(spamLength / (passwordLength/chunkLength)) == 0)
            {
                spam += (", pass" + count + ": " + password.Substring(chunkLength * count++, chunkLength) + ", ");
            }
        }
        Debug.Log(spam);

        doorRef.combination = password;

        Keypad[] keys = keypadRef.GetComponentsInChildren<Keypad>();
        Text[] texts = keypadRef.GetComponentsInChildren<Text>();
        //Make keys
        for (int i = 0; i < symbols.Count; i++)
        {
            keys[i].symbol = symbols[i];
            keys[i].doorRef = this.doorRef;
            texts[i].text = char.ToString(symbols[i]);
        }

    }

    public string getSpam() { return spam; }

}
