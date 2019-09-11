using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneSwitcher : MonoBehaviour {

    [SerializeField] private GameObject fadePanel;
    [SerializeField] private float fadeTime;
    private Image fadeImage;
    public string targetScene;
    public string curScene;
    public bool isFading;
    public bool isSwitching;
    public float fadeTimeCur;

    void Awake()
    {
        //fadePanel = GameObject.Find("fadePanel");
    }

	// Use this for initialization
	void Start ()
    {

        curScene = SceneManager.GetActiveScene().name;
        Debug.Log(curScene);
        if (curScene != "MainMenu")
        {
            GlobalData.LastScene = curScene;

        }

        

        isFading = true;
        fadeTimeCur = fadeTime;
        fadePanel.SetActive(true);
        fadeImage = fadePanel.GetComponent<Image>();
        Vector4 initialColor = fadeImage.color;
        fadeImage.DOFade(0, fadeTime).SetEase(Ease.InOutSine);
    }
	
	// Update is called once per frame
	void Update ()
    {
        fadeTimeCur = Mathf.MoveTowards(fadeTimeCur, 0f, Time.unscaledDeltaTime);
        if (fadeTimeCur != 0)
        {
            isFading = true;
        }
        else
        {
            isFading = false;
        }

        if (isSwitching && !isFading && targetScene != null)
        {

            SceneManager.LoadScene(targetScene);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneSwitch("Ryan - Dev");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneSwitch("Ben-Dev");
        }

    }

    public void SceneSwitch(string scene)
    {
        if (!isSwitching)
        {
            
            
            Vector4 initialColor = fadeImage.color;
            fadeImage.DOFade(1, fadeTime / 1.5f).SetEase(Ease.InOutSine);
            fadeTimeCur = fadeTime;
            targetScene = scene;
            isSwitching = true;
        }
    }


    public void StartFade()
    {
        if (!isSwitching)
        {
            Vector4 initialColor = fadeImage.color;
            fadeImage.DOFade(1, fadeTime / 1.5f).SetEase(Ease.InOutSine);
        }
        isSwitching = true;
        fadeTimeCur = fadeTime;
    }

    public void EnterMainMenu()
    {
        StartFade();
        targetScene = "MainMenu";
    }
}
