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
	private float timer;
	private bool IsLooking;
	private Transform LookTarget;
	private navMove PathComp;
	private AudioSource ThisAudio;

	private void Start()
	{
		ThisNavAgent = GetComponent<NavMeshAgent>();
		PathComp = GetComponent<navMove>();
		ThisAudio = GetComponent<AudioSource>();
		OrigSpeed = ThisNavAgent.speed;
	}

	private void Update()
	{
		if(IsLooking)
		{
			if(timer < WaitToLookTime)
			{
				timer += Time.deltaTime;
				return;
			}
			Vector3 Targ = new Vector3(LookTarget.position.x, transform.position.y, LookTarget.position.z);
			var TargetRot = Quaternion.LookRotation(Targ - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, TargetRot, LookSpeed * Time.deltaTime);
		}
	}

	public void StopAndGo(float WaitTime)
	{
		StartCoroutine(StopAndGoExecute(WaitTime));
	}

	private IEnumerator StopAndGoExecute(float WaitTime)
	{
		ThisNavAgent.speed = 0;
		ThisAnim.SetBool("isWalking", false);
		ThisAnim.SetBool("IsRunning", false);
		ThisAudio.Stop();
		yield return new WaitForSeconds(WaitTime);
		IsLooking = false;
		timer = 0;
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

	public void LookAtSomething(Transform Target)
	{
		LookTarget = Target;
		IsLooking = true;
	}

	public void OnDetectedTarget(SightTargetInfo info)
	{
		PathComp.enabled = false;
		ThisNavAgent.speed = RunSpeed;
		ThisNavAgent.SetDestination(info.target.transform.position);
		ThisAnim.SetTrigger("Bark");
		ThisAnim.SetBool("IsRunning", true);
		PlaySound("Bark");
	}

	public void OnUnDetectedTarget(SightTargetInfo info)
	{
		PathComp.enabled = true;
		ThisAudio.loop = false;
	}

	public void OnTargetCameIntoRange(SightTargetInfo info) {}
	public void OnTargetWentOutOfRange(SightTargetInfo info) {}
	public void OnTargetDestroyed(SightTargetInfo info) {}
	public void OnTryingToDetectTarget(SightTargetInfo info) {}
	public void OnDetectingTarget(SightTargetInfo info) {}
	public void OnStopDetectingTarget(SightTargetInfo info) {}
}