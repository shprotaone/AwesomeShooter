using System.Collections;
using System.Collections.Generic;
using Infrastructure.ECS.Systems;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/BulletSettings")]
public class BulletSettings : ScriptableObject
{
    public BulletType bulletType;
    public float projectileSpeed;
    public float damage;
    public float lifetime;
}
