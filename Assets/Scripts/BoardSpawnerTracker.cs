using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawnerTracker : MonoBehaviour
{
    #region Singleton

    public static BoardSpawnerTracker instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject boardSpawner;
}
