using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NPCControl : MonoBehaviour
{

    public int eventId;
    public UnityEvent<int> talkEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void talk()
    {
        talkEvent?.Invoke(eventId);
    }
}
