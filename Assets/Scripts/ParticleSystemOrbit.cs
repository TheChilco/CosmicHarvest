using UnityEngine;

public class ParticleSystemOrbit : MonoBehaviour
{
    public GameController gameController;
    public float orbitSpeed = 10.0f; // The speed of orbiting

    private void Update()
    {
        // Orbit around the planet
        transform.RotateAround(gameController.currentTarget.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
