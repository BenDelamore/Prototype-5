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
        Debug.Assert(currentHolder);
        Debug.Assert(type != KeyType.None);
    }
}
