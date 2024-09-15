using UnityEngine;

namespace UndeadAssault
{
    public class RangedAttackPrimaryAbility : PrimaryAbility
    {
        public double damageMultiplier = 1.00;
        public float swingAngle = 90f;
        public override float cooldownFormula => (float)(0.55f / _stats.primaryCdr);
        public HeadCastPoint headCastPoint;
        public Projectile projectile;

        private double _cdTimeout;
        private Stats _stats;
        private AiComponent _aiComponent;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            headCastPoint = GetComponentInChildren<HeadCastPoint>();
        }

        void Update()
        {
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0 && !_casting)
            {
                LaunchFireball();
            }
        }

        public void LaunchFireball()
        {
            _casting = true;
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Vector3 launchPoint =
                        headCastPoint == null
                            ? transform.position
                            : headCastPoint.transform.position;
                    Projectile proj = Instantiate(projectile, launchPoint, transform.rotation);
                    proj.owner = GetComponent<Entity>();
                    _cdTimeout += cooldownFormula;
                }
            );
        }
    }
}
