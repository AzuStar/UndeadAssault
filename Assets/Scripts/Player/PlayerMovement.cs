using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
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
            Vector3 offsetPoint = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                offsetPoint.z += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                offsetPoint.z -= 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                offsetPoint.x -= 1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                offsetPoint.x += 1;
            }
            if (offsetPoint != Vector3.zero)
            {
                offsetPoint.Normalize();
                _navMeshAgent.Move(offsetPoint * movementSpeed);
            }
            var offsetRotated = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, -Vector3.up) * offsetPoint.normalized;
            _animManager.SetLocomotionVector(offsetRotated.x, offsetRotated.z);
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
