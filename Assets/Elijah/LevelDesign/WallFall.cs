using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFall : MonoBehaviour
{
    public Rigidbody[] rbs;
    public float force = 1000;
    public float radius = 100;

    public void Explode()
    {
        foreach (var rb in rbs)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(force, transform.position, radius);
        }
    }
}
