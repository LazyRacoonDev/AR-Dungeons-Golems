using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public int maxDistance = 70;

    public void ActivateEvent(int eventID)
    {
        SceneManager.LoadScene("DummyScene 1");
    }

    public void GoToMap()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("Location-basedGame");
    }
}
