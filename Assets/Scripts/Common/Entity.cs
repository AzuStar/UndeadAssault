using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    public abstract class Entity : MonoBehaviour
    {
        public Stats stats = new Stats();
        public EntityAnimManager _animManager;
        public bool isDead = false;

        void Start()
        {
            _animManager = GetComponent<EntityAnimManager>();
        }

        protected virtual void Awake()
        {
            stats.health = stats.maxHealth;
        }

        public void DealDamage(Entity target, double damage)
        {
            target._animManager.PlayHit();
            target.stats.health -= damage;
            if (target.stats.health <= 0)
            {
                target.Die();
            }
        }

        public void Die()
        {
            isDead = true;
            if (_animManager != null)
            {
                _animManager.PlayDeath();
                var navMeshAgent = GetComponent<NavMeshAgent>();
                if (navMeshAgent)
                {
                    navMeshAgent.enabled = false;
                }
                var collider = GetComponent<CapsuleCollider>();
                if (collider)
                {
                    collider.enabled = false;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
