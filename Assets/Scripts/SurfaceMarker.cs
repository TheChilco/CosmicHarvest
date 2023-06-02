using UnityEngine;

public class SurfaceMarker : MonoBehaviour
{
    public GameObject markerPrefab;
    private GameObject currentMarker;
    public GameController gameController; // Reference to the GameController script
    public LayerMask planetLayer; // Layer mask for the planet

    void Start()
    {
        currentMarker = Instantiate(markerPrefab);
        currentMarker.SetActive(false);
    }

    void Update()
    {
        // Only proceed if there's a current target in GameController
        if (gameController.currentTarget != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, planetLayer))
            {
                currentMarker.SetActive(true);
                currentMarker.transform.position = hit.point;

                // Align marker up direction with the direction towards planet center
                Vector3 directionToPlanetCenter = (currentMarker.transform.position - gameController.currentTarget.position).normalized;
                currentMarker.transform.up = -directionToPlanetCenter;
            }
            else
            {
                currentMarker.SetActive(false);
            }
        }
    }
}
