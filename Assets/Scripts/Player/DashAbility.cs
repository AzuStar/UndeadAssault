using UnityEngine;

namespace UndeadAssault
{
    public class DashAbility : CastableAbility
    {
        public override float cooldownFormula => 1.25f;

        private float _cdTimeout;
        private Stats _stats;
        private PlayerMovement _playerMovement;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _playerMovement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0 && !_casting)
            {
                PerformDash();
                _cdTimeout += cooldownFormula;
            }
        }

        public void PerformDash()
        {
            this.AttachNTimer(
                1,
                () =>
                {
                    this.transform.position += this.transform.forward * 0.1f;
                }
            );
        }
    }
}
