using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Utils;

public class EventPointer : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10.0f;
    [SerializeField] float amplitude = 2.0f;
    [SerializeField] float frequency = 0.5f;

    LocationStatus playerLocation;
    public Vector2d eventPos;
    public int eventID; 

    UIMenu uiMenu;
    EventManager eventManager;
    void Start()
    {
        uiMenu = GameObject.Find("Canvas").GetComponent<UIMenu>();
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePointer();
    }

    void RotatePointer()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude) + 15, transform.position.z);
    }

    private void OnMouseDown()
    {
        playerLocation = GameObject.Find("Canvas").GetComponent<LocationStatus>();
        var currentLocation = new GeoCoordinatePortable.GeoCoordinate(playerLocation.GetLocation().LatitudeLongitude.x, playerLocation.GetLocation().LatitudeLongitude.y);
        var eventLocation = new GeoCoordinatePortable.GeoCoordinate(eventPos[0], eventPos[1]);
        var distance = currentLocation.GetDistanceTo(eventLocation);
        Debug.Log("Distance to the pointer: " + distance);
        if(distance < eventManager.maxDistance)
        {
            uiMenu.ShowInRangePanel(eventID);
        }
        else
        {
            uiMenu.ShowOutOfRangePanel();
        }
    }
}
