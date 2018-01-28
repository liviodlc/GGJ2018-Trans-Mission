using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotiveCompleteText : MonoBehaviour {

    public Text motiveText;
    //How long the blinks should be
    public float blinkTime = 1f;
    //How many blinks that will happen
    public int maxBlinks = 3;

	// Use this for initialization
	void Start () {
		
	}

    public void doBlink()
    {
        StartCoroutine(blink());
    }
	
	IEnumerator blink()
    {
        int numBlinks = 0;

        //Color color
        Color textColor = motiveText.color;
        //Invisible color
        Color invisibleColor = motiveText.color;
        invisibleColor.a = 0;

        //Enable Text
        motiveText.gameObject.SetActive(true);

        WaitForSeconds waitTime = new WaitForSeconds(blinkTime);
        while(numBlinks < maxBlinks)
        {
            motiveText.color = invisibleColor;
            yield return waitTime;

            motiveText.color = textColor;
            yield return waitTime;
            numBlinks++;
        }
    }
}
