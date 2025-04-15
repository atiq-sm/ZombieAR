using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using JetBrains.Annotations;

public class ZombieAI : MonoBehaviour
{
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    public float speed = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = Camera.main.transform.position; // Get the position of the main camera

        agent.SetDestination(targetPosition); // Set the destination of the NavMeshAgent to the camera position
        agent.speed = speed; // Set the speed of the NavMeshAgent
    }
}
