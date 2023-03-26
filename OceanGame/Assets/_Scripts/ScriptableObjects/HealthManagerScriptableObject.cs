using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName ="HealthManagerScriptableObject", menuName ="ScriptableObjects/Health Manager")]
public class HealthManagerScriptableObject : ScriptableObject
{
    public int health = 100;

    [SerializeField]
    private int maxHealth = 100;

    //people subscribe to this event to get notified of health changes
    [System.NonSerialized]
    public UnityEvent<int> healthChangeEvent;

    private void OnEnable()
    {
        health = maxHealth;
        if (healthChangeEvent == null)
            healthChangeEvent = new UnityEvent<int>();
    }

    public void DecreaseHealth(int amount)
    {
        health -= amount;
        healthChangeEvent.Invoke(health);
    }
}
