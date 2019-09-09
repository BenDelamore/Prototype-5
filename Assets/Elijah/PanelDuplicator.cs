using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDuplicator : MonoBehaviour
{
    public GameObject prefab;

    // number of columns, rows, and floors.
    public Vector3Int size = new Vector3Int(10, 1, 10);

    public Vector3 offset = new Vector3(defaultMinorLength, 0, defaultMinorLength / hexagonMinorFromMajor);
    
    public Vector3 displacementEvenZ = new Vector3(defaultMinorLength / 2, 0, 0);



    public event Action<(Vector3Int, GameObject)> Result;



    public const float defaultMinorLength = 2;
    public static float hexagonMinorFromMajor = 1.15470053838f;



    public void Process()
    {
        for (int z = 0; z < size.z; z++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var coords = new Vector3Int(x, y, z);

                    var go = Instantiate(prefab, transform, false);
                    go.name = prefab.name + " at coords " + coords;

                    var pos = Vector3.Scale(coords, offset);
                    if (z % 2 == 0) { pos += displacementEvenZ; }
                    go.transform.localPosition = pos;

                    go.transform.localRotation = Quaternion.identity;

                    Result((coords, go));
                }
            }
        }
    }
}
