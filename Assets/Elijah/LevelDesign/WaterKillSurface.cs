using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterKillSurface : MonoBehaviour
{
    public PlayerButtonZone zone;

    private void Start()
    {
        zone.onPressed -= OnPressed;
        zone.onPressed += OnPressed;
    }

    private void OnDestroy()
    {
        zone.onPressed -= OnPressed;
    }

    private void OnPressed()
    {
        FindObjectOfType<PlayerStats>().Damage(4000);
    }
}
