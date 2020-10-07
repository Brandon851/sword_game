using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class CameraFollow : MonoBehaviour
{
    // Properties
    public float cameraPositionY { get; private set; }
    public float unitsInCameraX { get; private set; }
    public float unitsInCameraY { get; private set; }

    // Public variables
    public float smoothedSpeed = 1f;
    public Vector3 offset;


    // Private variables
    [SerializeField] private Transform target;
    private Camera mainCamera;
    private FinishLevelController finishLevelController;

    /** PRIVATE METHODS **/

    // Awake can initialize variables only once per execution and can work inactivated
    private void Awake()
    {
        mainCamera = GetComponent<Camera>(); // Getting all components of type Camera
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        cameraPositionY = mainCamera.transform.position.y; // Initializing the cameraPositionY variable with the camera position in the axis Y
        unitsInCameraY = mainCamera.orthographicSize * 2; // Iniatializing and calculating the unitsInCameraY variable, multiplying the orthograpic camera size by 2
        unitsInCameraX = unitsInCameraY * mainCamera.aspect; // Iniatializing and calculating the unitsInCameraX variable, multiplying the orthograpic camera size by the aspect ratio of device
    }

    // FixedUpdate is called once per frame for the use of physics
    private void FixedUpdate()
    {
        cameraPositionY = transform.position.y; // Updating the value of the cameraPositionY variable with the current camera position in the axis Y

        // If sword is not destroyed and the camera does not stop, then the camera will follow to the sword [target]
        if(target != null && !finishLevelController.stopMovement)
        {
            Vector3 desiredPosition = new Vector3(0, target.position.y, 0) + offset; // Calculate the desired position to the camera, adding a offset to sword position in the axis Y
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothedSpeed); // Calculate a smoothed position using linear interpolate between camera position and desired position, depending on a smoothed speed
            transform.position = smoothedPosition; // Setting the camera position with the value of smoothedPosition (previously calculated)
        }
    }
}
