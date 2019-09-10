using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHexGroup : MonoBehaviour
{
    public List<ExtendingObject> eos = new List<ExtendingObject>();

    public void Extend()
    {
        foreach (var eo in eos)
        {
            eo.Extend();
        }
    }

    public void Retract()
    {
        foreach (var eo in eos)
        {
            eo.Retract();
        }
    }
}
