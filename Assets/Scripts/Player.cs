using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
   public bool canRepairWall;
    float rebuiltTimer = 0;
    public Text ui;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
