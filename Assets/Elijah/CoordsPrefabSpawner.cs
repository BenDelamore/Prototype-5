using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawns the prefab multiple times into each slot based on coordinates.
public class CoordsPrefabSpawner : MonoBehaviour
{
    public PanelManager grid;
    public GameObject prefab;
    public List<Vector3Int> coords = new List<Vector3Int> { new Vector3Int(0, 0, 0) };

    public event Action<Vector3Int, GameObject> onSpawned;

    private void Start()
    {
        foreach (var coord in coords)
        {
            var slot = grid[coord];
            var go = Instantiate(prefab, slot.transform, false);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            if (onSpawned != null)
            {
                onSpawned(coord, go);
            }
        }
    }
}
