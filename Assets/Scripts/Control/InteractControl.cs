using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpControl : MonoBehaviour
{
    public Boolean isInRange;
    public KeyCode interactKey;
    public UnityEvent interactEvent;

    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactEvent.Invoke();
                UnityEngine.Debug.Log($"{transform.parent.name} Interact Event invoke");
            }
        }
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            isInRange = true;
            UnityEngine.Debug.Log($"{transform.parent.name} is in range");
        }

    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.transform.CompareTag("Player"))
        {
            isInRange = false;
            UnityEngine.Debug.Log($"{transform.parent.name} is out of range");
        }
    }

}
