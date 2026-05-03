using UnityEngine;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] float droneSpeed = 1.0f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float arriveDistance = 0f;

    private Vector3 target;
    private bool hasTarget = false;
    private bool isRotating = false;

    private void Update()
    {
        if (hasTarget)
        {
            if (HasReachedTarget())
            {
                hasTarget = false;
                isRotating = true;
                return;
            }

            MoveToTarget();
        }
        else if (isRotating)
        {
            Rotate();

            if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(Vector3.forward)) < 1f)
            {
                isRotating = false;
            }
        }
    }

    private void MoveToTarget()
    {
        Vector3 direction = (target - transform.position);

        RotateTowards(direction);
        MoveTowards(target);
    }

    private void RotateTowards(Vector3 direction)
    {
        if (direction.sqrMagnitude < 0.0001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void Rotate()
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowards(Vector3 targetPos)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPos,
            droneSpeed * Time.deltaTime
        );
    }

    public void SetTarget(GameObject sectorTarget)
    {
        target = new Vector3(sectorTarget.transform.position.x, transform.position.y, sectorTarget.transform.position.z);
        hasTarget = true;
    }

    public void SetTarget(SpawnPoint targetPos)
    {
        target = targetPos.transform.position;
        hasTarget = true;
    }

    public bool HasReachedTarget()
    {
        return Vector3.Distance(transform.position, target) <= arriveDistance;
    }
}
