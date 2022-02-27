using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool canRepairWall;
    float rebuiltTimer = 0;
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
            collide.GetComponent<UI>().PromptText.text = "";
        }
    }

    void OnTriggerStay(Collider collide)
    {
        if (collide.gameObject.tag == "boardSpawner")
        {
            rebuiltTimer += Time.deltaTime;
            if (canRepairWall)
            {
                UI ui = collide.GetComponent<UI>();
                ui.PromptText.text = "Press F to repair barrier";
                if (Input.GetKey("F") && rebuiltTimer > 1.0f)
                {
                    collide.SendMessage("AddBoard", SendMessageOptions.RequireReceiver);
                    rebuiltTimer = 0;
                }
            }
        }
    }
}
