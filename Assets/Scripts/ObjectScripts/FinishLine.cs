using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : InteractiveObject
{
	private AgentPlayer playa;
	private float timer;
	private Color OrigColor;

	public Color GoodColor;
	public Color BadColor;
	public float ColorChangeTime;
	public Renderer ScreenRend;
	public AudioClip WinSound;

	private void Start()
	{
		OrigColor = ScreenRend.material.color;
	}

	public override void Interact()
	{
		if (null == playa)
			playa = FindObjectOfType<AgentPlayer>();
		if(playa.HasTreasure)
		{
			StartCoroutine(ChangeColor(true));
			playa.CmdRaiseGlobalEvent(GameEvent.GameWon);
			AudioSource.PlayClipAtPoint(WinSound, transform.position);
		}
		else
			StartCoroutine(ChangeColor(false));
	}

	private IEnumerator ChangeColor(bool IsGood)
	{
		while(timer < ColorChangeTime)
		{
			ScreenRend.material.color = IsGood ? Color.Lerp(OrigColor, GoodColor, timer / ColorChangeTime) : Color.Lerp(OrigColor, BadColor, timer / ColorChangeTime);
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		ScreenRend.material.color = IsGood ? GoodColor : BadColor;
		timer = 0;
		while(timer < ColorChangeTime)
		{
			ScreenRend.material.color = IsGood ? Color.Lerp(GoodColor, OrigColor, timer / ColorChangeTime) : Color.Lerp(BadColor, OrigColor, timer / ColorChangeTime);
			timer += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		ScreenRend.material.color = OrigColor;
		timer = 0;
	}
}