using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour {

    public bool doChase = false;
    private bool doLook = false;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float followRange = 15f;

    public Animator ani;
    public bool isAttacking;
    public bool doDamage;
    [SerializeField] private float attackStartup = 0.2f;
    [SerializeField] private float attackLength = 0.2f;
    [SerializeField] private float timeBetweenAttacks = 1.0f;
    private float delayTimer;
    private float attackTimer;
    private float attackAniPlayed;

    public GameObject weaponObject;
    private EnemyWeapon weapon;
    private EnemySense sense;
    private PlayerStats player;
    private Rigidbody rb;
    private float distance;
    private Vector3 posInitial;
    private float displacement;
    private bool hasReturned = false;
    private float timeAtBase;

    void Start () {
        player = FindObjectOfType<PlayerStats>();
        sense = GetComponent<EnemySense>();
        rb = GetComponent<Rigidbody>();
        delayTimer = timeBetweenAttacks;
        weapon = weaponObject.GetComponent<EnemyWeapon>();
        ani = transform.Find("Crystal Enemy").GetComponent<Animator>();
        posInitial = transform.position;
	}
	
	void Update () {

        distance = Vector3.Distance(transform.position, sense.currentPlayerLocation);
        displacement = Vector3.Distance(transform.position, posInitial);

        rb.velocity *= 0;

        // Act based on detection level
        if (sense.playerDetected && displacement < followRange && !player.isDead)
        {
            if (distance > 0.1f)
            {
                Chase();
                doChase = true;
                doLook = true;

                if ((sense.playerInSight && distance < 2f))
                {
                    doChase = false;
                }
            }
            else
            {
                doChase = false;
                doLook = false;
            }

            GlobalData.CombatMode = true;
        }
        else // Return to spawnpoint if player is out of range
        {
            doChase = false;
            doLook = false;
            hasReturned = true;
            ReturnToBase();
            sense.playerDetected = false;

            GlobalData.CombatMode = false;
        }

        if (player.isDead)
        {
            sense.timeInSight = 0;
        }


        // Attacking logic
        delayTimer = Mathf.MoveTowards(delayTimer, 0, Time.deltaTime);

        if (doLook && (distance < 3.5f && delayTimer <= 0f))
        {
            Attack();
            isAttacking = true;
            delayTimer = timeBetweenAttacks;
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackStartup && sense.playerDetected && distance <= 2.5f)
            {
                doDamage = true;
            }
            else
            {
                doDamage = false;
            }

            if (attackTimer > attackStartup + attackLength)
            {
                isAttacking = false;
                doDamage = false;
                attackTimer = 0;
            }
        }

        // Update enemy weapon collider
        weaponObject.SetActive(doDamage);
    }

    private void Chase()
    {
        // Chase player
        Vector3 moveDirection = sense.lastPlayerLocation - transform.position;
        Vector3 returnDirection = posInitial - transform.position;
        Quaternion target = Quaternion.LookRotation(moveDirection);

        if (doChase)
        {
            rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }

        if (doLook)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 7f);
        }
        else
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, transform.rotation.z), 0.15f);
        }
    }

    private void ReturnToBase()
    {
        Vector3 moveDirection = sense.lastPlayerLocation - transform.position;
        Vector3 returnDirection = posInitial - transform.position;
        Quaternion target = Quaternion.LookRotation(returnDirection);

        if (displacement > 2f)
        {
            rb.MovePosition(transform.position + transform.forward * moveSpeed/2f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * 3f);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, transform.rotation.z), Time.deltaTime * 1f);
        }

    }


    public void Attack()
    {

        //player.Damage(10);
        ani.SetTrigger("IsAttacking");
    }
}
