using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barricadeScript : MonoBehaviour
{
    public int boards;
    public GameObject[] board;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void addBarricade()
    {
        if(boards < 3)
        {
            board[boards].SetActive(true);
            boards += 1;
        }
    }
    void removeBarricade()
    {
        if (boards > 0)
        {    
            boards -= 1;
            board[boards].SetActive(false);
        }
    }
}
