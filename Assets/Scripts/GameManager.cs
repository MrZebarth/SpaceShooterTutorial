using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool gameOver = true;
    public GameObject player;

    private UIManager UIM;
    //if game over
    //if space key pressed
    //spawn the player
    //gameOver is false
    //hide the title screen

    private void Start()
    {
        UIM = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    private void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player, Vector3.zero, Quaternion.identity);
                gameOver = false;
                if (UIM)
                {
                    UIM.HideTitleScreen();
                }
            }
        }
    }
}
