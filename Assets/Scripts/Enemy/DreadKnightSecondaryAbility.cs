using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    public class DreadKnightSecondaryAbility : SecondaryAbility
    {
        public float rangeMultiplier = 2.50f;
        public RangeIndicator rangeIndicator;
        public double damageMultiplier = 1.00;
        public override float cooldownFormula => cooldown / _stats.primaryCdr;
        public float cooldown = 2.5f;

        private double _cdTimeout;
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
            if (_cdTimeout <= 0)
            {
                PerformSpin();
            }
        }

        public void PerformSpin()
        {
            _aiComponent.animationPaused = true;
            _casting = true;
            RangeIndicator indicator = Instantiate(
                rangeIndicator,
                transform.position,
                Quaternion.identity,
                transform
            );
            indicator.radius = _stats.attackRange * rangeMultiplier;
            indicator.DrawSegments();
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Utils
                        .GetEntitiesInRadius(
                            transform.position,
                            _stats.attackRange * rangeMultiplier,
                            entity => entity.tag != transform.tag
                        )
                        .ForEach(entity =>
                        {
                            entity.DealDamage(entity, _stats.attack * damageMultiplier);
                        });
                    _cdTimeout += cooldownFormula;
                    _casting = false;
                    _aiComponent.animationPaused = false;
                }
            );
        }
    }
}
