using UnityEngine;

[CreateAssetMenu(menuName = "Settings/WeaponSettings")]
public class WeaponSettings : ScriptableObject
{
    public WeaponType WeaponType;
    public float fireRate;
    public int magazineCapacity;
}