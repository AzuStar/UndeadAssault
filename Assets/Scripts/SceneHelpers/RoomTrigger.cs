using System.Collections.Generic;
using UndeadAssault;
using UnityEngine;
using UnityEngine.AI;

public class RoomTrigger : MonoBehaviour
{
    int _isInTheRoom = 0;
    int roomWeight = 0;
    bool roomInitialized = false;
    List<EnemySpawnPoint> _enemySpawnPoints = new List<EnemySpawnPoint>();
    private List<Entity> _roomEnemies = new List<Entity>();

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
                if (
                    collider.bounds.Contains(spawnPoint.transform.position)
                    && !_enemySpawnPoints.Contains(spawnPoint)
                )
                {
                    _enemySpawnPoints.Add(spawnPoint);
                }
            }
        }
    }

    public void Cleanup()
    {
        foreach (var enemy in _roomEnemies)
        {
            Destroy(enemy);
        }
    }

    void Update() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != GameConstants.TAG_PLAYER)
        {
            return;
        }
        _isInTheRoom++;
        Debug.Log("entered the room " + _isInTheRoom);
        if (!roomInitialized)
        {
            while (roomWeight < Gamemode.instance.floorWeight && _enemySpawnPoints.Count > 0 && Gamemode.instance.enemyTypes.Length > 0)
            {
                var enemyType = Gamemode.instance.enemyTypes[Random.Range(0, Gamemode.instance.enemyTypes.Length)];
                var spawn = _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Count)];
                var enemy = Instantiate(enemyType, spawn.transform.position, spawn.transform.rotation, transform);
                _roomEnemies.Add(enemy);
                roomWeight += enemy.weight;
            }
            roomInitialized = true;
        }
        else
        {
            foreach (var enemy in _roomEnemies)
            {
                enemy.GetComponent<AiComponent>().enabled = true;
                enemy.GetComponent<NavMeshAgent>().enabled = true;
                enemy.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != GameConstants.TAG_PLAYER)
        {
            return;
        }
        _isInTheRoom--;
        Debug.Log("left the room " + _isInTheRoom);

        if (_isInTheRoom == 0)
        {
            foreach (var enemy in _roomEnemies)
            {
                enemy.GetComponent<AiComponent>().enabled = false;
                enemy.GetComponent<NavMeshAgent>().enabled = false;
                enemy.GetComponent<EntityAnimManager>().SetLocomotionVector(0, 0);
                enemy.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }
}
