using System;
using System.Collections;
using Infrastructure.ECS.Systems;
using Leopotam.EcsLite;
using UnityEngine;

namespace Objects
{
   public class OnTriggerEvent : MonoBehaviour
   {
      public string _nameInCollision;
      public EcsPackedEntity entryEntity;
      public bool IsEnter;

      private void OnTriggerStay(Collider other)
      {
         if (other.TryGetComponent(out Projectile projectile) && !IsEnter)
         {
            IsEnter = true;
            entryEntity = projectile.PackedEntity;
            StartCoroutine(Delay());
            //Debug.Log("Collision with " + other.gameObject.name);
         }

         if (other != null)
         {
            _nameInCollision = other.gameObject.name;
         }
         else
         {
            _nameInCollision = "Empty";
         }
      }

      private IEnumerator Delay()
      {
         yield return new WaitForSeconds(0.2f);
         IsEnter = false;
      }
   }
}
