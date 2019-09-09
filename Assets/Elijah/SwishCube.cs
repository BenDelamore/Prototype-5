using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwishCube : MonoBehaviour
{
    private void Start()
    {
        transform.DOShakeRotation(1).SetLoops(-1).SetEase(Ease.Linear);
    }
}
