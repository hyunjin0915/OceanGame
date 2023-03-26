using UnityEngine;

public class HealthDecreaseTriggerNew : MonoBehaviour
{
    [SerializeField, Tooltip("How much should the player's health decrease by when entering this trigger.")]
    private int healthDecreaseAmount = 10;
    [SerializeField]
    private HealthManagerScriptableObject healthManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            healthManager.DecreaseHealth(healthDecreaseAmount);
        }
    }
}
