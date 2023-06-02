using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] planetPrefabs; // Array of planet prefabs
    public Material[] shaders; // Array of shaders
    public Transform spawnPoint; // Point where the planet will be spawned
    public Transform currentTarget; // The current target for the camera
    public CameraController cameraController; // Reference to the camera controller

    void Start()
    {
        SpawnRandomPlanet();
    }

    void SpawnRandomPlanet()
    {
        // Select a random planet prefab
        int randomPlanetIndex = Random.Range(0, planetPrefabs.Length);
        GameObject selectedPlanetPrefab = planetPrefabs[randomPlanetIndex];

        // Instantiate the selected planet at the spawnPoint
        GameObject newPlanet = Instantiate(selectedPlanetPrefab, spawnPoint.position, spawnPoint.rotation);
        newPlanet.layer = 3;

        // Select a random shader
        int randomShaderIndex = Random.Range(0, shaders.Length);
        Material selectedShader = shaders[randomShaderIndex];

        // Apply the selected shader to the planet
        newPlanet.GetComponent<Renderer>().material = selectedShader;

        // Set the new planet as the camera's target
        currentTarget = newPlanet.transform;

    }
}
