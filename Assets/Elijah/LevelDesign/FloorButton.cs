using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class FloorButton : MonoBehaviour
{
    public PlayerButtonZone zone;
    public ExtendingObject eo;

    private void Awake()
    {
        zone.onPressed -= OnPressed;
        zone.onPressed += OnPressed;

        zone.onReleased -= OnReleased;
        zone.onReleased += OnReleased;
    }

    private void OnPressed()
    {
        eo.Extend();
    }

    private void OnReleased()
    {
        eo.Retract();
    }
}
