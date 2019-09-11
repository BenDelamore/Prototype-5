﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public float grabTime = 1;

    private void Start()
    {
        Debug.Assert(currentHolder, "Holder is null. Did you accidentally put a key in the scene? Only pedestals should spawn keys.", this);
        Debug.Assert(type != KeyType.None, "KeyType is None. Did you forget to use a key variant?", this);
    }

    public void OnPickedUp(PlayerKeyHolder holder)
    {
        currentHolder = holder;
        transform.SetParent(holder.pos, true);
        transform.DOKill();
        var seq = DOTween.Sequence();
        seq.Append(transform.DOLocalMove(Vector3.zero, grabTime));
        seq.Insert(0, transform.DOLocalRotate(Vector3.zero, grabTime));
    }
}
