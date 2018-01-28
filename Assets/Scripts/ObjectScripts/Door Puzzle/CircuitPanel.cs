using UnityEngine;

public class CircuitPanel : InteractiveObject
{
	public Rigidbody rb;
	public Vector3 bump;
	public GameObject nextTrigger;

	public override void Interact()
	{
		rb.transform.localPosition += bump;
		rb.isKinematic = false;
		gameObject.SetActive(false);
		nextTrigger.SetActive(true);
		GetComponent<BoxCollider>().enabled = false;
	}
}
