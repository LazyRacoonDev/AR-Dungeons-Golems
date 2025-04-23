using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameManager : MonoBehaviour
{
    public GameObject victoryCanvas;
    public GameObject defeatCanvas;
    public GameObject inGameCanvas; 

    public void ShowVictoryCanvas()
    {
        victoryCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }

    public void ShowDefeatCanvas()
    {
        defeatCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
        Time.timeScale = 0f; // Pause the game
    }
}
