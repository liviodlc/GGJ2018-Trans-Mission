using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Generates a big string from a list of available characters and then returns the password hidden within a giant wall of text
public class VaultPasswordGenerator : MonoBehaviour {

    List<char> symbols;
    public TextAsset symbolList;

    string password;

    public void Start()
    {
        //Populate list
        foreach(char c in symbolList.text)
        {
            symbols.Add(c);
        }

        //Generate password
    }

}
