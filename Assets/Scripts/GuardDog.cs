using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Devdog.LosPro;
using SWS;

public class GuardDog : MonoBehaviour, IObserverCallbacks
{
	public float LookSpeed;
	public float WaitToLookTime;
	public Animator ThisAnim;
	public float RunSpeed;

	[Header("Sounds")]
	public AudioClip SniffSound;
	public AudioClip WalkingSound;
	public AudioClip[] BarkSounds;
	public AudioClip[] WhimperSounds;

	private NavMeshAgent ThisNavAgent;
	private float OrigSpeed;
	private navMove PathComp;
	private AudioSource ThisAudio;

	private void Start()
	{
		ThisNavAgent = GetComponent<NavMeshAgent>();
		PathComp = GetComponent<navMove>();
		ThisAudio = GetComponent<AudioSource>();
		OrigSpeed = ThisNavAgent.speed;
	}

	public void StopAndGo(float WaitTime)
	{
		StartCoroutine(StopAndGoExecute(WaitTime));
	}

	public void BackToNormal()
	{
		ThisAudio.Stop();
		ThisAnim.SetBool("IsRunning", false);
		ThisAnim.SetBool("isWalking", true);
		ThisNavAgent.speed = OrigSpeed;
		PlaySound("Walking");
	}

	private IEnumerator StopAndGoExecute(float WaitTime)
	{
		ThisNavAgent.speed = 0;
		ThisAnim.SetBool("isWalking", false);
		ThisAnim.SetBool("IsRunning", false);
		ThisAudio.Stop();
		yield return new WaitForSeconds(WaitTime);
		ThisAnim.SetBool("isWalking", true);
		ThisNavAgent.speed = OrigSpeed;
		PlaySound("Walking");
	}

	public void PlayAnim(string key)
	{
		ThisAnim.SetTrigger(key);
	}

	public void PlaySound(string key)
	{
		switch (key)
		{
			case "Sniff":
				StartCoroutine(WaitingToSniff());
				break;
			case "Walking":
				ThisAudio.loop = true;
				ThisAudio.clip = WalkingSound;
				ThisAudio.Play();
				break;
			case "Bark":
				ThisAudio.loop = true;
				ThisAudio.clip = BarkSounds[Random.Range(0, BarkSounds.Length)];
				ThisAudio.Play();
				break;
			case "Whimper":
				ThisAudio.PlayOneShot(WhimperSounds[Random.Range(0, WhimperSounds.Length)]);
				break;
		}
	}

	private IEnumerator WaitingToSniff()
	{
		yield return new WaitForSeconds(1);
		ThisAudio.PlayOneShot(SniffSound);
	}

	public void OnDetectedTarget(SightTargetInfo info)
	{
		PathComp.enabled = false;
		ThisNavAgent.speed = RunSpeed;
		ThisAnim.SetTrigger("Bark");
		ThisAnim.SetBool("IsRunning", true);
		PlaySound("Bark");
	}

	public void OnUnDetectedTarget(SightTargetInfo info)
	{
		PathComp.enabled = true;
		ThisAudio.loop = false;
	}

	public void OnDetectingTarget(SightTargetInfo info) { ThisNavAgent.SetDestination(info.target.transform.position); }

	public void OnTargetCameIntoRange(SightTargetInfo info) {}
	public void OnTargetWentOutOfRange(SightTargetInfo info) {}
	public void OnTargetDestroyed(SightTargetInfo info) {}
	public void OnTryingToDetectTarget(SightTargetInfo info) {}
	public void OnStopDetectingTarget(SightTargetInfo info) {}
}