using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthIncreaseTrigger : MonoBehaviour
{
    [SerializeField, Tooltip("How much should the player's health decrease by when entering this trigger.")]
    private int healthIncreaseAmount = 10;

    [SerializeField]
    private HealthManagerScriptableObject healthManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            healthManager.IncreaseHealth(healthIncreaseAmount);
            gameObject.SetActive(false);
        }
    }
}
