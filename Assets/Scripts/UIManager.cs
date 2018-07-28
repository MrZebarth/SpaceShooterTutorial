using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private Sprite[] liveSprites;
    public Image livesImageDisplay;
    public Text scoreDisplay;
    public GameObject titleScreen;
    private int score=0;
    //update lives
    public void UpdateLives(int lives)
    {
        livesImageDisplay.sprite = liveSprites[lives];
    }
    //update score
    public void UpdateScore()
    {
        score += 10;
        scoreDisplay.text = "Score: " + score;
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        score = 0;
        scoreDisplay.text = "Score: " + score;
    }
}
