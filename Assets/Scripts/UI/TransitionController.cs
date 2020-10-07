using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions
using UnityEngine.UI; // Library called UnityEngine.UI, it is used to all relationated with User Interfaces (UI)

public class TransitionController : MonoBehaviour
{
    // Private variables
    [SerializeField] private GameObject restartTransition;
    [SerializeField] private GameObject nextTransition;
    [SerializeField] private GameObject menuTransition;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button menuButton;
    private Animator myAnimator;
    private int disappearUpHashCode;
    private int disappearDownHashCode;
    private int disappearRightHashCode;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        myAnimator = GetComponent<Animator>(); // Getting all components of type Animator
        disappearUpHashCode = Animator.StringToHash("DisappearUp"); // Converting the "DisappearUp" word (animation condition) in a hash of type integer
        disappearDownHashCode = Animator.StringToHash("DisappearDown"); // Converting the "DisappearDown" word (animation condition) in a hash of type integer
        disappearRightHashCode = Animator.StringToHash("DisappearRight"); // Converting the "DisappearRight" word (animation condition) in a hash of type integer
        restartButton.onClick.AddListener(ActivateDisappearUpAnimation); // Adding the ActivateDisappearUpAnimation method to button of restart
        nextButton.onClick.AddListener(ActivateDisappearRightAnimation); // Adding the ActivateDisappearRightAnimation method to button of restart
        menuButton.onClick.AddListener(ActivateDisappearDownAnimation); // Adding the ActivateDisappearDownAnimation method to button of menu
    }

    // Method to activate or deactive the restart transition of UI, this can be called from DisappearUpScreen animation
    private void ActiveRestartTransition(int enabledButton)
    {
        restartTransition.SetActive(enabledButton != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of the restart transition restart
    }

    // Method to activate or deactive the next transition of UI, this can be called from DisappearRightScreen animation
    private void ActiveNextTransition(int enabledButton)
    {
        nextTransition.SetActive(enabledButton != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of the next transition restart
    }

    // Method to activate or deactive the menu transition of UI, this can be called from DisappearDownScreen animation
    private void ActiveMenuTransition(int enabledButton)
    {
        menuTransition.SetActive(enabledButton != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of the menu transition restart
    }

    // Method to activate the disappear up animation of screen
    private void ActivateDisappearUpAnimation()
    {
        myAnimator.SetBool(disappearUpHashCode, true); // Applying the disappear up screen animation, depending on a bool parameter
    }

    // Method to activate the disappear down animation of screen
    private void ActivateDisappearDownAnimation()
    {
        myAnimator.SetBool(disappearDownHashCode, true); // Applying the disappear down screen animation, depending on a bool parameter
    }

    // Method to activate the disappear right animation of screen
    private void ActivateDisappearRightAnimation()
    {
        myAnimator.SetBool(disappearRightHashCode, true); // Applying the disappear right screen animation, depending on a bool parameter
    }
}
