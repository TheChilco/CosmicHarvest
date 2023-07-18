using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public GameController gameController;
    [SerializeField] private const float OrbitSpeed = 10.0f; // The speed of orbiting
    [SerializeField] private const float Amplitude = 45.0f; // The amplitude of the rotation oscillation
    [SerializeField] private const float Frequency = 2.0f; // The frequency of the rotation oscillation

    private void Update()
    {
        // Orbit around the planet
        transform.RotateAround(gameController.currentTarget.transform.position, Vector3.up, OrbitSpeed * Time.deltaTime);

        // Adjust X rotation using a sine function
        float newXRotation = Mathf.Sin(Time.time * Frequency) * Amplitude;
        transform.rotation = Quaternion.Euler(newXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
