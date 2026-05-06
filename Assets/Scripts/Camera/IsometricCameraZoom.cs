using System;
using UnityEngine;

public class IsometricCameraZoom : MonoBehaviour
{
    [SerializeField] float zoomSpeed = 6;
    [SerializeField] float zoomSmoothness = 5;

    [SerializeField] float minZoom = 2;
    [SerializeField] float maxZoom = 40;

    [SerializeField] float currentZoom = 40;

    private Camera _camera;

    void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        currentZoom = Mathf.Clamp(currentZoom - Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime, minZoom, maxZoom);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, currentZoom, zoomSmoothness * Time.deltaTime);
    }   
}
