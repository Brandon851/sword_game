using System.Collections; // Library called System.Collections, it uses some of classes and functions of C#
using UnityEngine; // Library called UnityEngine, it uses most of classes and functions of Unity
using TMPro; // Library called TMPro, it uses all classes and proporties of TextMeshPro

public class FPSCount : MonoBehaviour
{
    // Private variables
    private TextMeshProUGUI fpsText;
    private float frequency = 0.25f;
    private string fps;

    // Start is called before the first frame update
    void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>(); // Getting all components of type TextMeshPro
        StartCoroutine(FPS()); // Calling and starting the coroutine to count the FPS
    }

    // This method counts and sets the FPS of game
    private IEnumerator FPS()
    {
        for (; ; )
        {
            // Capture frame-per-second
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;

            // Display it
            fps = string.Format("FPS: {0}", Mathf.RoundToInt(frameCount / timeSpan));
            fpsText.text = fps;
        }
    }
}
