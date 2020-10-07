using UnityEngine; // Library called UnityEngine, it uses most of classes and functions of Unity

public class SwordBehaviour : MonoBehaviour
{
    // Proporties
    public bool brokenSword { get; private set; }
    public int hitCount { get; private set; }
    public Rigidbody2D myRigidbody2D { get; private set; }

    // Private variables
    [SerializeField] private SpeedBarBehaviour speedBarBehaviour;
    private CameraFollow cameraFollow;
    private Animator myAnimator;
    private EdgeCollider2D myEdgeCollider2D;
    private SpriteRenderer mySpriteRenderer;
    private SwordMovement mySwordMovement;
    private int brokenHashCode;
    private int slightlyDamagedHashCode;
    private int severalyDamagedHashCode;

    /** PRIVATE METHODS **/

    // Awake can initialize variables only once per execution and can work inactivated
    private void Awake()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
        myAnimator = GetComponent<Animator>(); // Getting all components of type Animator
        myEdgeCollider2D = GetComponent<EdgeCollider2D>(); // Getting all components of type EdgeCollider2D
        mySpriteRenderer = GetComponent<SpriteRenderer>(); // Getting all components of type SpriteRenderer
        mySwordMovement = GetComponent<SwordMovement>(); // Getting all components of type SwordMovement
        myRigidbody2D = GetComponent<Rigidbody2D>(); // Getting all components of type Rigidbody2D
        brokenHashCode = Animator.StringToHash("Broken"); // Converting the "Broken" word (animation condition) in a hash of type integer
        slightlyDamagedHashCode = Animator.StringToHash("SlightlyDamaged"); // Converting the "SlightlyDamaged" word (animation condition) in a hash of type integer
        severalyDamagedHashCode = Animator.StringToHash("SeveralyDamaged"); // Converting the "SeveralyDamaged" word (animation condition) in a hash of type integer
        hitCount = 0; // Initializing the hitCount variable to 0
        brokenSword = false; // Initializing the brokenSword boolean variable as false
    }

    // Update is called once per frame
    private void Update()
    {
        // If the sword is outside of range of the camera, then the sword will destroy itself
        if (transform.position.y < (cameraFollow.transform.position.y - cameraFollow.unitsInCameraY * 2 / 3))
        {
            DestroySword(); // Calling to the DestroySword method to destroy the sword
        }
    }

    // OnCollisionEnter2D detects collisions between several objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the sword collides with some obstacle, then it will make some of its animations and reduce the speed bar (UI)
        if (collision.gameObject.layer == 9) // Layer 9 is equal to Obstacle layer
        {
            hitCount++; // Adding a hit to the hit count
            if (hitCount == 1) // If it is the first collision of the sword, then it will apply the slightly damaged sword animation
            {
                ActivateSlightlyDamagedSwordAnimation(true); // Callling to the ActivateSlightlyDamagedSwordAnimation method to activate the slightly damaged sword animation
            } else if (hitCount == 2) { // If it is the second collision of the sword, then it will apply the severaly damaged sword animation
                ActivateSeveralyDamagedSwordAnimation(true); // Callling to the ActivateSeveralyDamagedSwordAnimation method to activate the severaly damaged sword animation
            } else if (hitCount == 3) { // If it is the third and last collision of the sword, then it will restart the speed bar (UI) and apply the broken sword animation
                speedBarBehaviour.RestartSpeedBar(); // Restarting the speed bar (UI)
                ActivateBrokenSwordAnimation(true); // Callling to the ActivateSeveralyDamagedSwordAnimation method to the broken sword animation
            }

            speedBarBehaviour.DecreaseSpeed(); // Decreasing the speed bar (UI) with the penalty
        }
    }

    // OnTriggerEnter2D detects trigger collisions between several objects
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the sword collides with some speed charger (reward), then it will destroy the speed charger and increase the speed bar (UI)
        if (collision.gameObject.layer == 10) // Layer 10 is equal to Reward layer
        {
            Destroy(collision.gameObject); // Destroying the speed charger
            speedBarBehaviour.IncreaseSpeed(); // Increasing the speed bar (UI) with the reward
        }
    }

    // Method to enable or disable the trigger component of the sword, this can be called from TransitionSlightlyDamagedSword or TransitionSeveralyDamagedSword animation
    private void IsTriggerSwordCollider(int enabledTrigger)
    {
        myEdgeCollider2D.isTrigger = enabledTrigger != 0; // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to the trigger property of the EdgeCollider2D
    }

    // Method to enable or disable the SpriteRenderer of the sword, this can be called from TransitionSlightlyDamagedSword or TransitionSeveralyDamagedSword animation
    private void EnabledSwordSprite(int enabledSprite)
    {
        mySpriteRenderer.enabled = enabledSprite != 0; // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to the enabled property of the SpriteRenderer
    }

    // Method to enable or disable the SwordMovement script of the sword, this is called from BrokenSword animation
    private void EnabledSwordMovement(int enabledMovement)
    {
        mySwordMovement.enabled = enabledMovement != 0; // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to the enabled property of the SwordMovement
    }

    // Method to change RigidbodyType of the sword, this is called from BrokenSword animation
    public void SwordRigidbodyType(RigidbodyType2D typeRigidbody)
    {
        myRigidbody2D.bodyType = typeRigidbody; // Depending on the parameter value, getting a value of type rigidbody (Dynamic, Kinematic or Static) to apply it in the bodyType property of the Rigidbody2D
    }

    // Method to destroy the sword, this is called from SpeedBarBehaviour script
    private void DestroySword()
    {
        Destroy(gameObject); // Destroying the sword
        brokenSword = true; // Setting the brokenSword boolean variable as true
    }

    /** PUBLIC METHODS **/

    // Method to activate the SlightlyDamagedSword animation
    public void ActivateSlightlyDamagedSwordAnimation(bool activated)
    {
        myAnimator.SetBool(slightlyDamagedHashCode, activated); // Applying the slightly damaged sword animation, depending on a bool parameter
    }

    // Method to activate the SeveralyDamagedSword animation
    public void ActivateSeveralyDamagedSwordAnimation(bool activated)
    {
        myAnimator.SetBool(severalyDamagedHashCode, activated); // Applying the severaly damaged sword animation, depending on a bool parameter
    }

    // Method to activate the BrokenSword animation
    public void ActivateBrokenSwordAnimation(bool activated)
    {
        myAnimator.SetBool(brokenHashCode, activated); // Applying the broken sword animation, depending on a bool parameter
    }
}
