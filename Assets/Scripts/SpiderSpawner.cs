using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderSpawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject SpiderPrefab;
    public int MaxSpiderCount = 3;
    private int CurrentSpiderCount = 0;

    //PhaseSwitcher initialises this script so don't need Start()
    /*void Start()
    {
        SpawnSpider();
    }*/

    //Listens to the spider being killed event
    public int GetSpiderCount()
    {
        return CurrentSpiderCount;
    }
    public void ReduceCount()
    {
        CurrentSpiderCount--;
    }
    private void OnEnable()
    {
        SpiderAI.OnSpiderKilled += SpawnSpider;
    }

    public void SpawnSpider()
    {
        StartCoroutine(DelayedSpawnSpider());
    }

    //Method is IEnumerator for the purpose of activating it after a random delay
    IEnumerator DelayedSpawnSpider()
    {
        //random spawn delay between 3 - 8 seconds
        int delay = Mathf.RoundToInt(Random.Range(3f, 8f));
        yield return new WaitForSeconds(delay);

        CurrentSpiderCount++;
        int spawnPtNumber = Mathf.RoundToInt(Random.Range(0f, SpawnPoints.Length - 1));
        Instantiate(SpiderPrefab, SpawnPoints[spawnPtNumber].transform.position, Quaternion.identity);

        //spiders will keep being spawned until the max limit is reached
        if (CurrentSpiderCount < MaxSpiderCount)
            SpawnSpider();
    }
}
