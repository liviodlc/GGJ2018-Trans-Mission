using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MotiveManager : Singleton<MotiveManager> {

    public int agentMotivesComplete = 0;
    public int doctorMotivesComplete = 0;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void MotiveComplete(bool isAgent)
    {
        if (isAgent)
        {
            agentMotivesComplete++;
            //player.GetComponent<AgentPlayer>().motiveText.text = "AGENT MOTIVE COMPLETE!";

        }
        else
        {
            doctorMotivesComplete++;
            //player.GetComponent<AgentPlayer>().motiveText.text = "DOCTOR MOTIVE COMPLETE!";
        }

       // player.GetComponent<AgentPlayer>().DoBlink();

        //GameObject players[] = [NetManager.singleton.ClientPlayer, ]
    }
}
