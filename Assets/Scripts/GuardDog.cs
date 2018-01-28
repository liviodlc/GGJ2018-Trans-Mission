using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardDog : MonoBehaviour
{
	public float LookSpeed;
	public float WaitToLookTime;

	private NavMeshAgent ThisNavAgent;
	private float OrigSpeed;
	private float timer;
	private bool IsLooking;
	private Transform LookTarget;

	private void Start()
	{
		ThisNavAgent = GetComponent<NavMeshAgent>();
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
}