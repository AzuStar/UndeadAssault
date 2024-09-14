using UnityEngine;

namespace UndeadAssault
{
    public class MeteoriteSecondaryAbility : SecondaryAbility
    {
        public override double cooldownFormula => 0.5 / (1 + stats.primaryCdr);
        private double _cdTimeout;
        private Stats stats;

        void Start()
        {
            stats = GetComponent<Entity>().stats;
        }

        void Update()
        {
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;

            if (Input.GetMouseButton(1))
            {
                if (_cdTimeout <= 0)
                {
                    CastAbility();
                    _cdTimeout += cooldownFormula;
                }
            }
        }

        public override void CastAbility() { }
    }
}
