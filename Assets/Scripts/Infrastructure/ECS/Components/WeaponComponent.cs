using System;
using UnityEngine;

[Serializable]
public struct WeaponComponent
{
    public WeaponSettings settings;
    public Transform muzzle;
    public bool isEquipped;
}