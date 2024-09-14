using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(DefaultAi))]
    public class SkeletonMinionPrimaryAbility : PrimaryAbility
    {
        public double damageMultiplier = 1.00;
        public override double cooldownFormula => 0.55 / _stats.primaryCdr;
        private double _cdTimeout;
        private Stats _stats;
        private DefaultAi _defaultAi;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _defaultAi = GetComponent<DefaultAi>();
        }

        void Update()
        {
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;

            if (_defaultAi.allowAttack)
                if (_cdTimeout <= 0)
                {
                    CastAbility();
                    _cdTimeout += cooldownFormula;
                }
        }

        public override void CastAbility()
        {
            _defaultAi.animationPaused = true;
            this.AttachNTimer(
                (float)cooldownFormula,
                () =>
                {
                    Debug.Log("Skeleton Minion finish attack");

                    _defaultAi.animationPaused = false;
                }
            );
        }
    }
}
