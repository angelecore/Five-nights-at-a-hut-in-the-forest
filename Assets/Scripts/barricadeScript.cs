using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barricadeScript : MonoBehaviour
{
    public int boards,prev;

    public Animator[] boardAnimation;
    public GameObject[] board;
    public Player player;
    public int hp =  0;
    // Start is called before the first frame update
    void Start()
    {
        boardAnimation = GetComponentsInChildren<Animator>();
        board[0].SetActive(false);
        board[1].SetActive(false);
        board[2].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(boards != prev)
        {
            prev = boards;
            switch (boards)
            {
                case 1:
                    boardAnimation[0].Play("board1Animation");
                    return;
                case 2:
                    boardAnimation[1].Play("board2Animation");
                    return;
                case 3:
                    boardAnimation[2].Play("board3Animation");
                    return;
            }
        }    
    }
    void addBarricade()
    {
        if(boards < 3 && player.boardchech())
        {
            board[boards].SetActive(true);
            boards += 1;
            hp += 6;
            player.removebaricadecount();
        }
    }
    void removeBarricade()
    {
        if (boards > 0 &hp !=0)
        {
            hp -= 3;
            if (hp == 12)
            {
                boards -= 1;
                board[boards].SetActive(false);

            }
            else if (hp == 6)
            {
                boards -= 1;

                board[boards].SetActive(false);
               
            }
            else if (hp == 0)
            {
                boards -= 1;
                board[boards].SetActive(false);
                if (boards == 0)
                {
                    GetComponent<Renderer>().enabled = false;
                }
            }

        }
    }
}
