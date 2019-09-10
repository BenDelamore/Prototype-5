using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonZone : MonoBehaviour
{
    public event Action onPressed;
    public event Action onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            onPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            onReleased?.Invoke();
        }
    }
}
