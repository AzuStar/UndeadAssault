using UnityEngine;

namespace UndeadAssault
{
    public class FierySparkProjectile : Projectile
    {
        protected override void OnCollision(Entity target)
        {
            // if hit self or ally, do nothing
            if (owner == target || owner.tag == target.tag)
                return;

            double damage = owner.stats.attack * 1.00;
            owner.DealDamage(target, damage);
            Remove();
        }

        // void FixedUpdate()
        // {
        //     base.FixedUpdate();
        //     damageScale *= damageRecovery;
        // }
    }
}
