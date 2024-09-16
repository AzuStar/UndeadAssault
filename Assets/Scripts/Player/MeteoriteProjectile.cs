using UnityEngine;

namespace UndeadAssault
{
    public class MeteoriteProjectile : Projectile
    {
        public double damagePercent = 1.00;
        public Vector3 impactPoint;
        public float impactRadius = 5;

        protected override void FixedUpdate()
        {
            // always look at impact point
            transform.rotation = Quaternion.LookRotation(impactPoint - transform.position);
            transform.position +=
                transform.forward * speed * (1 + speedScale) * Time.fixedDeltaTime;
            speedScale *= speedRecovery;
            if (Vector3.Distance(transform.position, impactPoint) < 0.5f)
            {
                Explode();
            }
        }

        public void Explode()
        {
            double damage = owner.stats.attack * damagePercent;
            Utils
                .GetEntitiesInRadius(
                    transform.position,
                    impactRadius,
                    tar => owner != tar && owner.tag != tar.tag
                )
                .ForEach(target =>
                {
                    owner.DealDamage(target, damage);
                });

            Remove();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, impactRadius);
        }
    }
}
