using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using Unity.AI.Navigation;
using Meta.XR.MRUtilityKit;
public class RuntimeNavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        MRUK.Instance.RegisterSceneLoadedCallback(BuildNavmesh);

    }

    public void BuildNavmesh()
    {
        StartCoroutine(BuildNavmeshRoutine());
    }

    public IEnumerator BuildNavmeshRoutine()
    {
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame to ensure all objects are loaded
        navMeshSurface.BuildNavMesh(); // Build the NavMesh at runtime
    }
}
