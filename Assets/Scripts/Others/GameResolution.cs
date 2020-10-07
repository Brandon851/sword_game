using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class GameResolution : MonoBehaviour
{
    // Proporties
    public float gameResolutionScale { private set; get; }
    public float wallPositionYConstant { private set; get; }
    public float gameResolutionPositionY { private set; get; }
    public float screenAspectRatio { private set; get; }
    public float cameraDisplacement { private set; get; }

    // Private variables
    private Transform gameResolution;
    private Camera mainCamera;

    /** PRIVATE METHODS **/

    // Awake can initialize variables only once per execution and can work inactivated
    private void Awake()
    {
        gameResolution = GetComponent<Transform>(); // Getting all components of type Transform
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // Getting all components of type CameraFollow

        screenAspectRatio = mainCamera.aspect; //Screen.width / Screen.height; // Initializing and calculating the screen aspect ratio, dividing the screen heigth divided by the screen width
        gameResolutionScale = 0.5504f * screenAspectRatio; // Initializing and calculating the game resolution scale with a linear regression formula (R^2 = 0.9998)
        cameraDisplacement = 8.4f * gameResolutionScale; // Initializing and calculating the cameraDisplacementDistance variable, multiplying 8.4 by game resolution scale
        gameResolutionPositionY = -11.061f * screenAspectRatio + 19.754f; // Initializing and calculating the game resolutionm position Y with a linear regression formula (R^2 = 0.9949)
        wallPositionYConstant = 7.1291f * Mathf.Pow(screenAspectRatio, 2) - 37.277f * screenAspectRatio + 34.311f; // Initializing and calculating  the wall position Y constant with a polynomial equation of order 2 (R^2 = 0.9962)
        gameResolution.localScale = new Vector3(gameResolutionScale, gameResolutionScale, 1); // Initializing the game resolution scale with the gameResolutionScale variable
        gameResolution.position = new Vector3(0, gameResolutionPositionY, 0); // Initializing the game resolution position Y with the gameResolutionPositionY variable
    }
}
