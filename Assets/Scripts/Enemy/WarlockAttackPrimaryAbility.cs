using UnityEngine;

namespace UndeadAssault
{
    public class WarlockAttackPrimaryAbility : PrimaryAbility
    {
        public override float cooldownFormula => cooldown / _stats.primaryCdr;
        public float cooldown = 3.5f;

        private float _cdTimeout;
        private Stats _stats;
        private AiComponent _aiComponent;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _aiComponent = GetComponent<AiComponent>();
        }

        void Update()
        {
            if (_casting)
                return;
            if (_cdTimeout > 0)
                _cdTimeout -= Time.deltaTime;
        }

        public override void CastAbility(Entity target)
        {
            if (_cdTimeout <= 0 && !_casting)
            {
                LaunchCurse();
            }
        }

        public void LaunchCurse()
        {
            _casting = true;
            _aiComponent.animationPaused = true;
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    // GameObject curseground = Instantiate(
                    //     _stats.curseground,
                    //     transform.position,
                    //     transform.rotation
                    // );
                    // curseground.GetComponent<Curseground>().owner = GetComponent<Entity>();
                    _cdTimeout += cooldownFormula;
                    _casting = false;
                    _aiComponent.animationPaused = false;
                }
            );
        }
    }
}
