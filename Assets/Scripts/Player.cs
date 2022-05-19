using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
   public bool canRepairWall;
    float rebuiltTimer = 0;
    public int boardcount = 10;
    public Text ui;
    public Text boardsc;
    public CanvasController gameStateController;
    public playerMovement move;

    public SpiderSpawner SpiderSpawner;
    public TreeEntSpawner TreeEntSpawner;
    
    public int maxHealth = 10;
    public float currentHealth;
    public HealthBar healthBar;
    private int SpiderCount;
    private int TreeEntCount;

    public Text EnemyCount;
    int currentscore=0;
    int spiderkillscore = 100;
    int treeendstunscore = 200;
    public Text currentscorefield;

    int highscore => PlayerPrefs.GetInt("Highscore", 1);
    // public Text boardcountUI;
    // Start is called before the first frame update
    void Start()
    {
        boardsc.text = $"boards: {boardcount}";
        currentHealth = maxHealth;
        healthBar.SetMaxHP(maxHealth);
        SpiderCount = SpiderSpawner.GetSpiderCount();
        TreeEntCount = TreeEntSpawner.SapwnAmount;
        currentscore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SpiderCount = SpiderSpawner.GetSpiderCount();
        TreeEntCount = TreeEntSpawner.SapwnAmount;
        EnemyCount.text = $"Spiders: {SpiderCount}, TreeEnts: {TreeEntCount}";
    }

    public void spideronkill()
    {
        currentscore += spiderkillscore;
        currentscorefield.text = $"current score: {currentscore}";
    }
    public void Entonkill()
    {
        currentscore += treeendstunscore;
        currentscorefield.text = $"current score: {currentscore}";
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {   
            if(currentscore >= highscore)
                PlayerPrefs.SetInt("Highscore", currentscore);
            StopGame();
            currentHealth = 999999;
        }
    }
    public void StopGame()
    {
        move.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        gameStateController.ShowGameOverMenu();
    }
    void OnTriggerEnter(Collider collide)
    {
        if (collide.gameObject.tag == "boardSpawner")
        {
            canRepairWall = true;
        }
    }
    void OnTriggerExit(Collider collide)
    {
        if (collide.gameObject.tag == "boardSpawner")
        {
            canRepairWall = false;
            ui.text = "";
        }
    }

    public void removebaricadecount()
    {
        boardcount--;
        boardsc.text = $"boards: {boardcount}";
    }

    public bool boardchech()
    {
        return boardcount > 0;
    }


    void OnTriggerStay(Collider collide)
    {
        if (collide.gameObject.tag == "boardSpawner")
        {
            rebuiltTimer += Time.deltaTime;
            if (canRepairWall)
            {
                
                ui.text = "Press F to repair barrier";
                if (Input.GetKey(KeyCode.F) && rebuiltTimer > 1.0f)
                {
                    collide.SendMessage("addBarricade", SendMessageOptions.RequireReceiver);
                    rebuiltTimer = 0;
                }
            }
        }
    }
}
