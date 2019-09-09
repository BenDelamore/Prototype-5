using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public PanelDuplicator duplicator;
    public bool generateOnAwake = true;

    [Header("Generated")]
    [SerializeField]
    private List<PanelSlot> slots = new List<PanelSlot>();

    private void RegisterSlot((Vector3Int, GameObject) input)
    {
        var (coords, go) = input;
        var slot = go.GetComponent<PanelSlot>();
        Debug.Assert(slot);
        if (slot)
        {
            slots.Add(slot);
            slot.manager = this;
        }
    }

    private void Start()
    {
        if (generateOnAwake)
        {
            Erase();
            Generate();
        }

        foreach (var slot in slots)
        {
            slot.BroadcastMessage("Begin", SendMessageOptions.DontRequireReceiver);
        }
    }

    private void Generate()
    {
        duplicator.Result += RegisterSlot;
        try
        {
            duplicator.Process();
        }
        finally
        {
            duplicator.Result -= RegisterSlot;
        }
    }

    private void Erase()
    {
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();
    }

    private void EraseImmediate()
    {
        foreach (var slot in slots)
        {
            DestroyImmediate(slot.gameObject);
        }
        slots.Clear();
    }

    [ContextMenu("Erase")]
    private void ManualErase()
    {
        generateOnAwake = true;
        EraseImmediate();
    }

    [ContextMenu("Erase and Regenerate")]
    private void ManualRegenerate()
    {
        generateOnAwake = false;
        EraseImmediate();
        Generate();
    }
}
