using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FinalDoor : MonoBehaviour
{
    private bool hasOpened = false;
    public void Open()
    {
        if (!hasOpened)
        {
            hasOpened = true;
            transform.DORotate(new Vector3(0, 90, 0), 3, RotateMode.LocalAxisAdd);
        }
    }
}
