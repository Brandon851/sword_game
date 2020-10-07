using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions
using UnityEngine.SceneManagement; // Library called UnityEngine.SceneManagement, it is used to all relationated with Scene Management

public class GameController : MonoBehaviour
{
    // Private variable
    private Scene currentScene;
    private GameObject target;
    private string nameScene;

    /** PRIVATE METHODS **/

    // Update is called once per frame
    private void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        nameScene = currentScene.name;

        // If you press the Escape key, the game will quit
        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame(); // Calling the QuitGame method
        }

        // If you press the R key, the game will restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel(); // Calling the RestartLevel method
        }
    }

    // This method restarts the level
    private void RestartLevel()
    {
        SceneManager.LoadScene(nameScene, LoadSceneMode.Single); // Restarting the current scene
    }

    // This method quits the game or stops the play mode
    private void QuitGame()
    {
        #if UNITY_EDITOR // If you are in the Unity Editor, then the editor will stop play mode  
            UnityEditor.EditorApplication.isPlaying = false; // Stoping the play mode
        #else // If you are in another platform, then the game will quit
            Application.Quit(); // Quitting or closing the game
        #endif
    }

    // This method does not destroy an object from the current scene, when game is on load. This finds the object by its name
    private void DontDestroyObjectByName(string nameObject)
    {
        target = GameObject.Find(nameObject); // Finding a game object from the current scene by its name

        DontDestroyOnLoad(target); // Do not destroy the object that was passed as parameter
    }

    // This method deactivates a child object by its name, this depends on string parameter
    private void DeactivateChildObjectByName(string nameChildObject)
    {
        if(target != null) // If the target is not null, then the child object will deactivate and finding by its name
            target.transform.Find(nameChildObject).gameObject.SetActive(false); // Deactivating and finding the child object by its name
    }

    // This method destroys an object which was initialized in the DontDestroyObjectByName method
    private void DestroyObject()
    {
        if (target != null) // If the target is not null, then the object will destroy
            Destroy(target); // Destroying the object
    }
}
