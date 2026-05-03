using UnityEngine;

public class IsometricCameraPanner : MonoBehaviour
{
    [SerializeField] float panSpeed = 6f;
    private Camera _camera;

    void Awake()
    {
        _camera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        Vector2 panPosition = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        transform.position += Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * new Vector3(panPosition.x, 0, panPosition.y) * panSpeed * Time.deltaTime;
    }
}
