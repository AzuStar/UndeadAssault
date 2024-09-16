using System;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(Entity))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class AiComponent : MonoBehaviour
    {
        // public LineRenderer lineRenderer;
        public int pathIndex;
        public NavMeshPath path;
        // public bool destinationReached = false;

        public bool allowAttack => _navMeshAgent.isStopped;
        public bool animationPaused = false;
        public double attackRangeOffset = 0.99;

        public float turnSpeed = 10.0f;
        Vector3 velocity = new Vector3(0, 0, 0);

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
            path = new NavMeshPath();

            _navMeshAgent.speed = _stats.movementSpeed;
            _navMeshAgent.angularSpeed = _stats.angularSpeed;
            _navMeshAgent.velocity = _navMeshAgent.desiredVelocity;
            // _navMeshAgent.isStopped = true;
        }

        void Update()
        {
            if (_self.isDead)
            {
                _navMeshAgent.isStopped = true;
                // _navMeshAgent.updatePosition = false;
                // _navMeshAgent.updateRotation = false;
                return;
            }
            if (target == null)
                UpdateTarget();
            if (animationPaused || target == null)
                return;
            float distance = Vector3.Distance(transform.position, target.transform.position);
            StopWithinAttackRange(distance);
            CastSpells(distance);
            // DrawDebugPath();
            if (_seekTimeout > 0)
                _seekTimeout -= Time.deltaTime;

            if (_seekTimeout <= 0)
            {
                _seekTimeout = 0.05f;
                Seek();
            }
            MoveAgent();
        }

        // public void DrawDebugPath()
        // {
        //     if (path.corners.Length > 1)
        //     {
        //         lineRenderer.positionCount = path.corners.Length;
        //         lineRenderer.SetPositions(path.corners);
        //         foreach (var pos in path.corners)
        //         {
        //             Debug.DrawLine(pos, pos + new Vector3(0, 10, 0), Color.red);
        //         }

        //     }
        //     if (path.corners.Length < 2)
        //     {
        //         Debug.Log(path.corners.Length);
        //     }
        // }

        public void MoveAgent()
        {
            if (path.corners.Length < 2)
                return;

            pathIndex = Math.Min(pathIndex, path.corners.Length - 1);
            Vector3 nextWaypoint = path.corners[pathIndex];
            Vector3 targetDirection = nextWaypoint - transform.position;
            targetDirection.y = 0;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Time.deltaTime * turnSpeed, 0.0f).normalized;
            newDirection.y = 0;

            transform.rotation = Quaternion.LookRotation(newDirection);
            if (!_navMeshAgent.isStopped)
            {
                _navMeshAgent.Move(newDirection * _stats.movementSpeed * Time.deltaTime);
            }
            _self._animManager.SetLocomotionVector(0, _navMeshAgent.isStopped ? 0 : _stats.movementSpeed / 2.0f);
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
            }
            else if (distance >= _stats.attackRange)
            {
                _navMeshAgent.isStopped = false;
            }
        }

        public void Seek()
        {
            _navMeshAgent.CalculatePath(target.transform.position, path);
            pathIndex = 1;
        }
    }
}
