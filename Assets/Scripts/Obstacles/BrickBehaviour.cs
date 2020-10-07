using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class BrickBehaviour : MonoBehaviour
{
    // Public variables
    public float degreePerSecond = 720f;
    public float speedPosition = 20f;
    public float cameraDisplacementDistance = 12f;

    // Private variables
    private GameResolution gameResolution;
    private CameraFollow cameraFollow;
    private Vector3 brickPosition;
    private Vector3 brickRotation;
    private float degree;
    private float speed;
    private float brickPositionY;
    private float cameraPositionY;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
        gameResolution = GameObject.FindGameObjectWithTag("GameResolution").GetComponent<GameResolution>(); // Getting all components of type GameResolution
        //degreePerSecond *= gameResolution.gameResolutionScale; // Calculating degreePerSecond multiplying by game resolution scale
        //speedPosition /= gameResolution.gameResolutionScale; // Calculating speedPosition multiplying by game resolution scale
        //cameraDisplacementDistance /= gameResolution.gameResolutionScale; // Calculating cameraDisplacementDistance multiplying by game resolution scale
        brickRotation = Vector3.zero; // Initializing the brickRotation variable to a Vector3 of zeros
        brickPosition = Vector3.zero; // Initializing the brickPosition variable to a Vector3 of zeros
        degree = 0; // Initializing the degree variable to 0
        speed = 0; // Initializing the speed variable to 0
    }

    // FixedUpdate is called once per frame for the use of physics
    private void FixedUpdate()
    {
        cameraPositionY = cameraFollow.cameraPositionY; // Calculating the value of the cameraPositionY variable with the current camera position in the axis Y
        brickPositionY = transform.position.y; // Calculating the value of the brickPositionY variable with the current brick position in the axis Y

        brickPosition = new Vector3(0, speed * Time.fixedDeltaTime, 0); // Calculating a new position to the brick, multiplying the speed by Time.deltaTime (time in seconds since the last frame) in the axis Y
        brickRotation = new Vector3(0, 0, degree * Time.fixedDeltaTime); // Calculating a new rotation to the brick, multiplying the degree by Time.deltaTime (time in seconds since the last frame) in the axis Z
        transform.Rotate(brickRotation); // Applying a brick rotation in the axis Z
        transform.position += brickPosition; // Applying a increase of brick position in the axis Y

        // If the brick is on screen, speed and degree variables will set
        if ((cameraPositionY - cameraDisplacementDistance) <= brickPositionY)
        {
            speed = speedPosition; // Setting the speed variable with  the speedPosition variable (previously calculated)
            degree = degreePerSecond; // Setting the degree variable with the degreePerSecond variable (previously initialized)
        }
    }
}
