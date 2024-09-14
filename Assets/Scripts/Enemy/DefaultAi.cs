using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(Entity))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class DefaultAi : MonoBehaviour
    {
        public bool allowAttack => _navMeshAgent.isStopped;
        public bool animationPaused = false;
        public double attackRangeOffset = 0.99;
        public Entity target;
        private NavMeshAgent _navMeshAgent;
        private Stats _stats;
        private float _seekTimeout = 0;

        void Start()
        {
            _stats = GetComponent<Entity>().stats;
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update()
        {
            if (target == null)
                UpdateTarget();
            if (animationPaused)
                return;
            StopWithinAttackRange();

            _navMeshAgent.speed = (float)_stats.movementSpeed;
            if (_seekTimeout > 0)
                _seekTimeout -= Time.deltaTime;

            if (_seekTimeout <= 0)
            {
                _seekTimeout = 0.5f;
                Seek();
            }
        }

        public void UpdateTarget()
        {
            target = Gamemode.instance.hero;
        }

        public void StopWithinAttackRange()
        {
            if (target == null)
                return;
            float distance = Vector3.Distance(transform.position, target.transform.position);
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
