using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public static bool isGameStarted;
    public static int score;
    public Text scoreText;
    public GameObject startLevel;
    public Text taxText;
    public static float afterTax;

    //assigned starting variabled
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        score = 0;
        afterTax = 0;
    }

    // if game over (you git game object assigned with barricade) open game over panel and set time scale to 0. show the coin you collect. start the game if user touch screen.
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        scoreText.text = "Coins : " + score;
        afterTax = score - ((score * 80) / 100);
        taxText.text = "You Earned " + afterTax + " coins because government takes 80% for tax :)";

        if (Input.touchCount > 0)
        {
            isGameStarted = true;
            startLevel.SetActive(false);

        }
    }

}
