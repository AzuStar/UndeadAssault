using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    int _isInTheRoom = 0;
    List<EnemySpawnPoint> _enemySpawnPoints = new List<EnemySpawnPoint>();
    void OnDrawGizmos()
    {
        foreach (var collider in GetComponents<BoxCollider>())
        {
            Gizmos.color = new Color(0, 0, 1, 0.15f);
            Gizmos.DrawCube(collider.transform.position + collider.center, collider.size);
            Gizmos.color = new Color(0, 0, 1, 0.5f);
            Gizmos.DrawWireCube(collider.transform.position + collider.center, collider.size);
        }
    }

    void Start()
    {
        var enemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        foreach (var spawnPoint in enemySpawnPoints)
        {
            foreach (var collider in GetComponents<BoxCollider>())
            {
                if (collider.bounds.Contains(spawnPoint.transform.position) && !_enemySpawnPoints.Contains(spawnPoint))
                {
                    _enemySpawnPoints.Add(spawnPoint);
                }
            }
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        _isInTheRoom++;
        if (_isInTheRoom == 1)
        {
            // TODO use _enemySpawnPoints here
        }
    }

    void OnTriggerExit(Collider other)
    {
        _isInTheRoom--;
        if (_isInTheRoom == 0)
        {
            // TODO disable enemies/despawn here
        }
    }
}
