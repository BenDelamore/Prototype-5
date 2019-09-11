using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShieldBarrier : MonoBehaviour
{
    public PlayerButtonZone zone;

    public Collider barrier;

    private void Awake()
    {
        zone.onPressed -= OnPressed;
        zone.onPressed += OnPressed;

        zone.onReleased -= OnReleased;
        zone.onReleased += OnReleased;

        barrier.enabled = true;
    }

    private void OnDestroy()
    {
        zone.onPressed -= OnPressed;
        zone.onReleased -= OnReleased;
    }

    private void OnPressed()
    {
        barrier.enabled = false;
    }

    private void OnReleased()
    {
        barrier.enabled = true;
    }
}
