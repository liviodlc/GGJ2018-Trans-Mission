﻿using UnityEngine;

public class WireTrigger : InteractiveObject
{
	public Rigidbody wireEnd;
	public ParticleSystem ps;

	public override void Interact()
	{
		wireEnd.isKinematic = false;
		ps.Play();
		gameObject.SetActive(false);
	}
}