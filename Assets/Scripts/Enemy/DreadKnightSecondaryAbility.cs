using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    public class DreadKnightSecondaryAbility : SecondaryAbility
    {
        public double damageMultiplier = 1.00;
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
                PerformSpin();
            }
        }

        public void PerformSpin()
        {
            _aiComponent.animationPaused = true;
            casting = true;
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Utils
                        .GetEntitiesInRadius(
                            transform.position,
                            _stats.attackRange,
                            entity => entity.tag != transform.tag
                        )
                        .ForEach(entity =>
                        {
                            Debug.Log("Dread Knight finish attack " + entity.name);
                        });
                    _cdTimeout += cooldownFormula;
                    casting = false;
                    _aiComponent.animationPaused = false;
                }
            );
        }
    }
}
