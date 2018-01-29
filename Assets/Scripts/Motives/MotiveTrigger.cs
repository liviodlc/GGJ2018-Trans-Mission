using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(BoxCollider))]
public class MotiveTrigger : NetworkBehaviour {
    bool isAgent;

    [Command]
    void CmdTrigger()
    {
        MotiveManager.Instance.MotiveComplete(isAgent);
    }

    private void OnTriggerEnter(Collider other)
    {
        CmdTrigger();
    }
}
