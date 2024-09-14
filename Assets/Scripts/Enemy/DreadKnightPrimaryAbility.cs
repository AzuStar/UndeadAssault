using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    public class DreadKnightPrimaryAbility : PrimaryAbility
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

            // if (_defaultAi.allowAttack)
            //     if (_cdTimeout <= 0)
            //     {
            //         CastAbility();
            //         _cdTimeout += cooldownFormula;
            //     }
        }

        public override void CastAbility(Entity target)
        {
            _aiComponent.animationPaused = true;
            this.AttachNTimer(
                cooldownFormula,
                () =>
                {
                    Debug.Log("Skeleton Minion finish attack");

                    _aiComponent.animationPaused = false;
                }
            );
        }
    }
}
