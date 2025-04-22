using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject victoryScreen;

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("Location-basedGame");
    }
}
