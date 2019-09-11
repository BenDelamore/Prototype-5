using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHexThing : MonoBehaviour
{
    public PlayerButtonZone button;
    public List<MoveHexGroup> extendThese = new List<MoveHexGroup>();
    public List<MoveHexGroup> retractThese = new List<MoveHexGroup>();
    public bool buttonIsToggle = false;

    private void Start()
    {
        button.onPressed -= OnPressed;
        button.onPressed += OnPressed;

        button.onReleased -= OnReleased;
        button.onReleased += OnReleased;
    }

    private void OnDestroy()
    {
        button.onPressed -= OnPressed;
        button.onReleased -= OnReleased;
    }

    private void OnPressed()
    {
        foreach (var e in extendThese)
        {
            e.Extend();
        }

        foreach (var r in retractThese)
        {
            r.Retract();
        }
    }

    private void OnReleased()
    {
        if (buttonIsToggle)
        {
            foreach (var e in extendThese)
            {
                e.Retract();
            }

            foreach (var r in retractThese)
            {
                r.Extend();
            }
        }
    }
}
