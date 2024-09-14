using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private SceneLoader _sceneLoader;
    void OnDrawGizmos()
    {
        foreach (var collider in GetComponents<BoxCollider>())
        {
            Gizmos.color = new Color(0, 1, 0, 0.75f);
            Gizmos.DrawCube(collider.transform.position + collider.center, collider.size);
            Gizmos.color = new Color(0, 1, 0, 1);
            Gizmos.DrawWireCube(collider.transform.position + collider.center, collider.size);
        }
    }

    void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _sceneLoader.ShowLoadingScreen();
        // TODO load next level
    }
}
