using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;  // Prefab of the sphere to be instantiated
    public GameObject referenceObject;  // The object from which the distance will be calculated
    public float distance = 5f;  // Distance along the X-axis

    private Vector3 initialPosition;  // To store the initial position of the reference object
    private GameObject createdSphere; // To keep track of the created sphere

    public GameObject newCamera;
    public GameObject mainCamera;

    void Start()
    {
        newCamera.SetActive(false);
        mainCamera.SetActive(true);
        if (referenceObject != null)
        {
            initialPosition = referenceObject.transform.position;
        }
        else
        {
            Debug.LogError("Reference Object is not assigned.");
        }
    }

    // This function will be called when the button is pressed
    public void SpawnSphere()
    {
        if (spherePrefab != null && referenceObject != null)
        {
            if (createdSphere == null)
            {
                newCamera.SetActive(true);
                mainCamera.SetActive(true);
                // Calculate the new position based on the initial position and the distance
                Vector3 newPosition = initialPosition + new Vector3(distance, 0, 0);

                // Instantiate the sphere at the calculated position
                createdSphere = Instantiate(spherePrefab, newPosition, Quaternion.identity);
            }
            else
            {
                Debug.Log("A sphere has already been created.");
            }
        }
        else
        {
            Debug.LogError("Sphere Prefab or Reference Object is not assigned.");
        }
    }
}