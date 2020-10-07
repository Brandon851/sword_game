using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class DestroyObjectOffCamera : MonoBehaviour
{
    // Private variable
    [SerializeField] private int errorMargin = 0;
    private CameraFollow cameraFollow;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
    }

    // Update is called once per frame
    private void Update()
    {
        // If the object is outside of range of the camera, then the reward object will destroy itself
        if ((transform.position.y - errorMargin) > (cameraFollow.transform.position.y + cameraFollow.unitsInCameraY * 2 / 3))
        {
            Destroy(gameObject); // Destroying the object
        }
    }
}
