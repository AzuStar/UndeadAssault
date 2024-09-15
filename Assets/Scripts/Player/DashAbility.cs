using UnityEngine;

namespace UndeadAssault
{
    public class DashAbility : CastableAbility
    {
        public float dashDistance = 10f;
        public override float cooldownFormula => 1.25f;

        private float _cdTimeout;
        private Stats _stats;
        private PlayerMovement _playerMovement;
        private Vector3 _dashDirection;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _playerMovement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            RefreshText();
            if (_casting)
            {
                _playerMovement.movementPush = _dashDirection * dashDistance * castTime;
                return;
            }
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public void RefreshText()
        {
            HudSkillTrackerSingletonGroup.instance.dashSkillTracker.SetCooldown(
                _cdTimeout,
                cooldownFormula
            );
        }

        public void CastDash(Vector3 dashDirection)
        {
            if (_cdTimeout <= 0 && !_casting)
                PerformDash(dashDirection);
        }

        public void PerformDash(Vector3 dashDirection)
        {
            _cdTimeout += cooldownFormula;
            _dashDirection = dashDirection;
            _casting = true;
            this.AttachNTimer(
                castTime,
                () =>
                {
                    _casting = false;
                }
            );
        }
    }
}
