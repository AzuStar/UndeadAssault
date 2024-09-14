using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.75f);
        Gizmos.DrawSphere(transform.position, 1);
        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
