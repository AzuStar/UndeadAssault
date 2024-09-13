using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadAssault
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public NavMeshAgent navMeshAgent;
        public Stats stats;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            stats = GetComponent<Entity>().stats;
            // navMeshAgent.updatePosition = false;
        }

        void Update()
        {
            float movementSpeed = (float)(stats.movementSpeed / 100);
            Vector3 offsetPoint = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                offsetPoint.z += movementSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {
                offsetPoint.z -= movementSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                offsetPoint.x -= movementSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {
                offsetPoint.x += movementSpeed;
            }
            if (offsetPoint != Vector3.zero)
            {
                navMeshAgent.Move(offsetPoint);
            }
        }
    }
}
