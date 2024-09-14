namespace UndeadAssault
{
    public class SkeletonMinionAi : AiComponent
    {
        public double damageMultiplier = 1.00;
        public override double cooldownFormula => 0.55 / _stats.primaryCdr;
        private double _cdTimeout;
        private Stats _stats;
        private AiComponent _defaultAi;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _defaultAi = GetComponent<AiComponent>();
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
