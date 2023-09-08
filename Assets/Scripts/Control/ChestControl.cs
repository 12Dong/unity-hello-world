using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;

public class ChestControl : MonoBehaviour
{

    public Boolean isInRange;
    public KeyCode interactKey;

    public UnityEvent interactAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                UnityEngine.Debug.Log("InteractAction invoke");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        UnityEngine.Debug.Log("Is In Range turn true");
        isInRange = true;
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        
    }
    

}
