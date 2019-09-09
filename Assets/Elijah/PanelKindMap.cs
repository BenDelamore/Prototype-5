using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Collection of PanelKind objects.
public class PanelKindMap : MonoBehaviour
{
    public Transform kindsParent;
    private PanelKind[] kinds;
    private PanelKind[] safeKinds;

    public PanelKind RandomKind() { return kinds[Random.Range(0, kinds.Length)]; }
    public PanelKind RandomSafeKind() { return safeKinds[Random.Range(0, safeKinds.Length)]; }

    private void GatherKinds()
    {
        kinds = kindsParent.GetComponentsInChildren<PanelKind>();
        Debug.Assert(kinds.Length > 0);
        safeKinds = kinds.Where(k => k.isSafe).ToArray();
        Debug.Assert(safeKinds.Length > 0);
    }

    private void Awake()
    {
        if (kindsParent == null) { kindsParent = transform; }
        GatherKinds();
    }
}
