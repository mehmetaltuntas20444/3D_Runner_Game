using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
   //if you hit a barricade you die and this function assigned to try again button for loading the first scene to start the game from the beginning
  public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }

    //this function is asssigned to next level button and when you finish a level if you press next level button next scene is loaded by this function
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
