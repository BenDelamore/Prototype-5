using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonZone : MonoBehaviour
{
    public event Action onPressed;
    public event Action onReleased;
    public List<KeyType> allowedTypes = new List<KeyType> { KeyType.Red };

    private List<Collider> colliders = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var thing = other.GetComponentInParent<PlayerKeyHolder>();
            if (thing && allowedTypes.Contains(thing.heldKeyType))
            {
                // press this button
                bool invoke = colliders.Count == 0;
                colliders.Add(other);
                if (invoke)
                {
                    onPressed?.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (colliders.Remove(other)
                && colliders.Count == 0) {
                onReleased?.Invoke();
            }
        }
    }
}
