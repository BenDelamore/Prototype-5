using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositZone : MonoBehaviour
{
    public FinalDoor door;
    public int requiredCount = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var ph = other.GetComponentInParent<PlayerKeyHolder>();
            var th = GetComponent<PlayerKeyHolder>();
            th.Exchange(ph);

            if (th.heldObjects.Count >= requiredCount)
            {
                door.Open();
            }
        }
    }
}
