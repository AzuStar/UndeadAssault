using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(Entity))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AiComponent : MonoBehaviour
    {
        public bool allowAttack => _navMeshAgent.isStopped;
        public bool animationPaused = false;
        public double attackRangeOffset = 0.99;
        private Entity _self;
        public Entity target;
        private NavMeshAgent _navMeshAgent;
        private Stats _stats;
        private EntityAnimManager _animManager;
        private float _seekTimeout = 0;

        void Start()
        {
            _self = GetComponent<Entity>();
            _stats = _self.stats;
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animManager = GetComponent<EntityAnimManager>();
        }

        void Update()
        {
            if (_self.isDead)
            {
                return;
            }
            if (target == null)
                UpdateTarget();
            if (animationPaused)
                return;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            StopWithinAttackRange(distance);
            CastSpells(distance);

            _navMeshAgent.speed = _stats.movementSpeed;
            if (_seekTimeout > 0)
                _seekTimeout -= Time.deltaTime;

            if (_seekTimeout <= 0)
            {
                _seekTimeout = 0.5f;
                Seek();
            }
            _animManager.SetLocomotionVector(0, _navMeshAgent.velocity.normalized.magnitude);
        }

        public void UpdateTarget()
        {
            target = Gamemode.instance.hero;
        }

        public void CastSpells(float distance)
        {
            if (distance < _stats.attackRange)
            {
                if (animationPaused)
                    return;
                SecondaryAbility secondaryAbility = GetComponent<SecondaryAbility>();
                if (secondaryAbility != null)
                {
                    secondaryAbility.CastAbility(target);
                }
                if (animationPaused)
                    return;
                PrimaryAbility primaryAbility = GetComponent<PrimaryAbility>();
                if (primaryAbility != null)
                {
                    primaryAbility.CastAbility(target);
                    _animManager.FirePrimaryAttack();
                }
            }
        }

        public void StopWithinAttackRange(float distance)
        {
            if (target == null)
                return;
            if (distance < _stats.attackRange * attackRangeOffset)
            {
                _navMeshAgent.isStopped = true;
            }
            else
            {
                _navMeshAgent.isStopped = false;
            }
        }

        public void Seek()
        {
            if (target == null)
                return;
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }
}
