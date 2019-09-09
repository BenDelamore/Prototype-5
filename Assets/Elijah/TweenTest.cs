using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this.transform.DOMove(new Vector3(5, 2, 1), 5.0f).SetEase(Ease.OutElastic);
        transform.DOPunchRotation(new Vector3(5, 0, 0), 5f, 5, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
