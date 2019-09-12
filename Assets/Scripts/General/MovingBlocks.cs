using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingBlocks : MonoBehaviour {

    [SerializeField] private float movePeriod = 2f;
    [SerializeField] private float beatOffset = 0f;
    [SerializeField] private float beatYAmount;
    [SerializeField] private float tweenDuration;

    private bool atNewPos = false;
    private float posXInitial;
    private float posYInitial;

    private float beatInterval;
    private int bpmBefore;
    private float beatTimer;

    // Use this for initialization
    void Start()
    {
        posXInitial = transform.localPosition.x;
        posYInitial = transform.localPosition.y;

        beatInterval = movePeriod;
        beatTimer = beatInterval + beatOffset;
    }

    // Update is called once per frame
    void Update()
    {
        beatInterval = movePeriod;
        beatTimer -= Time.deltaTime;

        float tweenToY = atNewPos ? posYInitial : (posYInitial + beatYAmount);

        if (beatTimer <= 0)
        {
            transform.DOKill(true);

            if (Mathf.Abs(beatYAmount) > 0f || Mathf.Abs(beatYAmount) > 0f)
            {
                //transform.DOMove(new Vector3(posXInitial, tweenToY, 0.0f), tweenDuration).SetEase(Ease.InOutQuad);
                transform.DOMoveY(tweenToY, tweenDuration).SetEase(Ease.InOutQuad);
            }

            atNewPos = !atNewPos;
            beatTimer = beatInterval;
        }
    }


    public void SetBeatInterval(int interval)
    {
        beatInterval = interval;
    }
}
