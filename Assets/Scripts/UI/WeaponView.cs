using System;
using Infrastructure.ECS.Components;
using Infrastructure.Services;
using Leopotam.EcsLite;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIComponents
{
    public class WeaponView : MonoBehaviour,IView
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _ammoCount;
        [SerializeField] private TMP_Text _maxAmmoCount;

        public void ChangeAmmoCount(int count)
        {
            _ammoCount.text = count.ToString();
        }

        public void SetMaxAmmoCount(int count)
        {
            _maxAmmoCount.text = count.ToString();
        }

        public void SetCurrentWeapon(WeaponComponent weaponComponent)
        {
            if (!weaponComponent.isEquipped)
            {
                _image.enabled = false;
            }
            else
            {
                SetMaxAmmoCount(weaponComponent.settings.magazineCapacity);

                _image.enabled = true;
            }
        }

        public void ResetView()
        {
            _image.enabled = false;
        }
    }
}