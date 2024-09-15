using System.Collections.Generic;
using UndeadAssault;
using UnityEngine;
using UnityEngine.AI;

public class RoomTrigger : MonoBehaviour
{
    int _isInTheRoom = 0;
    bool roomInitialized = false;
    List<EnemySpawnPoint> _enemySpawnPoints = new List<EnemySpawnPoint>();
    public GameObject[] enemyPrefabs;
    private List<GameObject> _roomEnemies = new List<GameObject>();
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
        Debug.Log("Started sceneloader" + _enemySpawnPoints.Count);
    }

    public void Cleanup()
    {
        foreach (var enemy in _roomEnemies)
        {
            Destroy(enemy);
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Mage")
        {
            return;
        }
        _isInTheRoom++;
        if (!roomInitialized)
        {
            foreach (var spawn in _enemySpawnPoints)
            {
                if (enemyPrefabs.Length > 0)
                {
                    _roomEnemies.Add(Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawn.transform.position, spawn.transform.rotation));
                }
            }
            roomInitialized = true;
        }
        else
        {
            foreach (var enemy in _roomEnemies)
            {
                enemy.GetComponent<AiComponent>().enabled = true;
                enemy.GetComponent<CapsuleCollider>().enabled = true;
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name != "Mage")
        {
            return;
        }
        _isInTheRoom--;
        if (_isInTheRoom == 0)
        {
            foreach (var enemy in _roomEnemies)
            {
                enemy.GetComponent<AiComponent>().enabled = false;
                enemy.GetComponent<NavMeshAgent>().isStopped = true;
                enemy.GetComponent<EntityAnimManager>().SetLocomotionVector(0, 0);
                enemy.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }
}
