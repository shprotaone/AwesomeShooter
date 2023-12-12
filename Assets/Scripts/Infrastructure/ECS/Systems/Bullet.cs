using UnityEngine;
using UnityEngine.Rendering;

namespace Infrastructure.ECS.Systems
{
    public class Bullet : MonoBehaviour
    {
        private ObjectPool<Bullet> _bulletPool;
        public void DestroyBullet()
        {
            Destroy(this.gameObject);
        }

        public void SetPool(ObjectPool<Bullet> pool) =>
            _bulletPool = pool;
    }
}