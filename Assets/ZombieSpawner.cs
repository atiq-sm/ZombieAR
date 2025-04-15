using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
public class ZombieSpawner : MonoBehaviour
{
    public float spawnTimer = 1;
    private float timer;

    public float minEdgeDistance = 0.3f; // Minimum distance from the player to spawn the zombie
    public MRUKAnchor.SceneLabels spawnLabels;
    public float normalOffset;

    public int spawnTry = 1000;

    public GameObject zombiePrefab; // Prefab for the zombie to be spawned
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MRUK.Instance && MRUK.Instance.IsInitialized)
            return; // Check if MRUK is initialized, if not, exit the method

        timer += Time.deltaTime; // Increment the timer by the time since the last frame    
        if (timer >= spawnTimer) // Check if the timer has reached the spawn interval
        {
            SpawnZombie(); // Call the method to spawn a zombie
            timer -= spawnTimer; 
        }

    }

    public void SpawnZombie()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();

        int currentTry = 0;

        while(currentTry < spawnTry)
        {
            bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, minEdgeDistance, LabelFilter.Included(spawnLabels), out Vector3 pos, out Vector3 norm);

            if (hasFoundPosition)
            {
                Vector3 randomPositionNormalOffset = pos + norm * normalOffset; // Generate a random position within a sphere of radius 5
                randomPositionNormalOffset.y = 0; // Set the y-coordinate to 0 to keep the zombie on the ground


                Instantiate(zombiePrefab, randomPositionNormalOffset, Quaternion.identity);
                return;
            }

            else
            {
                currentTry++;
            }

        }




    }
}
