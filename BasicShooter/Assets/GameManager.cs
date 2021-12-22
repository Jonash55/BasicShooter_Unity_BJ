using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    bool wonGame = false;
    public Text endText;
    public float restartDelay = 1f;
    
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            LossShow();
            Invoke("LossClear", restartDelay);
            Invoke("Restart", restartDelay);
        }
        
    }

    public void GameWon()
    {
        if (!wonGame)
        {
            wonGame = true;
            WinShow();
            Invoke("WinClear", restartDelay);
            Invoke("Restart", restartDelay);
        }
    }
    void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void LossShow()
    {
        endText.text = "You died!\nGoing back to main menu...";
    }
    void LossClear()
    {
        endText.text = "";
    }
    void WinShow()
    {
        endText.text = "Congratulations!\nYou killed all enemies!";
    }
    void WinClear()
    {
        endText.text = "";
    }
}
