using UnityEngine;

namespace UndeadAssault
{
    [RequireComponent(typeof(AudioSource))]
    public class MeleeAttackPrimaryAbility : PrimaryAbility
    {
        public AudioClip[] fireAudioClips;
        public double damageMultiplier = 1.00;
        public float swingAngle = 90f;
        public override float cooldownFormula => cooldown / _stats.primaryCdr;
        public float cooldown = 0.55f;

        private double _cdTimeout;
        private Entity _entity;
        private Stats _stats;
        private AiComponent _aiComponent;
        private AudioSource _audioSource;

        void Start()
        {
            _entity = GetComponent<Entity>();
            _stats = _entity.stats;
            _aiComponent = GetComponent<AiComponent>();
            _audioSource = GetComponent<AudioSource>();
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
                PerformSwing();
                _cdTimeout += cooldownFormula;
            }
        }

        public void PerformSwing()
        {
            _casting = true;
            _aiComponent.animationPaused = true;
            _entity._animManager.FirePrimaryAttack();
            if (fireAudioClips.Length > 0)
            {
                this.AttachNTimer(0.3f, () =>
                {
                    _audioSource.PlayOneShot(fireAudioClips[Random.Range(0, fireAudioClips.Length)]);
                });
            }
            this.AttachNTimer(
                castTime / _stats.primaryCdr,
                () =>
                {
                    Utils
                        .GetEntitiesInCone(
                            transform.position,
                            transform.forward,
                            _stats.attackRange,
                            swingAngle,
                            filter => filter.tag != transform.tag
                        )
                        .ForEach(target =>
                        {
                            _entity.DealDamage(target, _stats.attack * damageMultiplier);
                        });
                    _aiComponent.animationPaused = false;
                    _casting = false;
                }
            );
        }
    }
}
