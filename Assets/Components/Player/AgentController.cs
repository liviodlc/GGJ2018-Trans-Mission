using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour {

    public GameObject _cameraObj;
    public float turnSpeed = 150f;
    public float moveSpeed = 3f;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {

        var x = Input.GetAxis("Mouse Y") * Time.deltaTime * turnSpeed;
        var y = Input.GetAxis("Mouse X") * Time.deltaTime * turnSpeed;
        var fwd = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var horiz = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        transform.Rotate(0, y, 0);
        transform.Translate(horiz, 0, fwd);

        //Update camera
       // _cameraObj.transform.position = transform.position;
       // _cameraObj.transform.rotation = new Quaternion(_cameraObj.transform.rotation.x,
       //                                                 transform.rotation.y,
       //                                                 _cameraObj.transform.rotation.z,
       //                                                 transform.rotation.w);
                                                        
       //_cameraObj.transform.Rotate(-x, 0, 0);
	}

}
