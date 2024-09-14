using UnityEngine;

namespace UndeadAssault
{
    public class SkeletonMinionPrimaryAbility : PrimaryAbility
    {
        public double damageMultiplier = 1.00;
        public override float cooldownFormula => (float)(0.55f / _stats.primaryCdr);

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
            _aiComponent.animationPaused = true;
            this.AttachNTimer(
                (float)cooldownFormula,
                () =>
                {
                    Debug.Log("Skeleton Minion finish attack");
                    _aiComponent.animationPaused = false;
                }
            );
        }
    }
}
