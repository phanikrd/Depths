using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Dynamic : MonoBehaviour
{
    public GameObject player;
    public float maxDistance = 100f;

    private NavMeshSurface navMeshSurface;

    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        UpdateNavMesh();
    }

    void Update()
    {
        // Check distance between player and this object
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > maxDistance)
        {
            // Player is far away, remove NavMeshSurface
            RemoveNavMesh();
        }
        else
        {
            // Player is nearby, ensure NavMeshSurface exists
            UpdateNavMesh();
        }
    }

    void UpdateNavMesh()
    {
        if (navMeshSurface == null)
        {
            navMeshSurface = gameObject.AddComponent<NavMeshSurface>();
        }

        navMeshSurface.BuildNavMesh();
    }

    void RemoveNavMesh()
    {
        if (navMeshSurface != null)
        {
            Destroy(navMeshSurface);
            navMeshSurface = null;
            NavMesh.RemoveAllNavMeshData();
        }
    }
}
