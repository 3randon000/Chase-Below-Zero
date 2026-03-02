using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundFamily : MonoBehaviour
{
    [Header("Win Screen UI")]
    public GameObject winScreen;

    private bool foundFam = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (foundFam) return;

        if (collision.gameObject.CompareTag("Family"))
        {
            Found();
        }
    }

    private void Found()
    {
        foundFam = true;
        Debug.Log("You made it back to your family!");

        if (winScreen != null)
            winScreen.SetActive(true);

        SealMovement movement = GetComponent<SealMovement>();
        if (movement != null)
            movement.enabled = false;
    }
}
