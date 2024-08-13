using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsInArea : MonoBehaviour
{
    public GameObject objectToSpawn;
    [SerializeField] private int numberOfObjectsToSpawn = 40; 
    public Collider spawnArea; 
    private List<Vector3> spawnPositions = new ();

    private void Start()
    {
        if (objectToSpawn == null || spawnArea == null)
        {
            Debug.LogError("Spawn");
            return;
        }

        Bounds spawnBounds = spawnArea.bounds;

        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPositionInArea(spawnBounds);
            spawnPositions.Add(randomPosition);
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionInArea(Bounds bounds)
    {
        Vector3 randomPosition = Vector3.zero;

        int maxAttempts = 100;

        for (int i = 0; i < maxAttempts; i++)
        {
            float boost = 0.2f;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = spawnArea.transform.position.y + boost;
            float z = Random.Range(bounds.min.z, bounds.max.z);

            randomPosition = new Vector3(x, y, z);

            bool positionIsValid = IsPositionValid(randomPosition);

            if (positionIsValid)
                return randomPosition;
        }

        return randomPosition;
    }

    private bool IsPositionValid(Vector3 position)
    {
 
        foreach (Vector3 existingPosition in spawnPositions)
        {
            if (Vector3.Distance(position, existingPosition) < 2f)
            {
                return false;
            }
        }

        return true;
    }
}