﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonZone : MonoBehaviour
{
    public event Action onPressed;
    public event Action onReleased;

    private List<Collider> colliders = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            bool invoke = colliders.Count == 0;
            colliders.Add(other);
            if (invoke) {
                onPressed?.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (colliders.Remove(other)
                && colliders.Count == 0) {
                onReleased?.Invoke();
            }
        }
    }
}
