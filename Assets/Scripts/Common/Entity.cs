using UnityEngine;

namespace UndeadAssault
{
    public abstract class Entity : MonoBehaviour
    {
        public Stats stats = new Stats();

        public void DealDamage(Entity target, double damage)
        {
            target.stats.health -= damage;
            if (target.stats.health <= 0)
            {
                target.Die();
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}
