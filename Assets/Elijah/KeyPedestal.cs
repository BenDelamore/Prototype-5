using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPedestal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var thisHolder = GetComponent<PlayerKeyHolder>();
            var playerHolder = other.GetComponent<PlayerKeyHolder>();
            thisHolder.Swap(playerHolder);
        }
    }
}
