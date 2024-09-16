using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [Range(2, 20)]
    public float spawnOffset = 2f;

    public Vector3 GetSpawnPosition()
    {
        return transform.position + Random.insideUnitSphere * spawnOffset;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.75f);
        Gizmos.DrawSphere(transform.position, spawnOffset);
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, spawnOffset);
    }
}
