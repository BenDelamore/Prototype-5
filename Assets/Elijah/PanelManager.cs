using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 3D array of spawned panel slots.
public class PanelManager : MonoBehaviour
{
    public PanelDuplicator duplicator;
    public bool generateOnAwake = true;

    [Header("Generated")]
    [SerializeField]
    private List<PanelSlot> slots = new List<PanelSlot>();

    private Dictionary<int, PanelSlot> slotMap;

    public PanelSlot this[Vector3Int coords]
    {
        get { return slotMap[linearIndex(coords)]; }
    }

    private void RegisterSlot((Vector3Int, GameObject) input)
    {
        var (coords, go) = input;
        var slot = go.GetComponent<PanelSlot>();
        Debug.Assert(slot);
        if (slot)
        {
            slots.Add(slot);
            slot.manager = this;
            slot.coords = coords;
        }
    }

    private int linearIndex(Vector3Int coords)
    {
        return coords.x + coords.y * duplicator.size.x
            + coords.z * duplicator.size.x * duplicator.size.y;
    }

    private void Awake()
    {
        if (generateOnAwake)
        {
            Erase();
            Generate();
        }

        slotMap = slots.ToDictionary(slot => linearIndex(slot.coords));

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

    [ContextMenu("! Erase")]
    private void ManualErase()
    {
        generateOnAwake = true;
        EraseImmediate();
    }

    [ContextMenu("! Erase and Regenerate")]
    private void ManualRegenerate()
    {
        generateOnAwake = false;
        EraseImmediate();
        Generate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var slot in slots)
        {
            Gizmos.DrawSphere(slot.transform.position, 0.1f);
        }
    }
}
