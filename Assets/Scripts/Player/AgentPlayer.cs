using UnityEngine;
using UnityEngine.Networking;

public class AgentPlayer : NetworkBehaviour
{
	[Header("Controls")]
	public float moveSpeed;
	public float turnSpeed;
	public float nodSpeed;

	[Header("References")]
	public Transform cam;
	public Rigidbody rb;

	private float turn = 0;
	private float nod = 0;
	private bool isPlayerAgent = false;

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;

		isPlayerAgent = GameManager.Instance.playerMode == GameManager.PlayerMode.Server;
		cam.gameObject.SetActive(isPlayerAgent);
	}

	[ClientCallback]
	private void FixedUpdate()
	{
		turn += Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime;
		rb.MoveRotation(Quaternion.Euler(Vector3.up * turn));
		nod += -Input.GetAxis("Mouse Y") * nodSpeed * Time.deltaTime;
		cam.localEulerAngles = new Vector3(nod, 0, 0);

		Vector3 fwd = transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		Vector3 horiz = transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		rb.velocity = fwd + horiz;
	}
}