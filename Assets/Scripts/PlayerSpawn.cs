using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.75f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}
