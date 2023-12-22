using Cysharp.Threading.Tasks;
using Settings.Weapons;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public interface IWeaponFactory
    {
        UniTask<GameObject> GetWeapon(WeaponType weaponType);
    }
}