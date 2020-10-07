using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class FireEffectBehaviour : MonoBehaviour
{
    // Private variables
    [SerializeField] private SpeedBarBehaviour speedBarBehaviour;
    [SerializeField] private GameResolution gameResolution;
    private ParticleSystem particleSystem;
    private SwordBehaviour swordBehaviour;
    private SwordMovement swordMovement;
    private float normalizedValueSpeedBar;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>(); // Getting all components of type ParticleSystem
        swordBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordBehaviour>(); // Getting all components of type SwordBehaviour
        swordMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordMovement>(); // Getting all components of type SwordMovement
        var main = particleSystem.main; // Being reference to ParticleSystem.main components
        main.startLifetime = 0.5f * gameResolution.gameResolutionScale; // Iniatializing a start life time of ParticleSystem denpending on game resolution scale
        main.startSize = new ParticleSystem.MinMaxCurve(0.1f * gameResolution.gameResolutionScale, 0.15f * gameResolution.gameResolutionScale); // Iniatializing a start size of ParticleSystem denpending on game resolution scale
    }

    // Update is called once per frame
    private void Update()
    {
        // If the sword is broken, then the particle system will destroy
        if (swordBehaviour.brokenSword)
            Destroy(gameObject); // Destroying the particle system

        var emission = particleSystem.emission; // Setting a var variable with the emission of the particle system
        normalizedValueSpeedBar = speedBarBehaviour.sliderSpeedBar.normalizedValue; // Setting the normalized value of the speed bar, this value can be between 0 and 1

        // If the value of speed bar is less than 50%, then the emission of particle system will be 0 particles per second
        if (normalizedValueSpeedBar < 0.5)
        {
            emission.rateOverTime = 0f; // Setting the emission of particle system to 0
            swordMovement.axisY = 0; // Setting the axis Y of sword movement to 0
        } else if (normalizedValueSpeedBar >= 0.5 && normalizedValueSpeedBar < 0.75) { // If the value of speed bar is greater than or equal to 50% and less than 75%, then the emission of particle system will be 10 particles per second
            emission.rateOverTime = 10f; // Setting the emission of particle system to 10
            swordMovement.axisY = -0.25f; // Setting the axis Y of sword movement to -0.25
        } else if (normalizedValueSpeedBar >= 0.75 && normalizedValueSpeedBar < 0.95) { // If the value of speed bar is greater than or equal to 75% and less than 95%, then the emission of particle system will be 25 particles per second
            emission.rateOverTime = 25f; // Setting the emission of particle system to 25
            swordMovement.axisY = -0.50f; // Setting the axis Y of sword movement to -0.50
        } else if (normalizedValueSpeedBar >= 0.95) {// If the value of speed bar is greater than 95%, then the emission of particle system will be 50 particles per second
            emission.rateOverTime = 50f; // Setting the emission of particle system to 50
            swordMovement.axisY = -0.75f; // Setting the axis Y of sword movement to -0.75
        }
    }
}
