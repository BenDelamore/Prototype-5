using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExtendingObject : MonoBehaviour
{
    public Rigidbody rb;
    
    public Transform posExtended;
    public float durationExtend = 2;

    public Transform posRetracted;
    public float durationRetract = 2;

    private void Start()
    {
        rb.position = posRetracted.position;
    }

    public void Extend()
    {
        rb.DOKill(true);
        rb.DOMove(posExtended.position, durationExtend);
    }

    public void Retract()
    {
        rb.DOKill(true);
        rb.DOMove(posRetracted.position, durationRetract);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(posExtended?.position ?? transform.position, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(posRetracted?.position ?? transform.position, 0.1f);
    }
}
