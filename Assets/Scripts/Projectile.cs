using UnityEngine;

namespace UndeadAssault
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        public float speed = 20;
        public float speedScale = 0;
        public float speedRecovery = 0.8f;

        public float lifetime = 3f;
        private float _lifetimetimout;

        void Start()
        {
            _lifetimetimout = lifetime;
        }

        void Update()
        {
            _lifetimetimout -= Time.deltaTime;
            if (_lifetimetimout <= 0)
            {
                Destroy(gameObject);
            }
        }

        void FixedUpdate()
        {
            transform.position +=
                transform.forward * speed * (1 + speedScale) * Time.fixedDeltaTime;
            speedScale *= speedRecovery;
        }
    }
}
