using UnityEngine;

public class CircuitPanel : InteractiveObject
{
	public Rigidbody rb;
	public Vector3 bump;

	public override void Interact()
	{
		rb.transform.localPosition += bump;
		rb.isKinematic = false;
		gameObject.SetActive(false);
	}
}
