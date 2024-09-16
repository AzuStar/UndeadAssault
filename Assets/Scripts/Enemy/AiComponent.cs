using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    public class AiComponent : MonoBehaviour
    {
        // public LineRenderer lineRenderer;
        // public int pathIndex;
        // public NavMeshPath path;
        // public bool destinationReached = false;

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
            // path = new NavMeshPath();
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
            _navMeshAgent.angularSpeed = _stats.angularSpeed;
            _navMeshAgent.velocity = _navMeshAgent.desiredVelocity;
            // DrawDebugPath();
            if (_seekTimeout > 0)
                _seekTimeout -= Time.deltaTime;

            if (_seekTimeout <= 0)
            {
                _seekTimeout = 0.05f;
                Seek();
            }
            // MoveAgent();
            _self._animManager.SetLocomotionVector(0, _navMeshAgent.velocity.normalized.magnitude);
        }

        // public void DrawDebugPath()
        // {
        //     if (path.corners.Length > 1)
        //     {
        //         lineRenderer.positionCount = path.corners.Length;
        //         lineRenderer.SetPositions(path.corners);
        //     }
        // }

        // public void MoveAgent()
        // {
        //     if (destinationReached || path.corners.Length == 0)
        //         return;
        //     Vector3 nextWaypoint = path.corners[pathIndex];
        //     Vector3 direction = nextWaypoint - transform.position;
        //     direction.y = 0;
        //     float angle = Vector3.Angle(transform.forward, direction);
        //     Debug.Log(angle);
        //     if (angle >= 10)
        //     {
        //         RotateTowards(nextWaypoint);
        //     }
        //     else
        //     {
        //         _navMeshAgent.Move(direction.normalized * _stats.movementSpeed * Time.deltaTime);
        //         if (Vector3.Distance(transform.position, nextWaypoint) < 1f)
        //         {
        //             pathIndex++;
        //             if (pathIndex >= path.corners.Length)
        //             {
        //                 destinationReached = true;
        //             }
        //         }
        //     }
        // }

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
                RotateTowards(target.transform.position);
            }
            else if (distance >= _stats.attackRange)
            {
                _navMeshAgent.isStopped = false;
            }
        }

        public void RotateTowards(Vector3 position)
        {
            Vector3 direction = position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(direction),
                _stats.angularSpeed * Time.deltaTime
            );
        }

        public void Seek()
        {
            // destinationReached = false;
            // pathIndex = 0;
            // _navMeshAgent.CalculatePath(target.transform.position, path);
            _navMeshAgent.SetDestination(target.transform.position);
        }
    }
}
