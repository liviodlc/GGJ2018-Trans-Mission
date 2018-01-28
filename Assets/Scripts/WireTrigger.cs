using UnityEngine;

public class WireTrigger : InteractiveObject
{
	public Rigidbody wireEnd;

	public override void Interact()
	{
		wireEnd.isKinematic = false;
		gameObject.SetActive(false);
	}
}