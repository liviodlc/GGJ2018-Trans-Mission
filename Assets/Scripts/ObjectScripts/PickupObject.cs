using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PickupObject : InteractiveObject {
    
    public Vector3 offest;
    bool isHeld = false;

    public Collider mainCollider;

    InteractionCollider IC;
    string originalVerb;

    private void Start()
    {
        IC = GetComponent<InteractionCollider>();
        originalVerb = verb;
    }

    public override void Interact()
    {

        if(!isHeld)
        {
            transform.SetParent(IC.player.gameObject.transform);
            //Disable physics
            GetComponent<Rigidbody>().isKinematic = true;
            mainCollider.enabled = false;

            transform.position = IC.player.transform.position;
            transform.localPosition = offest;
            isHeld = true;
            verb = "Drop";
        }
        else
        {
            transform.SetParent(null);
            //Enable physics
            GetComponent<Rigidbody>().isKinematic = false;
            mainCollider.enabled = true;

            isHeld = false;
            verb = originalVerb;
        }
    }
}
