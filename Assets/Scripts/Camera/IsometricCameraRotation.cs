using UnityEngine;

public class IsometricCameraRotation : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 200f;

    private float yaw;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            yaw += mouseX * rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }
}