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

	private NavMeshAgent ThisNavAgent;
	private float OrigSpeed;
	private float timer;
	private bool IsLooking;
	private Transform LookTarget;
	private navMove PathComp;

	private void Start()
	{
		ThisNavAgent = GetComponent<NavMeshAgent>();
		PathComp = GetComponent<navMove>();
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
		yield return new WaitForSeconds(WaitTime);
		IsLooking = false;
		timer = 0;
		ThisNavAgent.speed = OrigSpeed;
	}

	public void LookAtSomething(Transform Target)
	{
		LookTarget = Target;
		IsLooking = true;
	}

	public void OnDetectedTarget(SightTargetInfo info)
	{
		Debug.Log("found " + info.target.name);
		//StopAllCoroutines();
		//PathComp.Stop();
		PathComp.enabled = false;
		//ThisNavAgent.isStopped = true;
		//ThisNavAgent.ResetPath();
		ThisNavAgent.SetDestination(info.target.transform.position);
	}

	public void OnUnDetectedTarget(SightTargetInfo info)
	{
		Debug.Log("can't see " + info.target.name);
	}

	public void OnTargetCameIntoRange(SightTargetInfo info) { Debug.Log(info.target.name + "is in range"); }
	public void OnTargetWentOutOfRange(SightTargetInfo info) { Debug.Log(info.target.name + "is out of range"); }
	public void OnTargetDestroyed(SightTargetInfo info) {}
	public void OnTryingToDetectTarget(SightTargetInfo info) {}
	public void OnDetectingTarget(SightTargetInfo info) {}
	public void OnStopDetectingTarget(SightTargetInfo info) {}
}