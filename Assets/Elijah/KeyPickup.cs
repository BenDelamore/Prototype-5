using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject HeldPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var holder = other.GetComponentInParent<PlayerKeyHolder>();
            Debug.Assert(holder);
            var go = Instantiate(HeldPrefab);
            holder.Add(go);
            Destroy(gameObject);
        }
    }
}
