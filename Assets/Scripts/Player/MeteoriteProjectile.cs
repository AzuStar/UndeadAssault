using UnityEngine;

namespace UndeadAssault
{
    public class MeteoriteProjectile : Projectile
    {
        public double damagePercent = 1.00;
        public Vector3 impactPoint;

        protected override void OnTerrainCollision(Vector3 collisionPoint)
        {
            double damage = owner.stats.attack * damagePercent;
            Utils
                .GetEntitiesInRadius(collisionPoint, 5, tar => owner == tar || owner.tag == tar.tag)
                .ForEach(target =>
                {
                    owner.DealDamage(target, damage);
                });

            Remove();
        }

        protected override void FixedUpdate()
        {
            // always look at impact point
            transform.rotation = Quaternion.LookRotation(impactPoint - transform.position);
            transform.position +=
                transform.forward * speed * (1 + speedScale) * Time.fixedDeltaTime;
            speedScale *= speedRecovery;
        }
    }
}
