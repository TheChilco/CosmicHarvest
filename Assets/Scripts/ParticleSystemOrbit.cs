using UnityEngine;

public class ParticleSystemOrbit : MonoBehaviour
{
    public GameController gameController;
    public float minOrbitSpeed = 5.0f;  // Minimum speed of orbiting
    public float maxOrbitSpeed = 15.0f; // Maximum speed of orbiting
    public float transitionSpeed = 0.1f; // Speed at which to change directions

    private Vector3 currentDirection;
    private Vector3 targetDirection;
    private float currentOrbitSpeed;
    private float targetOrbitSpeed;
    private float changeDirectionTime = 0f;
    private float maxChangeDirectionTime = 5f;

    private void Start()
    {
        // Initialize the random direction and speed
        currentDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        targetDirection = currentDirection;
        currentOrbitSpeed = Random.Range(minOrbitSpeed, maxOrbitSpeed);
        targetOrbitSpeed = currentOrbitSpeed;
    }

    private void Update()
    {
        // Smoothly interpolate to the target direction and speed
        currentDirection = Vector3.Slerp(currentDirection, targetDirection, Time.deltaTime * transitionSpeed);
        currentOrbitSpeed = Mathf.Lerp(currentOrbitSpeed, targetOrbitSpeed, Time.deltaTime * transitionSpeed);

        // Orbit around the planet in a random direction at a random speed
        transform.RotateAround(gameController.currentTarget.transform.position, currentDirection, currentOrbitSpeed * Time.deltaTime);

        // Update the change direction time
        changeDirectionTime += Time.deltaTime;
        if(changeDirectionTime > maxChangeDirectionTime)
        {
            // Change direction and speed after a random period of time
            targetDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            targetOrbitSpeed = Random.Range(minOrbitSpeed, maxOrbitSpeed);

            // Reset the change direction time and set a new max time
            changeDirectionTime = 0f;
            maxChangeDirectionTime = Random.Range(3f, 10f);
        }
    }
}
