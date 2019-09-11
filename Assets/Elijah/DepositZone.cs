using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("use KeyPedestal")]
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
            //th.TakeAll(ph);

            door.Open();
        }
    }
}
