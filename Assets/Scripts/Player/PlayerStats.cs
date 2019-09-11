using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerStats : MonoBehaviour
{
    public bool isDead;
    public int hpMax = 400;
    public int hpCurrent;
    public float damageITime;
    private float damageTimer;

    // Start is called before the first frame update
    void Start()
    {
        hpCurrent = hpMax;
    }

    // Update is called once per frame
    void Update()
    {
        damageTimer = Mathf.MoveTowards(damageTimer, 0, Time.deltaTime);

        if (hpCurrent <= 0 && !isDead)
        {
            FindObjectOfType<SceneSwitcher>().SceneSwitch(SceneManager.GetActiveScene().name);
            isDead = true;
        }
    }

    public void Damage(int amount)
    {
        if (damageTimer <= 0)
        {
            hpCurrent -= amount;

            // Camera Shake
            Camera.main.transform.DOKill(true);
            Camera.main.transform.DOPunchRotation(new Vector3(-8f, 0.1f, -4.0f), 0.2f, 2, 1f);
            Camera.main.transform.DOPunchPosition(new Vector3(-0.05f, 0.05f, -0.1f), 1f, 2, 0.5f);

            damageTimer = damageITime;
        }



    }
}
