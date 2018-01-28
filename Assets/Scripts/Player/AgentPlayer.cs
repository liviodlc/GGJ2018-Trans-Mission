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

	private void Start()
	{
		isPlayerAgent = GameManager.Instance.playerMode == GameManager.PlayerMode.Server || testWithoutNetworking;
		cam.gameObject.SetActive(isPlayerAgent);

		if (isPlayerAgent)
			Cursor.lockState = CursorLockMode.Locked;
	}

	private void FixedUpdate()
	{
		if (!isPlayerAgent)
			return;

		turn += Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
		rb.MoveRotation(Quaternion.Euler(Vector3.up * turn));
		nod += -Input.GetAxis("Mouse Y") * nodSpeed * Time.deltaTime;
		nod = Mathf.Max(minMaxNod.x, Mathf.Min(minMaxNod.y, nod));
		cam.localEulerAngles = new Vector3(nod, 0, 0);

		Vector3 fwd = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		Vector3 horiz = transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		rb.velocity = fwd + horiz;

		bool isActionDown = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F);
		if (isActionDown && !wasActionDown)
			Interact();
		wasActionDown = isActionDown;
	}

	public void SetInteractiveObject(InteractiveObject target)
	{
		hoveringObject = target;
		canvas.SetActive(target != null);
		if (target)
			prompt.text = target.verb;
	}

	private void Interact()
	{
		if (hoveringObject)
			hoveringObject.Interact();
	}
}