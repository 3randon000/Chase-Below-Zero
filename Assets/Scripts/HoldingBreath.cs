using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealBreath : MonoBehaviour
{
    [Header("Breath Settings")]
    public float maxBreath = 10f;
    public float drainRate = 1f;
    public float refillRate = 3f;

    [Header("Lose Screen UI")]
    public GameObject loseScreen;

    private float currentBreath;
    private bool inWater = false;
    private bool inAirHole = false;
    private bool isDead = false;

    void Start()
    {
        currentBreath = maxBreath;
    }

    void Update()
    {
        if (isDead) return;

        if (inWater && !inAirHole)
            currentBreath -= drainRate * Time.deltaTime;
        else
            currentBreath += refillRate * Time.deltaTime;

        currentBreath = Mathf.Clamp(currentBreath, 0f, maxBreath);

        if (currentBreath <= 0f)
            Die();
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("You Drowned LOL");

        if (loseScreen != null)
            loseScreen.SetActive(true);

        SealMovement movement = GetComponent<SealMovement>();
        if (movement != null)
            movement.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("whatIsWater"))
            inWater = true;

        if (other.CompareTag("whatIsAirHole"))
            inAirHole = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("whatIsWater"))
            inWater = false;

        if (other.CompareTag("whatIsAirHole"))
            inAirHole = false;
    }
}