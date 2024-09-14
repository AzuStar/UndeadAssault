using UnityEngine;

namespace UndeadAssault
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        public float speed = 20;
        public float speedScale = 0;
        public float speedRecovery = 0.8f;

        public SpecialEffect hitEffect;
        public SpecialEffect spawnEffect;

        public float lifetime = 3f;
        private float _lifetimetimout;

        public Entity owner;

        void Start()
        {
            _lifetimetimout = lifetime;
            if (spawnEffect != null)
            {
                Instantiate(spawnEffect, transform.position, transform.rotation);
            }
        }

        protected virtual void Update()
        {
            _lifetimetimout -= Time.deltaTime;
            if (_lifetimetimout <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void FixedUpdate()
        {
            transform.position +=
                transform.forward * speed * (1 + speedScale) * Time.fixedDeltaTime;
            speedScale *= speedRecovery;
        }

        public void SpawnParticles()
        {
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, transform.rotation);
        }

        // onCollisionEnter is called when the projectile collides with another object
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == GameConstants.TAG_ENVIRONMENT)
                OnTerrainCollision(other.ClosestPointOnBounds(transform.position));
            Entity et;
            et = other.GetComponent<Entity>();
            if (et != null)
            {
                if (owner == null)
                {
                    Remove();
                    return;
                }
                else
                {
                    OnCollision(et);
                }
            }
        }

        public void Remove()
        {
            SpawnParticles();
            Destroy(gameObject);
        }

        protected virtual void OnCollision(Entity target) { }

        protected virtual void OnTerrainCollision(Vector3 collisionPoint)
        {
            Remove();
        }
    }
}
