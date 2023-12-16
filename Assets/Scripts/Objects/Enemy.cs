using System;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{
    public event Action OnDeath;

    [SerializeField] private EnemySettings _settings;
    [SerializeField] private float health;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        health = _settings.Health;
    }

    public void GetDamage(float projectileDamage)
    {
        health -= projectileDamage;

        if (health < 0)
        {
            OnDeath?.Invoke();
            Debug.Log("Death");
        }
    }
}