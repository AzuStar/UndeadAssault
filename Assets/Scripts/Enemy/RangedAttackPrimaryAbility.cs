using UnityEngine;

namespace UndeadAssault
{
    public class RangedAttackPrimaryAbility : PrimaryAbility
    {
        public double damageMultiplier = 1.00;
        public override float cooldownFormula => cooldown / _stats.primaryCdr;
        public float cooldown = 0.55f;

        public GenericProjectile projectile;

        private double _cdTimeout;
        private Stats _stats;
        private AiComponent _aiComponent;
        private HeadCastPoint _headCastPoint;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _headCastPoint = GetComponentInChildren<HeadCastPoint>();
        }

        void Update()
        {
            if (_casting)
                return;
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0 && !_casting)
            {
                LaunchProjectile();
            }
        }

        public void LaunchProjectile()
        {
            _casting = true;
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Vector3 launchPoint =
                        _headCastPoint == null
                            ? transform.position
                            : _headCastPoint.transform.position;
                    GenericProjectile proj = Instantiate(
                        projectile,
                        launchPoint,
                        transform.rotation
                    );
                    proj.owner = GetComponent<Entity>();
                    proj.damagePercent = damageMultiplier;
                    _cdTimeout += cooldownFormula;
                    _casting = false;
                }
            );
        }
    }
}
