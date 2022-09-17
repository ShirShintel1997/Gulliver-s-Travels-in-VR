using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Button : MonoBehaviour
{

    public UnityEvent buttonEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            buttonEvent.Invoke();

    }
}
