using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class ButtonController : MonoBehaviour
{
    // Private variables
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;

    /** PRIVATE METHODS **/

    // Method to activate or deactive the button 1 of UI, this can be called from IdleGameOver or IdleLevelCompleted
    private void ActiveButton1(int enabledButton)
    {
        button1.SetActive(enabledButton != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of button 1
    }

    // Method to activate or deactive the button 2 of UI, this can be called from IdleGameOver or IdleLevelCompleted
    private void ActiveButton2(int enabledButton)
    {
        button2.SetActive(enabledButton != 0); // Depending on the parameter value, getting a boolean value (0 is false and 1 is true) to activate the game object of button 2
    }
}
