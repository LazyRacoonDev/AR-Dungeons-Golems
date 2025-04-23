using UnityEngine;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.Collections;
using System.Collections.Generic;

public class SpawnAroundPlayer : MonoBehaviour
{
    [SerializeField] AbstractMap _map;
    [SerializeField] GameObject _markerPrefab;
    [SerializeField] float _spawnScale = 100f;
    [SerializeField] int _numberOfPoints = 10;
    [SerializeField] float _spawnRadiusInMeters = 500f;
    [SerializeField] float _regenerationInterval = 10f;

    List<GameObject> _spawnedObjects = new List<GameObject>();

    private bool _mapReady = false;

    void OnEnable()
    {
        _map.OnInitialized += StartSpawning;
    }

    void OnDisable()
    {
        _map.OnInitialized -= StartSpawning;
    }

    void StartSpawning()
    {
        _mapReady = true;
        StartCoroutine(RegeneratePointsRoutine());
    }

    private IEnumerator RegeneratePointsRoutine()
    {
        while (true)
        {
            // Eliminar los anteriores
            foreach (var obj in _spawnedObjects)
            {
                Destroy(obj);
            }
            _spawnedObjects.Clear();

            Vector2d playerLatLon = _map.CenterLatitudeLongitude;

            for (int i = 0; i < _numberOfPoints; i++)
            {
                Vector2d randomLatLon = GenerateRandomPointAround(playerLatLon, _spawnRadiusInMeters);

                var instance = Instantiate(_markerPrefab);
                instance.GetComponent<EventPointer>().eventPos = randomLatLon;
                instance.GetComponent<EventPointer>().eventID = i + 1;
                instance.transform.localPosition = _map.GeoToWorldPosition(randomLatLon, true);
                instance.transform.localScale = Vector3.one * _spawnScale;

                _spawnedObjects.Add(instance);
            }

            yield return new WaitForSeconds(_regenerationInterval);
        }
    }

    private void Update()
    {
        if (!_mapReady) return;

        foreach (var obj in _spawnedObjects)
        {
            var location = obj.GetComponent<EventPointer>().eventPos;
            obj.transform.localPosition = _map.GeoToWorldPosition(location, true);
        }
    }

    Vector2d GenerateRandomPointAround(Vector2d center, float radiusInMeters)
    {
        float radiusInDegrees = radiusInMeters / 111320f;
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(0f, radiusInDegrees);

        float latOffset = distance * Mathf.Cos(angle);
        float lonOffset = distance * Mathf.Sin(angle) / Mathf.Cos((float)center.x * Mathf.Deg2Rad);

        return new Vector2d(center.x + latOffset, center.y + lonOffset);
    }
}