using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMash : MonoBehaviour {

    public string text;
    public TextAsset file;
    public int maxChunkLength = 10;
    public int minChunkLength = 3;

    List<string> chunks;

	// Use this for initialization
	void Start () {
        text = file.text;

        //Generate chunks
        int i = 0;
        while( i < text.Length)
        {
            int chunkLength = Random.Range(minChunkLength, maxChunkLength + 1);
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
