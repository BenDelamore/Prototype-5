using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExtendingPanel : MonoBehaviour
{
    public Transform posExtended;
    public Transform posRetracted;
    public Transform movingObject;
    public float durationExtend = 2;
    public float durationStay = 2;
    public float durationRetract = 2;

    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        var seq = DOTween.Sequence();
        seq.Append(rb.DOMove(posExtended.position, durationExtend));
        seq.AppendInterval(durationStay);
        seq.Append(rb.DOMove(posRetracted.position, durationRetract));
    }
}
