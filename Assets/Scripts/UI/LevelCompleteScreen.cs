using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelCompleteScreen : MonoBehaviour
{
    [SerializeField] private GameObject scriptPiggy;
    [SerializeField] private GameObject vicMenuPanel;
    private CanvasGroup canvas;
    private bool isActive;

    // Use this for initialization
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        canvas.alpha = 0;
        //pauseMenuPanel.transform.position = transformInitial;
    }

    // Update is called once per frame
    void Update()
    {

        if (GlobalData.Victory && !isActive)
        {
            ToggleMenu(1f);
            isActive = true;
        }
        if (!GlobalData.Victory && isActive)
        {
            ToggleMenu(0f);
            isActive = false;
        }

    }

    public void ToggleMenu(float alpha)
    {
        //float alpha = uiHandler.isPaused ? 1f : 0f;
        canvas.DOFade(alpha, 0.2f);
        canvas.interactable = GlobalData.Victory;
        canvas.blocksRaycasts = GlobalData.Victory;
    }
}
