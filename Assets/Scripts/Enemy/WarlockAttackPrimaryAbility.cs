using UnityEngine;

namespace UndeadAssault
{
    public class WarlockAttackPrimaryAbility : PrimaryAbility
    {
        public SpecialEffect rangeIndicator;
        public float xOffset = 5f;
        public float heightOffset = 10f;
        public MeteoriteProjectile meteoriteProjectile;
        public override float cooldownFormula =>
            cooldown
            / _stats.primaryCdr
            * (1 + Random.Range(-cooldownFluctuation, cooldownFluctuation));
        public float cooldown = 3.5f;
        public float offsetFromTarget;
        public float cooldownFluctuation = 0.1f;

        private float _cdTimeout;
        private Stats _stats;
        private AiComponent _aiComponent;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
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
                LaunchCurse(target);
            }
        }

        public void LaunchCurse(Entity target)
        {
            _casting = true;
            _aiComponent.animationPaused = true;
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Vector3 impactPoint =
                        target.transform.position
                        + new Vector3(
                            Random.Range(-offsetFromTarget, offsetFromTarget),
                            0,
                            Random.Range(-offsetFromTarget, offsetFromTarget)
                        );
                    float sign = Random.value > 0.5f ? 1 : -1;
                    Vector3 spawnPoint = impactPoint + new Vector3(xOffset * sign, heightOffset, 0);

                    MeteoriteProjectile proj = Instantiate(
                        meteoriteProjectile,
                        spawnPoint,
                        transform.rotation
                    );
                    proj.impactPoint = impactPoint;
                    proj.owner = GetComponent<Entity>();
                    Instantiate(
                        rangeIndicator,
                        impactPoint + new Vector3(0, 0.1f, 0),
                        Quaternion.identity
                    );

                    _cdTimeout += cooldownFormula;
                    _casting = false;
                    _aiComponent.animationPaused = false;
                }
            );
        }
    }
}
