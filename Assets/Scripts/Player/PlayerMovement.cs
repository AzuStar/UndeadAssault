using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public DashAbility dashAbility;
        public Vector3 movementPush = Vector3.zero;
        public Vector3 movementNatural = Vector3.zero;

        private NavMeshAgent _navMeshAgent;
        private Stats _stats;
        private EntityAnimManager _animManager;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stats = GetComponent<Entity>().stats;
            // navMeshAgent.updatePosition = false;
            _animManager = GetComponent<EntityAnimManager>();
        }

        void Update()
        {
            float movementSpeed = (float)_stats.movementSpeed * Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
            {
                movementNatural.z = 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementNatural.z = -1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementNatural.x = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementNatural.x = 1;
            }
            if (movementNatural != Vector3.zero)
            {
                movementNatural.Normalize();

                if (Input.GetKey(KeyCode.Space))
                {
                    dashAbility.CastDash(movementNatural);
                }
            }

            _navMeshAgent.Move((movementNatural + movementPush) * movementSpeed);

            var offsetRotated =
                Quaternion.AngleAxis(transform.rotation.eulerAngles.y, -Vector3.up)
                * movementNatural.normalized;
            _animManager.SetLocomotionVector(offsetRotated.x, offsetRotated.z);

            movementNatural = Vector3.zero;
            movementPush = Vector3.zero;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 point = ray.GetPoint(distance);
                Vector3 direction = point - transform.position;
                direction.y = 0;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
