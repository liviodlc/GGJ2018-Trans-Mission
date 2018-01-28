using UnityEngine;
using UnityEngine.Networking;

public class AgentPlayer : NetworkBehaviour
{
	[Header("Controls")]
	public float moveSpeed;
	public float turnSpeed;
	public float nodSpeed;
	public Vector2 minMaxNod;

	[Header("References")]
	public Transform cam;
	public Rigidbody rb;
	public GameObject canvas;
	public TMPro.TextMeshProUGUI prompt;

	[Header("Debug")]
	public bool testWithoutNetworking = false;

	private float turn = 0;
	private float nod = 0;
	private bool isPlayerAgent = false;
	private InteractiveObject hoveringObject;

	private bool wasActionDown = false;
    private bool isCursorLocked = true;

	[Command]
	public void CmdRaiseGlobalEvent(string eventName)
	{
		Debug.Log("SERVER raising global event: " + eventName);
		RpcRaiseGlobalEvent(eventName);
	}

	[ClientRpc]
	public void RpcRaiseGlobalEvent(string eventName)
	{
		Debug.Log("CLIENT raising global event: " + eventName);
		EventManager.TriggerEvent(eventName);
	}

	void TestEvents()
	{
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			CmdRaiseGlobalEvent(GameEvent.HackPanel);
		}

		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			CmdRaiseGlobalEvent(GameEvent.OpenDoor+"1");
		}

		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			CmdRaiseGlobalEvent(GameEvent.OpenDoor+"2");
		}

		if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			CmdRaiseGlobalEvent(GameEvent.OpenDoor+"3");
		}

		if(Input.GetKeyDown(KeyCode.Alpha4))
		{
			CmdRaiseGlobalEvent(GameEvent.OpenDoor+"4");
		}

		if(Input.GetKeyDown(KeyCode.Alpha5))
		{
			CmdRaiseGlobalEvent(GameEvent.OpenDoor+"5");
		}
		if(Input.GetKeyDown(KeyCode.Alpha6))
		{
			CmdRaiseGlobalEvent(GameEvent.OpenDoor+"6");
		}
	}

	private void Start()
	{
		isPlayerAgent = GameManager.Instance.playerMode == GameManager.PlayerMode.Client || testWithoutNetworking;
		cam.gameObject.SetActive(isPlayerAgent);

		if (isPlayerAgent)
			Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		if (!isPlayerAgent)
			return;
		
		TestEvents();
		
	}
	private void FixedUpdate()
	{
		if (!isPlayerAgent)
			return;

        //Only use mouse rotation if cursor is locked
        if (isCursorLocked)
        {
            turn += Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
            rb.MoveRotation(Quaternion.Euler(Vector3.up * turn));
            nod += -Input.GetAxis("Mouse Y") * nodSpeed * Time.deltaTime;
            nod = Mathf.Max(minMaxNod.x, Mathf.Min(minMaxNod.y, nod));
            cam.localEulerAngles = new Vector3(nod, 0, 0);
        }
        else
        {
            rb.angularVelocity = Vector3.zero;
        }

		Vector3 fwd = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		Vector3 horiz = transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		rb.velocity = fwd + horiz;

		bool isActionDown = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F);
		if (isActionDown && !wasActionDown)
			Interact();
		wasActionDown = isActionDown;

        if (Input.GetKeyDown(KeyCode.LeftAlt))
            isCursorLocked = !isCursorLocked;

        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
	}

	public void SelectInteractiveObject(InteractiveObject target)
	{
		hoveringObject = target;
		prompt.gameObject.SetActive(true);
		prompt.text = target.verb;
	}

	public void DeselectInteractiveObject(InteractiveObject target)
	{
		if (hoveringObject != target)
			return;
		hoveringObject = null;
		prompt.gameObject.SetActive(false);
	}

    private void Interact()
	{
		if (hoveringObject)
			hoveringObject.Interact();
	}
}