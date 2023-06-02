using UnityEngine;

public class RotateLight : MonoBehaviour
{
    public GameController gameController;
    public float orbitSpeed = 10.0f; // The speed of orbiting
    public float amplitude = 45.0f; // The amplitude of the rotation oscillation
    public float frequency = 2.0f; // The frequency of the rotation oscillation

    private void Update()
    {
        // Orbit around the planet
        transform.RotateAround(gameController.currentTarget.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);

        // Adjust X rotation using a sine function
        float newXRotation = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.rotation = Quaternion.Euler(newXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
