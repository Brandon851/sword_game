using System.Collections; // Library called System.Collections, it uses some of classes and functions of C#
using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions
using UnityEngine.UI; // Library called UnityEngine.UI, it is used to all relationated with User Interfaces (UI)

public class StarController : MonoBehaviour
{
    // Private variables
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;
    [SerializeField] private Sprite emptyStar;
    private CanvasBehaviour canvasBehaviour;
    private SwordBehaviour swordBehaviour;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        canvasBehaviour = GameObject.FindGameObjectWithTag("UI").GetComponent<CanvasBehaviour>(); // Getting all components of type CanvasBehaviour
        swordBehaviour = canvasBehaviour.swordBehaviour; // Initializing the sword behaviour with the sword behavior of canvas
    }

    // Method to activate or deactive the star 1 of UI, this is called from IdleLevelCompleted
    private void ActiveStar1(int enabledStar)
    {
        star1.gameObject.SetActive(enabledStar != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of star 1
        StartCoroutine(ActivateStarEffect(star1.transform)); // Calling and starting the coroutine to activate the star effect object after 0.15 seconds
    }

    // Method to activate or deactive the star 2 of UI, this is called from IdleLevelCompleted
    private void ActiveStar2(int enabledStar)
    {
        star2.gameObject.SetActive(enabledStar != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of star 2

        if (!(swordBehaviour.hitCount < 2)) // If hit count is not less than 2, then the sprite of star 2 will change to the sprite of empty star
            star2.sprite = emptyStar; // Changing the sprite of star 2 to the sprite of empty star
        else // If hit count is less than 2, then the star effect will activate
            StartCoroutine(ActivateStarEffect(star2.transform)); // Calling and starting the coroutine to activate the star effect object after 0.15 seconds
    }

    // Method to activate or deactive the star 3 of UI, this is called from IdleLevelCompleted
    private void ActiveStar3(int enabledStar)
    {
        star3.gameObject.SetActive(enabledStar != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of star 3

        if (!(swordBehaviour.hitCount < 1) && canvasBehaviour != null) // If hit count is not less than 1, then the sprite of star 3 will change to the sprite of empty star
            star3.sprite = emptyStar; // Changing the sprite of star 3 to the sprite of empty star
        else // If hit count is less than 2, then the star effect will activate
            StartCoroutine(ActivateStarEffect(star3.transform)); // Calling and starting the coroutine to activate the star effect object after 0.15 seconds
    }

    // This method activates the star effect which is the child of the star, this is activated after 0.15 seconds
    private IEnumerator ActivateStarEffect(Transform starParent)
    {
        yield return new WaitForSeconds(0.15f); // Waiting 0.15 seconds
        starParent.GetChild(0).gameObject.SetActive(true); // Select and activate the first child of star, which is star effect
    }

}
