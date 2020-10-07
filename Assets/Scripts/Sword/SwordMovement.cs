using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class SwordMovement : MonoBehaviour
{
    // Proporties
    public float swordPositionY { get; private set; }
    public float axisY { get; set; }

    // Public variables
    public float speed = 6f;

    // Private variables
    [SerializeField] private GameResolution gameResolution;
    private Vector2 velocityVector;
    private CameraFollow cameraFollow;
    private FinishLevelController finishLevelController;
    private Rigidbody2D myRigidbody2D;
    private float swordPositionX;
    private float cameraNegativeLimitX;
    private float cameraPositiveLimitX;
    private float swordScaleX;
    private float axisX;
    private float screenCenterX;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        myRigidbody2D = GetComponent<Rigidbody2D>(); // Getting all components of type Rigidbody2D
        swordScaleX = transform.localScale.x; // Initializing the swordScaleX variable with the sword scale in the axis X
        cameraPositiveLimitX = cameraFollow.unitsInCameraX / 2 - (swordScaleX * gameResolution.gameResolutionScale);// Initializing and calculating the cameraPositiveLimitX variable, using a math formula
        cameraNegativeLimitX = -(cameraFollow.unitsInCameraX / 2) + (swordScaleX * gameResolution.gameResolutionScale); // Initializing and calculating the cameraNegativeLimitX variable, using a math formula
        screenCenterX = Screen.width * 0.5f; // Initializing and calculating the center of the screen
        axisX = 0; // Initializing the axisX variable to 0
        axisY = 0f; // Initializing the axisY variable to 0
        speed = gameResolution.screenAspectRatio * speed / 1.5f; // Calculating new adaptative speed to each resolution of screen, multiplying screen aspect ratio by original speed divided by 1.5 (it is based on screen aspect ratio of iPhone 4S as screen aspect ratio by default)
    }

    // FixedUpdate is called once per frame for the use of physics 
    private void FixedUpdate()
    {
        swordPositionX = transform.position.x; // Calculating the value of the swordPositionX variable with the current sword position in the axis X
        swordPositionY = transform.position.y; // Calculating the value of the swordPositionY variable with the current sword position in the axis Y
        myRigidbody2D.gravityScale = gameResolution.screenAspectRatio * 75 / 1.5f; // Calculating gravity adaptative scale, multiplying screen aspect ratio by 75 divided by 1.5 (it is based on screen aspect ratio of iPhone 4S as screen aspect ratio by default)

        axisX = Input.GetAxisRaw("Horizontal"); // Getting a number between -1 and 1 which indicate a input of movemento to the sword in the axis X

        // If the movement in X is different to 0 and the sword does not collide with the finish level, then the velocity vector will make equal to axes X and Y values
        if (axisX != 0 && !(finishLevelController.stopMovement))
        {
            velocityVector = new Vector2(axisX, axisY) * speed; // Calculating the velocity vector, multiplying a Vector2 with the sword speed
        } else if (finishLevelController.stopMovement) { // If the sword collides with the finish level, then the velocity vector will make equal to a vector of zeros
            velocityVector = Vector2.zero; // Making the value of velocity vector equal to a Vector2 of zeros
        } else { // If the sword does not collide with the finish level, then the velocity vector will make equal to axis Y values
            velocityVector.y = axisY * speed; // Calculating the velocity vector in the axis Y, multiplying the axis Y value with the sword speed
        }

        // If there are any touches currently, then it will calculate the position of touch (right or left)
        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0); // Getting the first one

            // If it began this frame and the sword does not collide with the finish level, then the sword will move to right or left, depending on the position of the screen touched
            if (firstTouch.phase == TouchPhase.Began && !(finishLevelController.stopMovement))
            {
                // If the touch position is to the right of center, then the sword will move to right
                if (firstTouch.position.x > screenCenterX)
                {
                    velocityVector = new Vector2(1f, axisY) * speed; // Calculating the velocity vector, multiplying a Vector2 with the sword speed
                } else if (firstTouch.position.x < screenCenterX) { // If the touch position is to the left of center, then the sword will move to left
                    velocityVector = new Vector2(-1f, axisY) * speed; // Calculating the velocity vector, multiplying a Vector2 with the sword speed
                }
            } else if (finishLevelController.stopMovement) { // If the sword collides with the finish level, then the velocity vector will make equal to a vector of zeros
                velocityVector = Vector2.zero; // Making the value of velocity vector equal to a Vector2 of zeros
            }
        }

        /*if (Input.GetKey(KeyCode.Space))
        {
            axisY = -2;
            velocityVector = new Vector2(axisX, axisY) * speed; // Calculating the velocity vector, multiplying a Vector2 with the sword speed
        } else {
            axisY = 0;
            velocityVector = new Vector2(axisX, axisY) * speed; // Calculating the velocity vector, multiplying a Vector2 with the sword speed
        }*/

        // If the sword is not in the camera range, then it will set a new sword position inside of camera range
        if (!(cameraNegativeLimitX < swordPositionX && swordPositionX < cameraPositiveLimitX))
        {
            if (swordPositionX < 0) // If sword position in the axis X is negative, then it will move the sword to the left border of the camera
                transform.position = new Vector3(cameraNegativeLimitX, transform.position.y, 0); // Moving the sword to the left border of the camera
            else if (swordPositionX > 0) // If sword position in the axis X is positive, then it will move the sword to the right border of the camera
                transform.position = new Vector3(cameraPositiveLimitX, transform.position.y, 0); // Moving the sword to the right border of the camera
        }
        
        myRigidbody2D.velocity = velocityVector; // Setting the value of velocityVector (previously calculated) to the velocity of Rigidbody2D
    }
}
