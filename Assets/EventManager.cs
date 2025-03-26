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
        switch (eventID)
        {
            case 1:
                SceneManager.LoadScene("DummyScene");
                break;
            case 2:
                SceneManager.LoadScene("DummyScene 1");
                break;
            default:
                Debug.Log("Event not found");
                break;
        }
    }
}
