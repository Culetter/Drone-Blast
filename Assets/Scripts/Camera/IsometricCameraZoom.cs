using System;
using UnityEngine;

public class IsometricCameraZoom : MonoBehaviour
{
    [SerializeField] float zoomSpeed = 6;
    [SerializeField] float zoomSmoothness = 5;

    [SerializeField] float minZoom = 2;
    [SerializeField] float maxZoom = 40;

    private float _currentZoom;

    private Camera _camera;

    void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _currentZoom = Mathf.Clamp(_currentZoom - Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _currentZoom, zoomSmoothness * Time.deltaTime);
    }   
}
