using UnityEngine;

namespace UndeadAssault
{
    public class RangedAttackPrimaryAbility : PrimaryAbility
    {
        public float inaccuracy;
        public double damageMultiplier = 1.00;
        public override float cooldownFormula => cooldown / _stats.primaryCdr;
        public float cooldown = 0.55f;

        public GenericProjectile projectile;

        private double _cdTimeout;
        private Entity _entity;
        private Stats _stats;
        private AiComponent _aiComponent;
        private HeadCastPoint _headCastPoint;

        void Start()
        {
            _entity = GetComponent<Entity>();
            _stats = _entity.stats;
            _headCastPoint = GetComponentInChildren<HeadCastPoint>();
            _aiComponent = GetComponent<AiComponent>();
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
                LaunchProjectile(target);
            }
        }

        public void LaunchProjectile(Entity target)
        {
            _casting = true;
            _aiComponent.animationPaused = true;
            _entity._animManager.FirePrimaryAttack();
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Vector3 launchPoint =
                        _headCastPoint == null
                            ? transform.position
                            : _headCastPoint.transform.position;
                    Vector3 eulers = transform.rotation.eulerAngles;
                    eulers.y += Random.Range(-inaccuracy, inaccuracy);
                    GenericProjectile proj = Instantiate(
                        projectile,
                        launchPoint,
                        Quaternion.Euler(eulers)
                    );
                    proj.owner = GetComponent<Entity>();
                    proj.damagePercent = damageMultiplier;
                    _cdTimeout += cooldownFormula;
                    _casting = false;
                    _aiComponent.animationPaused = false;
                }
            );
        }
    }
}
