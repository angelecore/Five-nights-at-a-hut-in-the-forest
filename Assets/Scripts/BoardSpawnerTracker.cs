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

    public barricadeScript boardSpawner;
    public GameObject boardSpawnerTarget;
    public GameObject insideTarget;
    public barricadeScript boardSpawner1;
    public GameObject boardSpawnerTarget1;
    public GameObject insideTarget1;
    public barricadeScript boardSpawner2;
    public GameObject boardSpawnerTarget2;
    public GameObject insideTarget2;
    public barricadeScript boardSpawner3;
    public GameObject boardSpawnerTarget3;
    public GameObject insideTarget3;
    public barricadeScript boardSpawner4;
    public GameObject boardSpawnerTarget4;
    public GameObject insideTarget4;
    public barricadeScript boardSpawner5;
    public GameObject boardSpawnerTarget5;
    public GameObject insideTarget5;
}
