using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType
{
    None,
    Red,
    Green,
    Blue,
    Purple,
}

public class KeyPickup : MonoBehaviour
{
    public PlayerKeyHolder currentHolder;
    public KeyType type = KeyType.Blue;

    private void Start()
    {
        Debug.Assert(currentHolder, "Holder is null. Did you accidentally put a key in the scene? Only pedestals should spawn keys.", this);
        Debug.Assert(type != KeyType.None, "KeyType is None. Did you forget to use a key variant?", this);
    }
}
