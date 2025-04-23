using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public int maxDistance = 70;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateEvent(int eventID)
    {
        SceneManager.LoadScene("DummyScene 1");
    }

    public void GoToMap()
    {
        SceneManager.LoadScene("Location-basedGame");
    }
}
