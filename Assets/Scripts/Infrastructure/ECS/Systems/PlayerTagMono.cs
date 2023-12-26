using Leopotam.EcsLite;
using UnityEngine;

namespace Infrastructure.ECS.Systems
{
    public class PlayerTagMono : MonoBehaviour
    {
        private EcsPackedEntity _entity;

        public EcsPackedEntity Entity => _entity;
        public void SetEntity(EcsPackedEntity packEntity)
        {
            _entity = packEntity;
        }
    }
}