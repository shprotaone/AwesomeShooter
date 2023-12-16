using Infrastructure.ECS.Systems;
using Leopotam.EcsLite;
using UnityEngine;

public class CollisionEvent : MonoBehaviour
{
   public EcsPackedEntity entryEntity;
   public bool IsEnter;

   private void OnTriggerEnter(Collider other)
   {
      IsEnter = true;
      entryEntity = other.GetComponent<Projectile>().PackedEntity;
      Debug.Log("Collision with " + other.gameObject.name);
   }

   private void OnTriggerExit(Collider other)
   {
      IsEnter = false;
   }
}
