using UnityEngine;

public class WakeObject : MonoBehaviour
{
	public GameObject target;

	private void LateUpdate()
	{
		target.SetActive(true);
		gameObject.SetActive(false);
	}
}
