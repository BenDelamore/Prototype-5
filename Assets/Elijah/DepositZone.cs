using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositZone : MonoBehaviour
{
    public int numDeposited = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var holder = other.GetComponent<PlayerKeyHolder>();
            numDeposited += holder.heldObjects.Count;
            holder.DisposeAll();
        }
    }
}
