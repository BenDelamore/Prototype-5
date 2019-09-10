using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeyHolder : MonoBehaviour
{
    public Transform pos;
    public Vector3 offset = Vector3.up * 1.0f;
    public List<GameObject> heldObjects = new List<GameObject>();

    public event Action<GameObject> OnAdded;

    public void Add(GameObject itemToHold)
    {
        itemToHold.transform.SetParent(pos, false);
        itemToHold.transform.localPosition = offset * heldObjects.Count;
        itemToHold.transform.localRotation = Quaternion.identity;
        heldObjects.Add(itemToHold);
        OnAdded?.Invoke(itemToHold);
    }

    public void Exchange(PlayerKeyHolder from)
    {
        foreach (var go in from.heldObjects.ToArray())
        {
            from.heldObjects.Remove(go);
            Add(go);
        }
    }
    
    public void DisposeAll()
    {
        foreach (var go in heldObjects)
        {
            Destroy(go);
        }
        heldObjects.Clear();
    }
}
