using UnityEngine;

namespace UndeadAssault
{
    public class FierySparkPrimaryAbility : PrimaryAbility
    {
        public Projectile sparkProjectile;

        public override double cooldownFormula => 0.5 / (1 + _stats.primaryCdr);
        private double _cdTimeout;
        private Stats _stats;
        private HeadCastPoint _headCastPoint;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _headCastPoint = GetComponentInChildren<HeadCastPoint>();
        }

        void Update()
        {
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;

            if (Input.GetMouseButton(0))
            {
                if (_cdTimeout <= 0)
                {
                    CastAbility();
                    _cdTimeout += cooldownFormula;
                }
            }
        }

        public override void CastAbility()
        {
            Vector3 launchPoint =
                _headCastPoint == null ? transform.position : _headCastPoint.transform.position;
            Instantiate(sparkProjectile, launchPoint, transform.rotation);
        }
    }
}
