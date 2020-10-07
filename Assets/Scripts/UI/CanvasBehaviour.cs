using System.Collections; // Library called System.Collections, it uses some of classes and functions of C#
using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions
using UnityEngine.UI; // Library called UnityEngine.UI, it is used to all relationated with User Interfaces (UI)

public class CanvasBehaviour : MonoBehaviour
{
    // Proporty
    public SwordBehaviour swordBehaviour { get; private set; }

    // Private variable
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject levelCompleted;
    [SerializeField] private SpeedBarBehaviour speedBarBehaviour;
    private CanvasScaler UICanvasScaler;
    private FinishLevelController finishLevelController;

    /** PRIVATE METHODS **/

    // Awake can initialize variables only once per execution and can work inactivated
    private void Awake()
    {
        swordBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordBehaviour>(); // Getting all components of type SwordBehaviour
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        UICanvasScaler = GetComponent<CanvasScaler>(); // Getting all components of type CanvasScaler
        // UICanvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height); // Initializing the canvas resolution, depending on size of screen
    }

    // Update is called once per frame
    private void Update()
    {
        //UICanvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height); // Initializing the canvas resolution, depending on size of screen

        // If the sword is broken and the speed bar is deactivated, then the "Game Over" text will activate
        if (swordBehaviour.brokenSword && speedBarBehaviour.deactivateSpeedBar)
        {
            StartCoroutine(ActivateGameOver()); // Calling and starting the coroutine to activate the gameOver object on the end of last frame
        } else if (swordBehaviour.brokenSword && finishLevelController.brokenFinishLevel) { // If the sword is broken and the finished level is broken, then the "Level Completed" text will activate
            StartCoroutine(ActivateLevelCompleted()); // Calling and starting the coroutine to activate the levelCompleted object on the end of last frame
        }
    }

    // This method activates the gameOver object, this is activated when is on the end of last frame
    private IEnumerator ActivateGameOver()
    {
        yield return new WaitForEndOfFrame(); // Waiting until the end of last frame
        gameOver.SetActive(true); // Activating the game over animation with its respectives buttons
    }

    // This method activates the gameOver object, this is activated when is on the end of last frame
    private IEnumerator ActivateLevelCompleted()
    {
        yield return new WaitForEndOfFrame(); // Waiting until the end of last frame
        levelCompleted.SetActive(true); // Activating the level completed animation with its respectives stars and buttons
    }
}
