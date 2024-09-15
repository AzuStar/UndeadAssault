using UnityEngine;

namespace UndeadAssault
{
    public class FierySparkPrimaryAbility : PrimaryAbility
    {
        public Projectile sparkProjectile;

        public override float cooldownFormula => cooldown / _stats.primaryCdr;
        public float cooldown;

        private float _cdTimeout;
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
            RefreshText();
            if (_casting)
                return;
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public void RefreshText()
        {
            HudSkillTrackerSingletonGroup.instance.primarySkillTracker.SetCooldown(
                _cdTimeout,
                cooldownFormula
            );
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
            _animManager.FirePrimaryAttack();
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Vector3 launchPoint =
                        _headCastPoint == null
                            ? transform.position
                            : _headCastPoint.transform.position;
                    Projectile proj = Instantiate(sparkProjectile, launchPoint, transform.rotation);
                    proj.owner = GetComponent<Entity>();
                    _cdTimeout += cooldownFormula;
                    _casting = false;
                }
            );
        }
    }
}
