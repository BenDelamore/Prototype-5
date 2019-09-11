using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerStats : MonoBehaviour
{
    public bool isDead;
    public int hpMax = 400;
    public int hpCurrent;
    private float damageTimer;

    public GameObject system;
    private GameObject heathTint;
    private CanvasGroup canvas;
    [SerializeField] private float delayBeforeRegen = 4f;
    [SerializeField] private float regenRate = 1f;
    [SerializeField] private int regenAmount = 10;
    private float regenTimer;

    private float regenFlowOver = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("System") == null)
        {
            Instantiate(system);
            Debug.Log("Added System to scene because it was not added manually");
        }

        hpCurrent = hpMax;
        heathTint = GameObject.Find("Health");
        canvas = heathTint.GetComponent<CanvasGroup>();
        //canvas.alpha = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer = Mathf.MoveTowards(damageTimer, 0, Time.deltaTime);

        if (hpCurrent <= 0 && !isDead)
        {
            var sceneman = FindObjectOfType<SceneSwitcher>();
            sceneman.isRespawning = true;
            sceneman.StartFade();
            isDead = true;
        }

        float aTint = 1f - ((float)hpCurrent / (float)hpMax);
        canvas.alpha = aTint;

        regenTimer = Mathf.MoveTowards(regenTimer, 0, Time.deltaTime);

        if (regenTimer <= 0)
        {
            regenFlowOver += Time.deltaTime;

            if (regenFlowOver >= regenRate)
            {
                hpCurrent += regenAmount;
            }
        }

        hpCurrent = Mathf.Clamp(hpCurrent, 0, hpMax);
    }

    public void Damage(int amount)
    {
    
        hpCurrent -= amount;

        // Camera Shake
        Camera.main.transform.DOKill(true);
        Camera.main.transform.DOPunchRotation(new Vector3(-8f, 0.1f, -4.0f), 0.2f, 2, 1f);
        Camera.main.transform.DOPunchPosition(new Vector3(-0.05f, 0.05f, -0.1f), 1f, 2, 0.5f);

        // damageTimer = damageITime;
        regenTimer = delayBeforeRegen;
    }
}
