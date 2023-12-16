using UnityEngine;

[CreateAssetMenu(menuName = "Settings/EnemySettings")]
public class EnemySettings: ScriptableObject
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private float _health;

    public float Health => _health;
}