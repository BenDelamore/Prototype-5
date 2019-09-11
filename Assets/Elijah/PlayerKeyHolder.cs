using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerKeyHolder : MonoBehaviour
{
    public Transform pos;
    public List<KeyType> allowedTypes = new List<KeyType> { KeyType.None, KeyType.Blue, KeyType.Green, KeyType.Purple, KeyType.Red };
    public KeyPickup initialKeyPrefab;

    public KeyPickup held;

    public event Action<KeyType> onPickedUp;

    public KeyType heldKeyType { get { return held?.type ?? KeyType.None; } }

    public bool IsAllowedType(KeyType type) { return allowedTypes.Contains(type); }

    private void Awake()
    {
        if (held)
        {
            PickItUp(held);
        }
        else if (initialKeyPrefab)
        {
            if (IsAllowedType(initialKeyPrefab.type))
            { 
                var pickup = Instantiate(initialKeyPrefab);
                PickItUp(pickup);
                initialKeyPrefab = null;
            }
            else
            {
                Debug.LogError("initial key prefab's key type is not allowed for this holder", this);
            }
        }
    }

    private void PickItUp(KeyPickup pickup)
    {
        if (!pickup)
        {
            held = null;
            onPickedUp?.Invoke(KeyType.None);
            return;
        }
        held = pickup;
        pickup.currentHolder = this;
        pickup.transform.SetParent(pos, false);
        pickup.transform.localPosition = Vector3.zero;
        pickup.transform.localRotation = Quaternion.identity;
        onPickedUp?.Invoke(pickup.type);
    }

    public void Swap(PlayerKeyHolder other)
    {
        var a = other.IsAllowedType(this.heldKeyType);
        var b = this.IsAllowedType(other.heldKeyType);
        if (a && b)
        {
            var pickup = other.held;
            other.PickItUp(this.held);
            this.PickItUp(pickup);
        }
    }

    //public void Swap(PlayerKeyHolder other)
    //{
    //    var a = other.held;
    //    other.PickItUp(this.held);
    //    this.PickItUp(a);
    //}

    //public void Take(PlayerKeyHolder from)
    //{
    //    var pickup = from.held;
    //    if (pickup)
    //    {
    //        from.held = null;
    //        PickItUp(pickup);
    //    }
    //}

    //public void TakeIf(PlayerKeyHolder from, IEnumerable<KeyType> allowedTypes)
    //{
    //    var kt = from.heldKeyType;
    //    if (kt.HasValue)
    //    {
    //        var type = kt.Value;
    //        if (allowedTypes.Contains(type))
    //        {
    //            Take(from);
    //        }
    //    }
    //}
}
