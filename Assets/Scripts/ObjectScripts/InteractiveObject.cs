using UnityEngine;

public abstract class InteractiveObject : MonoBehaviour
{
	public string verb = "Activate";

	public AgentPlayer PlayerFace { get; set; }

	public abstract void Interact();

	private void OnDisable()
	{
		if (PlayerFace)
			PlayerFace.DeselectInteractiveObject(this);
	}
}