using Infrastructure.ECS.Systems;
using Leopotam.EcsLite;
using UnityEngine;

namespace Objects
{
   public class CollisionEvent : MonoBehaviour
   {
      public EcsPackedEntity entryEntity;
      public bool IsEnter;

      private void OnTriggerEnter(Collider other)
      {
         IsEnter = true;
         if (other.TryGetComponent(out Projectile projectile))
         {
            entryEntity = projectile.PackedEntity;
            Debug.Log("Collision with " + other.gameObject.name);
         }
         else if(other.TryGetComponent(out PlayerTagMono player))
         {
            entryEntity = player.Entity;
            Debug.Log("Collision with " + other.gameObject.name);
         }
      }

      private void OnTriggerExit(Collider other)
      {
         IsEnter = false;
      }
   }
}
