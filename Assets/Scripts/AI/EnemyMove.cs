using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public bool doChase = false;

    [SerializeField] private float moveSpeed;
    private EnemySense sense;
    private Rigidbody rb;
    private float distance;

    void Start () {
        sense = GetComponent<EnemySense>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {

        distance = Vector3.Distance(transform.position, sense.lastPlayerLocation);
        rb.velocity *= 0;

        if (sense.playerDetected)
        {
            doChase = true;
        }
        else if (distance < 0.1f || !sense.playerDetected)
        {
            doChase = false;
        }

        Vector3 moveDirection = sense.lastPlayerLocation - transform.position;
        Quaternion target = Quaternion.LookRotation(moveDirection);

        if (doChase)
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, target, 0.2f);
            rb.MovePosition (transform.position + transform.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.rotation.y, transform.rotation.z), 0.15f);
            //transform.rotation.y = Quaternion.Lerp(transform.rotation.y, 0);
        }
    }
}
