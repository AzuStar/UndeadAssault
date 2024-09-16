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
        private float _seekTimeout = 0;

        void Start()
        {
            _self = GetComponent<Entity>();
            _stats = _self.stats;
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (_self.isDead)
            {
                _navMeshAgent.isStopped = true;
                _navMeshAgent.updatePosition = false;
                _navMeshAgent.updateRotation = false;
                return;
            }
            if (target == null)
                UpdateTarget();
            if (animationPaused || target == null)
                return;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            StopWithinAttackRange(distance);
            CastSpells(distance);

            _navMeshAgent.speed = _stats.movementSpeed;
            _navMeshAgent.velocity =
                _navMeshAgent.desiredVelocity.normalized * _stats.movementSpeed;
            if (_seekTimeout > 0)
                _seekTimeout -= Time.deltaTime;

            if (_seekTimeout <= 0)
            {
                _seekTimeout = 0.5f;
                Seek();
            }
            _self._animManager.SetLocomotionVector(0, _navMeshAgent.velocity.normalized.magnitude);
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
                }
            }
        }

        public void StopWithinAttackRange(float distance)
        {
            if (distance < _stats.attackRange * attackRangeOffset)
            {
                _navMeshAgent.isStopped = true;
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    Quaternion.LookRotation(target.transform.position - transform.position),
                    _navMeshAgent.angularSpeed * Time.deltaTime * Random.Range(1f, 10f)
                );
            }
            else
            {
                _navMeshAgent.isStopped = false;
            }
        }

        public void Seek()
        {
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }
}
