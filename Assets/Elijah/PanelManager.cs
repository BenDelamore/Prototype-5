using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public PanelDuplicator duplicator;
    public Transform kindsParent;

    private PanelKind[] kinds;
    private PanelKind[] safeKinds;
    private List<PanelSlot> slots = new List<PanelSlot>();

    public PanelKind RandomKind() { return kinds[Random.Range(0, kinds.Length)]; }
    public PanelKind RandomSafeKind() { return safeKinds[Random.Range(0, safeKinds.Length)]; }

    private void GatherKinds()
    {
        kinds = kindsParent.GetComponentsInChildren<PanelKind>();
        Debug.Assert(kinds.Length > 0);
        safeKinds = kinds.Where(k => k.isSafe).ToArray();
        Debug.Assert(safeKinds.Length > 0);
    }

    private void Process((Vector3Int, GameObject) input)
    {
        var (coords, go) = input;
        var slot = go.GetComponent<PanelSlot>();
        slots.Add(slot);
        slot.Begin(this);
    }

    private void Start()
    {
        GatherKinds();

        duplicator.Result += Process;
        duplicator.Process();
    }
}
