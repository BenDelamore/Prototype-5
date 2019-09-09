using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSlot : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Wow());
    }

    IEnumerator Wow()
    {
        yield return new WaitForSeconds(2);
    }
}
