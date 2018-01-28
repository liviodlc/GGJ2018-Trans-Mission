using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvent
{
	public static string HackPanel = "hackpanel-001";
	public static string OpenFirstDoor = "open-first-door";
}

public class EventManager : MonoBehaviour 
{
    private static EventManager Instance;

	void Start()
	{
		if(Instance != null)
		{
			Destroy(this.gameObject);
		}
		Instance = this;
		Init();
	}

    private Dictionary<string, UnityEvent> eventDictionary;

    void Init()
    {
		DontDestroyOnLoad(this);

        if(eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        if(!Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent = new UnityEvent();
            Instance.eventDictionary.Add(eventName, thisEvent);
        }

        thisEvent.AddListener(listener);
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (!Instance) return;

        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}