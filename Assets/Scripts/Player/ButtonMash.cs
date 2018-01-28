using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Takes in String*/
public class ButtonMash : MonoBehaviour {

    public string text;
    public TextAsset file;
    public Text outputText;
    public int maxChunkLength = 10;
    public int minChunkLength = 3;

    List<string> chunks;

    public bool debugMash = true;
    int debugIdx = 0;

	// Use this for initialization
	void Awake () {
        if(file != null)
            text = file.text;

        chunks = new List<string>();

        //Generate chunks
        int i = 0;
        while( i < text.Length)
        {
            int chunkLength = Random.Range(minChunkLength, maxChunkLength + 1);
            int i2 = i + chunkLength;
            if (i2 + chunkLength < text.Length)
                chunks.Add(text.Substring(i, chunkLength));
            else
                chunks.Add(text.Substring(i, text.Length));

            i = i2;
        }

	}

    void Update()
    {
        if (debugMash && Input.anyKeyDown)
        {
            Debug.Log(getChunk(debugIdx++));
        }

        if(outputText != null && Input.anyKeyDown)
        {
            outputText.text += getChunk(debugIdx++);
        }
    }


    string getChunk(int idx)
    {
        return idx < chunks.Count ? chunks[idx] : null;
    }

    public List<string> getChunks() { return chunks; }

}
