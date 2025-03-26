using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelInRange;
    [SerializeField] private GameObject panelOutOfRange;

    bool isUiActive;
    int tempEventID;
    [SerializeField] private EventManager eventManager;
    void Start()
    {
        isUiActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowInRangePanel(int eventID)
    {
        if (!isUiActive)
        {
            tempEventID = eventID;
            panelInRange.SetActive(true);
            isUiActive = true;
        }
    }

    public void ShowOutOfRangePanel()
    {
        if (!isUiActive) {
            panelOutOfRange.SetActive(true);
            isUiActive = true;
        }
    }

    public void CloseButtonClick()
    {
        panelInRange.SetActive(false);
        panelOutOfRange.SetActive(false);
        isUiActive = false;
    }

    public void OnJoinButtonClick()
    {
        Debug.Log("Join button clicked for event ID: " + tempEventID);
        eventManager.ActivateEvent(tempEventID);
    }
}
