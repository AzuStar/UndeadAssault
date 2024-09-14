using UnityEngine;

namespace UndeadAssault
{
    public class MeteoriteSecondaryAbility : SecondaryAbility
    {
        public override float cooldownFormula => (float)(0.5f / stats.secondaryCdr);
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
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0)
            {
                Debug.Log("MeteoriteSecondaryAbility");
                _cdTimeout += cooldownFormula;
            }
        }
    }
}
