using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseSwitchTrigger : MonoBehaviour
{
    [SerializeField]
    private string PStag = "PhaseSwitcher";
    public SpiderSpawner spiderSpawner;

    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;

        if (otherGameObject.CompareTag(PStag))
        {
            otherGameObject.SetActive(false);
            Destroy(otherGameObject);
            spiderSpawner.SpawnSpider();
        }
    }
}
