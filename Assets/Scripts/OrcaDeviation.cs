using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaDeviation : MonoBehaviour
{
    [Header("Hunting Prey")]
    public Transform seal;
    public float detectionRange = 20f;

    [Header("Hunting Speed")]
    public float chaseSpeed = 8f;

    [Header("Hunting...wait where did you go?")]
    public float lingerTime = 4f;

    private SealMovement sealMovement;
    private Path patrol;

    private bool chasing = false;
    private bool searching = false;
    private float searchTimer;

    void Start()
    {
        sealMovement = seal.GetComponent<SealMovement>();
        patrol = GetComponent<Path>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, seal.position);

        // PATROL STATE
        if (!chasing && !searching)
        {
            if (distance <= detectionRange && !sealMovement.IsHidden)
            {
                chasing = true;
                patrol.enabled = false; // stop normal path
            }
        }

        // CHASE STATE
        if (chasing)
        {
            if (sealMovement.IsHidden)
            {
                chasing = false;
                searching = true;
                searchTimer = lingerTime;
            }
            else
            {
                MoveTowards(seal.position, chaseSpeed);
            }
        }

        // SEARCH STATE
        if (searching)
        {
            searchTimer -= Time.deltaTime;

            if (searchTimer <= 0f)
            {
                searching = false;
                patrol.enabled = true; // smoothly resumes path
            }
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );

        transform.LookAt(target);
    }
}