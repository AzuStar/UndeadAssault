using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(Entity))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class DefaultAi : MonoBehaviour
    {
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
            _navMeshAgent.speed = (float)_stats.movementSpeed;
            if (_seekTimeout > 0)
                _seekTimeout -= Time.deltaTime;

            if (_seekTimeout <= 0)
            {
                _seekTimeout = 0.5f;
                Seek();
            }
        }

        public void Seek()
        {
            _navMeshAgent.SetDestination(Gamemode.instance.hero.transform.position);
        }
    }
}
