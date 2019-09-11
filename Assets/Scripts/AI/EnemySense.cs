using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySense : MonoBehaviour {

    public Transform eyeLocation;
    [SerializeField] private float detectionFOV;
    [SerializeField] private float detectionRange;
    [SerializeField] private float searchFOV;
    [SerializeField] private float searchRange;
    [SerializeField] private float searchTime;
    [SerializeField] private float detectionTime;
    public bool playerInSight = false;
    public bool playerDetected = false;
    [HideInInspector] public Vector3 currentPlayerLocation;
    public Vector3 lastPlayerLocation;
    public GameObject player;

    private SphereCollider sCol;
    private Vector3 previousPlayerLocation;
    private float currentRange;
    private float currentFOV;
    private Vector3 playerDirection;
    private float timeInSight;
    private float timeOutSight;

	// Use this for initialization
	void Awake () {
        sCol = GetComponent<SphereCollider>();
        sCol.radius = detectionRange;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentPlayerLocation = player.transform.position;

        // Check if player is in line of sight, then increment time in sight
        if (playerInSight)
        {
            lastPlayerLocation = player.transform.position;
            timeInSight += Time.deltaTime;
            timeOutSight = 0;
        }
        else
        {
            timeOutSight += Time.deltaTime;
            timeInSight = 0;
        }

        if (timeInSight >= detectionTime)
        {
            playerDetected = true;
        }

        // Stop searching after a certain amount of time
        if (timeOutSight >= searchTime)
        {
            playerDetected = false;
        }

        currentRange = playerDetected ? searchRange : detectionRange;
        currentFOV = playerDetected ? searchFOV : detectionFOV;
        sCol.radius = currentRange;
    }

    private void OnTriggerStay(Collider other)
    {   // Check if player is in range
        if (other.gameObject == player)
        {
            playerInSight = false;
            playerDirection = other.transform.position - transform.position;
            float angle = Vector3.Angle(playerDirection, transform.forward);

            // Check if player is in FOV
            if (angle < currentFOV * 0.5f)
            {
                RaycastHit hit;

                if (Physics.Raycast(eyeLocation.transform.position, playerDirection.normalized, out hit, currentRange))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInSight = false;
        }
    }
}
