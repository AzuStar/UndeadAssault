namespace UndeadAssault
{
    public class GenericProjectile : Projectile
    {
        public double damagePercent = 1.00;

        protected override void OnCollision(Entity target)
        {
            // if hit self or ally, do nothing
            if (owner == target || owner.tag == target.tag)
                return;

            double damage = owner.stats.attack * damagePercent;
            owner.DealDamage(target, damage);
            Remove();
        }
    }
}
