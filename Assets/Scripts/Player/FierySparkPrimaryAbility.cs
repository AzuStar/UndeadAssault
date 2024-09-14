using UnityEngine;

namespace UndeadAssault
{
    public class FierySparkPrimaryAbility : PrimaryAbility
    {
        public Projectile sparkProjectile;

        public override float cooldownFormula => (float)(0.5f / _stats.primaryCdr);
        private double _cdTimeout;
        private Stats _stats;
        private HeadCastPoint _headCastPoint;
        private EntityAnimManager _animManager;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _headCastPoint = GetComponentInChildren<HeadCastPoint>();
            _animManager = GetComponent<EntityAnimManager>();
        }

        void Update()
        {
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0)
            {
                LaunchFireball();
                _cdTimeout += cooldownFormula;
            }
        }

        public void LaunchFireball()
        {
            Vector3 launchPoint =
                _headCastPoint == null ? transform.position : _headCastPoint.transform.position;
            Projectile proj = Instantiate(sparkProjectile, launchPoint, transform.rotation);
            proj.owner = GetComponent<Entity>();
            _animManager.FirePrimaryAttack();
        }
    }
}
