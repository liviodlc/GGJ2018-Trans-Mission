using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class MotiveDropPoint : MonoBehaviour {

    public List<PickupObject> objectsToCollect;
    public bool isAgent;

	// Use this for initialization
	void Start () {
        objectsToCollect = new List<PickupObject>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PickupObject>() != null)
        {
            PickupObject po = other.gameObject.GetComponent<PickupObject>();
            if (objectsToCollect.Contains(po) && !po.isMotiveComplete)
            {
                po.isMotiveComplete = true;

                MotiveManager.Instance.MotiveComplete(isAgent);
            }
        }
    }
}
