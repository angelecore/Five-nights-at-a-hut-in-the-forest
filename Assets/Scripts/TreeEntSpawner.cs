using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEntSpawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject TreeEntPrefab;
    public float SpawnDeviationDistance = 3f;
    public int SapwnAmount = 6;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SapwnAmount; i++)
        {
            SpawnTreeEnt();
        }
    }

    public void SpawnTreeEnt()
    {
        int spawnPtNumber = Mathf.RoundToInt(Random.Range(0f, SpawnPoints.Length - 1));

        float randomX = Random.Range(-SpawnDeviationDistance, SpawnDeviationDistance);
        float randomZ = Random.Range(-SpawnDeviationDistance, SpawnDeviationDistance);

        Vector3 spawnPosition = new Vector3(
            SpawnPoints[spawnPtNumber].transform.position.x + randomX,
            SpawnPoints[spawnPtNumber].transform.position.y,
            SpawnPoints[spawnPtNumber].transform.position.z + randomZ);

        Instantiate(TreeEntPrefab, spawnPosition, Quaternion.identity);
    }
}
