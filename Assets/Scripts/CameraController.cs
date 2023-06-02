using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameController gameController; // Reference to the game controller
    public float distance = 5.0f; // Set initial distance
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    // Variables for zooming
    public float zoomSpeed = 1.0f; // Speed of the zoom
    public float minZoom = 1.0f; // Minimum zoom distance
    public float maxZoom = 20.0f; // Maximum zoom distance
    private float targetDistance; // The target zoom level

    // Variables for vertical rotation clamping
    public float yMinLimit = -80f; // Minimum vertical angle
    public float yMaxLimit = 80f; // Maximum vertical angle

    private float x = 0.0f;
    private float y = 0.0f;

    private void Start ()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        targetDistance = distance; // Initialize targetDistance to be the same as the initial distance
    }

    private void LateUpdate ()
    {
        if (gameController != null && gameController.currentTarget != null)
        {
            x += Input.GetAxis("Horizontal") * xSpeed * 0.02f;
            y -= Input.GetAxis("Vertical") * ySpeed * 0.02f;

            // Clamping vertical rotation
            y = ClampAngle(y, yMinLimit, yMaxLimit);

            // Set the target zoom level
            targetDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            targetDistance = Mathf.Clamp(targetDistance, minZoom, maxZoom);

            // Interpolate from the current distance to the target distance
            distance = Mathf.Lerp(distance, targetDistance, Time.deltaTime * zoomSpeed);

            Quaternion rotation = Quaternion.Euler(-y, -x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + gameController.currentTarget.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    // Clamping angle function
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
