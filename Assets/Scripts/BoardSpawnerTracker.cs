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
    public GameObject boardSpawnerTarget;
    public GameObject boardSpawner1;
    public GameObject boardSpawnerTarget1;
    public GameObject boardSpawner2;
    public GameObject boardSpawnerTarget2;
    public GameObject boardSpawner3;
    public GameObject boardSpawnerTarget3;
    public GameObject boardSpawner4;
    public GameObject boardSpawnerTarget4;
    public GameObject boardSpawner5;
    public GameObject boardSpawnerTarget5;
}
