using UnityEngine;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.UI;
using TMPro;

public class LookAtTargetNew : MonoBehaviour {

    [Tooltip("This is the object that the script's game object will look at by default")]
    public GameObject defaultTarget; // the default target that the camera should look at

    [Tooltip("This is the object that the script's game object is currently look at based on the player clicking on a gameObject")]
    public GameObject currentTarget; // the target that the camera should look at
    public GameObject starCenter;
    public GameObject imageOfTarget;

    private string textoDaImagem;


    private Camera mainCamera;

    private float focusedFOV;

    private float coordX;

    private float defaultFOV;

	void Start () {
		if (defaultTarget == null) 
		{
            defaultTarget = this.gameObject;
			Debug.Log ("defaultTarget target not specified. Defaulting to parent GameObject");
		}

        if (currentTarget == null)
        {
            currentTarget = this.gameObject;
            Debug.Log("currentTarget target not specified. Defaulting to parent GameObject");
        }
        mainCamera = GetComponent<Camera>();

        defaultFOV = mainCamera.fieldOfView;
    }
	
	// Update is called once per frame
    // For clarity, Update happens constantly as your game is running
    void Update()
    {
        // if primary mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // determine the ray from the camera to the mousePosition
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // cast a ray to see if it hits any gameObjects
            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);

            // if there are hits
            if (hits.Length>0)
            {
                // get the first object hit
                RaycastHit hit = hits[0];
                currentTarget = hit.collider.gameObject;
                textoDaImagem = currentTarget.GetComponent<PlanetInfo>().explanation;
                focusedFOV = currentTarget.GetComponent<PlanetInfo>().FOV;
                mainCamera.fieldOfView = focusedFOV;

                Debug.Log("defaultTarget changed to "+currentTarget.name);
            }
        } else if (Input.GetMouseButtonDown(1)) // if the second mouse button is pressed
        {
            mainCamera.fieldOfView = defaultFOV;
            currentTarget = defaultTarget;
            Debug.Log("defaultTarget changed to " + currentTarget.name);
        }

       // if a currentTarget is set, then look at it
        if (currentTarget != null && currentTarget != starCenter) {
            imageOfTarget.SetActive(true);

            // Adjust the look direction slightly to the left of target
            coordX = currentTarget.GetComponent<PlanetInfo>().coorX;
            Vector3 lookAtTarget = currentTarget.transform.position + new Vector3(coordX, 0, 0);
            transform.LookAt(lookAtTarget);
            imageOfTarget.GetComponentInChildren<TextMeshProUGUI>().text = textoDaImagem;
        }
        else if (currentTarget != null) {
            imageOfTarget.SetActive(false);

            // If the current target is the exception, do a different or standard behavior
            // For example, just centering the target
            currentTarget = defaultTarget; // Standard behind the target
            transform.LookAt(currentTarget.transform);
        } else // reset the look at back to the default
        {
            imageOfTarget.SetActive(false);
            currentTarget = defaultTarget;
            Debug.Log("defaultTarget changed to " + currentTarget.name);
        }
    }
}
