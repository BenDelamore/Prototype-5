﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyMove : MonoBehaviour {

    public bool doChase = false;
    private bool doLook = false;

    [SerializeField] private float moveSpeed;

    public Animator ani;
    public bool isAttacking;
    public bool doDamage;
    public bool attackDistance;
    [SerializeField] private float attackStartup = 0.2f;
    [SerializeField] private float attackLength = 0.2f;
    [SerializeField] private float timeBetweenAttacks = 1.0f;
    public float delayTimer;
    public float attackTimer;

    private EnemySense sense;
    private PlayerStats player;
    private Rigidbody rb;
    private float distance;

    void Start () {
        player = FindObjectOfType<PlayerStats>();
        sense = GetComponent<EnemySense>();
        rb = GetComponent<Rigidbody>();
        delayTimer = timeBetweenAttacks;
	}
	
	void Update () {

        distance = Vector3.Distance(transform.position, sense.lastPlayerLocation);
        rb.velocity *= 0;

        // Act based on detection level
        if (sense.playerDetected)
        {  
            doChase = true;
            doLook = true;
            
        }
        else if (distance < 0.1f)
        {
            doChase = false;
            doLook = false;
        }

        if (sense.playerInSight && distance < 2f)
        {
            doChase = false;
        }

        delayTimer = Mathf.MoveTowards(delayTimer, 0, Time.deltaTime);

        // Attacking logic
        if (doLook && distance < 2.5f && !isAttacking && delayTimer <= 0)
        {
            Attack();
            delayTimer = timeBetweenAttacks;
        }

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > attackStartup)
            {
                doDamage = true;
            }
            if (attackTimer > attackStartup + attackLength)
            {
                isAttacking = false;
                doDamage = false;
                attackTimer = 0;
            }
        }

        // Chase player
        Vector3 moveDirection = sense.lastPlayerLocation - transform.position;
        Quaternion target = Quaternion.LookRotation(moveDirection);

        if (doChase)
        {
            rb.MovePosition (transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            
        }

        if (doLook)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, target, 0.2f);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, transform.rotation.z), 0.15f);
        }
    }

    public void Attack()
    {
        isAttacking = true;
        player.Damage(10);
        //ani.SetTrigger("Attack");
    }
}
