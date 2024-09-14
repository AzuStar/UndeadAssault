using UnityEngine;

namespace UndeadAssault
{
    public class MeleeAttackPrimaryAbility : PrimaryAbility
    {
        public double damageMultiplier = 1.00;
        public float swingAngle = 90f;
        public override float cooldownFormula => (float)(0.55f / _stats.primaryCdr);

        private double _cdTimeout;
        private Stats _stats;
        private AiComponent _aiComponent;
        private bool casting = false;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _aiComponent = GetComponent<AiComponent>();
        }

        void Update()
        {
            if (casting)
                return;
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0)
            {
                PerformSwing();
                _cdTimeout += cooldownFormula;
            }
        }

        public void PerformSwing()
        {
            casting = true;
            _aiComponent.animationPaused = true;
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Utils
                        .GetEntitiesInCone(
                            transform.position,
                            transform.forward,
                            swingAngle,
                            _stats.attackRange,
                            entity => entity.tag != transform.tag
                        )
                        .ForEach(entity =>
                        {
                            Debug.Log("Melee attack on " + entity.name);
                        });
                    _aiComponent.animationPaused = false;
                    casting = false;
                }
            );
        }
    }
}
