using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
	public string verb = "Activate";

	public abstract void Interact();
}