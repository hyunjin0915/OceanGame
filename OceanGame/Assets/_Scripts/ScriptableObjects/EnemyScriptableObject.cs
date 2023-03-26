using UnityEngine;

[CreateAssetMenu(fileName ="EnemyScriptableObject",menuName ="ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public int health = 100;
    public float speed = 5f;

    public EnemyAttackScriptableObject enemyAttackType;
}
