using System.Collections; // Library called System.Collections, it uses some of classes and functions of C#
using UnityEngine; // Library called UnityEngine, it uses most of classes and functions of Unity

public class FinishLevelController : MonoBehaviour
{
    // Proporties
    public bool stopBackground { get; private set; }
    public bool stopMovement { get; private set; }
    public bool brokenFinishLevel { get; private set; }
    public float distanceFinishLevel { get; private set; }

    // Private variables
    [SerializeField] private SpeedBarBehaviour speedBarBehaviour;
    [SerializeField] private GameResolution gameResolution;
    private CameraFollow cameraFollow;
    private SwordMovement swordMovement;
    private SwordBehaviour swordBehaviour;
    private Animator myAnimator;
    private BoxCollider2D myBoxCollider2D;
    private int numberOfTotalWalls;
    private int numberOfFreeWalls;
    private int brokenFinishLevelHashCode;
    private float swordPositionY;
    private float finishLevelPositionY;
    private float distanceSwordFinishLevel;
    private float currentSpeedBarValue;
    private float speedBarMaxValue;

    /** PRIVATE METHODS **/

    // Awake can initialize variables only once per execution and can work inactivated
    private void Awake()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
        swordMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordMovement>(); // Getting all components of type SwordMovement
        swordBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordBehaviour>(); // Getting all components of type SwordBehaviour
        myAnimator = GetComponent<Animator>(); // Getting all components of type Animator
        myBoxCollider2D = GetComponent<BoxCollider2D>(); // Getting all components of type BoxCollider2D
        speedBarMaxValue = speedBarBehaviour.sliderMaxValue; // Initializing the speedBarMaxValue variable with the maximum value of the speed bar
        brokenFinishLevelHashCode = Animator.StringToHash("BrokenFinishLevel"); // Coverting the "BrokenFinishLevel" word (animation condition) in a hash of type integer
        numberOfTotalWalls = 85; // Initializing the numberOfTotalWalls variable to 85
        numberOfFreeWalls = 7; // Initializing the numberOfFreeWalls variable to 5
        distanceSwordFinishLevel = 0; // Initializing the distanceSwordFinishLevel variable to 0
        stopBackground = false; // Initializing the stopBackground boolean variable as false
        stopMovement = false; // Initializing the stopCameraMovement boolean variable as false
        brokenFinishLevel = false; // Initializing the brokenFinishLevel boolean variable as false
    }

    // Start is called before the first frame update
    private void Start()
    {
        finishLevelPositionY = -numberOfTotalWalls * gameResolution.cameraDisplacement; // Calculating the position Y of finish level
        transform.position = new Vector3(transform.position.x, finishLevelPositionY, transform.position.z); // Initializing the position Y of finish level
        swordPositionY = swordMovement.swordPositionY; // Calculating the value of the swordPositionY variable with the current sword position in the axis Y
        distanceFinishLevel = CalculateDistance() + cameraFollow.unitsInCameraY; // Calculating the initial distance between the sword and the end of level plus units in camera Y
    }

    // Update is called once per frame
    private void Update()
    {
        swordPositionY = swordMovement.swordPositionY; // Calculating the value of the swordPositionY variable with the current sword position in the axis Y
        distanceSwordFinishLevel = CalculateDistance(); // Calculating the distance between the sword and the end of level
        currentSpeedBarValue = speedBarBehaviour.currentSliderValue; // Calculating the value of the currentSpeedBarValue variable with the current slider value

        // If the distance between the sword and the end of level allows to have the last 7 free walls, then the change background will stop
        if (distanceSwordFinishLevel <= gameResolution.cameraDisplacement * numberOfFreeWalls)
        {
            stopBackground = true; // Setting the stopBackground boolean variable as true
        }
    }

    // OnCollisionEnter2D detects collisions between several objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectCollisionWithSword(collision);
    }

    // OnTriggerEnter2D detects trigger collisions between several objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DetectCollisionWithSword(collision);
    }

    // This method detects the collision between the sword and the end of level, it uses a Collision2D as parameter
    private void DetectCollisionWithSword(Collision2D collision)
    {
        // If the end of level detects a collision with the sword, then it will compare the current speed bar value with the speed bar maximum value and execute several function (depending on the result)
        if (collision.gameObject.layer == 8) // Layer 8 is equal to Player layer
        {
            stopMovement = true; // Setting the stopCameraMovement boolean variable as true
            // If the current speed bar value is greater than speed bar maximum value, then it will display the "Level completed" text on screen and apply the broken finish level animation
            if (currentSpeedBarValue >= speedBarMaxValue)
            {
                myAnimator.SetBool(brokenFinishLevelHashCode, true); // Applying the broken finish level castle animation, depending on a bool parameter
                speedBarBehaviour.gameObject.SetActive(false); // Deactivating the speed bar, when the finish level is broken
                brokenFinishLevel = true; // Setting the brokenFinishLevel boolean variable as true
            } else { // If the current speed bar value does not reach the speed bar maximum value, then the sword will activate the broken sword animation and destroy itself
                speedBarBehaviour.RestartSpeedBar(); // Restarting the speed bar (UI)
                swordBehaviour.ActivateBrokenSwordAnimation(true); // Callling to the ActivateSeveralyDamagedSwordAnimation method to the broken sword animation
            }
        }
    }

    // This method detects the collision between the sword and the end of level, it uses a Collider2D as parameter
    private void DetectCollisionWithSword(Collider2D collision)
    {
        // If the end of level detects a collision with the sword, then it will compare the current speed bar value with the speed bar maximum value and execute several function (depending on the result)
        if (collision.gameObject.layer == 8) // Layer 8 is equal to Player layer
        {
            stopMovement = true; // Setting the stopCameraMovement boolean variable as true
            // If the current speed bar value is greater than speed bar maximum value, then it will display the "Level completed" text on screen and apply the broken finish level animation
            if (currentSpeedBarValue >= speedBarMaxValue)
            {
                myAnimator.SetBool(brokenFinishLevelHashCode, true); // Applying the broken finish level castle animation, depending on a bool parameter
            } else { // If the current speed bar value does not reach the speed bar maximum value, then the sword will activate the broken sword animation and destroy itself
                speedBarBehaviour.RestartSpeedBar(); // Restarting the speed bar (UI)
                swordBehaviour.ActivateBrokenSwordAnimation(true); // Callling to the ActivateSeveralyDamagedSwordAnimation method to the broken sword animation
            }
        }
    }

    // Method to enable or disable the BoxCollider2D of the finish level, this is called from BrokenFinishLevel animation
    private void EnabledFinishLevelCollider(int enabledCollider)
    {
        myBoxCollider2D.enabled = enabledCollider != 0; // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to the enabled property of the BoxCollider2D
    }

    // Method to activate or deactive the broken stones effect of finish level, this is called from BrokenFinishLevel animation
    private void ActivateBrokenStonesEffect(int enabledEffect)
    {
        transform.GetChild(0).gameObject.SetActive(enabledEffect != 0); // Activating the stone particles of finish level objecto, getting a boolean value (0 is false and 1 is true) to activate the game object of button 2
    }

    // Method to destroy the FinishLevel, this is called from BrokenFinishLevel animation
    private void DestroyFinishLevel()
    {
        StartCoroutine(ScheduleDestroyFinishLevel()); // Calling and starting the coroutine to destroy the finish level object after 0.5 seconds
    }

    // This method destroy the finish level object, this is activated after 0.5 seconds
    private IEnumerator ScheduleDestroyFinishLevel()
    {
        yield return new WaitForSeconds(0.5f); // Waiting 0.5 seconds
        Destroy(gameObject); // Destroying the FinishLevel
    }

    /** PUBLIC METHODS **/

    // This method calculates distance between the sword position Y and the final level position Y
    public float CalculateDistance()
    {
        float distance = 0; // Iniatilizing the distance variable to 0

        // If the finish level is not null, then it will calculate the distance between the sword position Y and the final level position Y
        if (this != null)
            distance = swordPositionY - transform.position.y; // Calculating the distance between the sword position Y and the final level position Y
        return distance; // Returning the distance (previously calculated)
    }
}
