using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OrcaDeviation : MonoBehaviour
{
    [Header("Hunting")]
    public Transform seal;
    public float detectionRange = 20f;
    public float chaseSpeed = 10f;
    public float lingerTime = 3f;

    private SealMovement sealMovement;
    private NavMeshAgent agent;
    private Orca patrolScript;

    private bool chasing = false;
    private bool searching = false;
    private float searchTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolScript = GetComponent<Orca>();
        sealMovement = seal.GetComponent<SealMovement>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, seal.position);

        if (!chasing && !searching)
        {
            if (distance <= detectionRange && !sealMovement.IsHidden)
            {
                chasing = true;
                patrolScript.enabled = false;  
                agent.ResetPath();           
            }
        }

        if (chasing)
        {
            if (sealMovement.IsHidden)
            {
                chasing = false;
                searching = true;
                searchTimer = lingerTime;
                agent.ResetPath();
                return;
            }

            agent.speed = chaseSpeed;
            agent.acceleration = 50f;
            agent.angularSpeed = 720f;
            agent.stoppingDistance = 0f;
            agent.autoBraking = false;

            Vector3 dir = (seal.position - transform.position).normalized;
            Vector3 attackPoint = seal.position + dir * 1.5f;

            agent.SetDestination(attackPoint);
        }
        if (searching)
        {
            searchTimer -= Time.deltaTime;

            if (searchTimer <= 0f)
            {
                searching = false;
                agent.ResetPath();
                patrolScript.enabled = true;
            }
        }
    }
}