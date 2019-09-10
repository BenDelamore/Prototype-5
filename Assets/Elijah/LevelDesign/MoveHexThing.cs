using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHexThing : MonoBehaviour
{
    public PlayerButtonZone button;
    public List<MoveHexGroup> extendThese = new List<MoveHexGroup>();
    public List<MoveHexGroup> retractThese = new List<MoveHexGroup>();

    private void Start()
    {
        button.onPressed -= OnPressed;
        button.onPressed += OnPressed;
    }

    private void OnDestroy()
    {
        button.onPressed -= OnPressed;
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
}
