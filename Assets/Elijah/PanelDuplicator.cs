using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDuplicator : MonoBehaviour
{
    public GameObject prefab;
    public Vector3Int size = new Vector3Int(10, 1, 10);
    public Vector3 offset = new Vector3(2, 2, 2);

    public event Action<(Vector3Int, GameObject)> Result;

    public void Process()
    {
        for (int z = 0; z < size.z; z++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var go = Instantiate(prefab, transform, false);
                    var coords = new Vector3Int(x, y, z);
                    go.name = prefab.name + " at coords " + coords;
                    go.transform.localPosition = Vector3.Scale(coords, offset);
                    go.transform.localRotation = Quaternion.identity;
                }
            }
        }
    }
}
