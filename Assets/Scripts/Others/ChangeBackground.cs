using UnityEngine; // Library called UnityEngine, it uses most of classes and functions of Unity

public class ChangeBackground : MonoBehaviour
{
    // Private variables
    [SerializeField] private GameResolution gameResolution;
    [SerializeField] private GameObject backgroundParent;
    [SerializeField] private float earthquakeScale;
    [SerializeField] private GameObject[] walls;
    [SerializeField] private int[] frequencyOfWalls;
    private int[] randomNumbers;
    private int[] numberOfWalls;
    private CameraFollow cameraFollow;
    private FinishLevelController finishLevelController;
    private int indexRandomNumbers;
    private int lengthWalls;
    private int randomNumber;
    private float cameraDisplacementDistance;
    private float wallPositionY;
    private float cameraPositionY;
    private float auxCameraPositionY;
    private float auxEarthquakeScale;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        cameraPositionY = cameraFollow.transform.position.y; // Initializing the cameraPositionY variable with the camera position in the axis Y
        lengthWalls = walls.Length; // Initializing the lengthWalls variable with the length of walls array
        numberOfWalls = new int[lengthWalls]; // Creating an array where the dimension depends on length of the walls array
        randomNumbers = new int[2]; // Creating an array of 2 dimensions which will store the last 2  generated random numbers
        cameraDisplacementDistance = 8.4f * gameResolution.gameResolutionScale; // Initializing and calculating the cameraDisplacementDistance variable, multiplying 8.4 by game resolution scale
        wallPositionY = gameResolution.wallPositionYConstant * gameResolution.gameResolutionScale; // Initializing and calculating the wallPositionY variable, multiplying wall position Y constant by game resolution scale
        earthquakeScale *= gameResolution.gameResolutionScale; // Calculating the new scale of earthquake, multiplying it by tha game resolution scale
        indexRandomNumbers = 0; // Initializing the indexRandomNumbers variable to 0
        auxEarthquakeScale = 0; // Initializing the auxEarthquakeScale variable to 0

        for (int i = 0; i < lengthWalls; i++) // This loop will fill the numberOfWalls array with numbers from 0 to length of walls array minus one
        {
            numberOfWalls[i] = i; // Filling the numberOfWalls array with numbers from 0 to length of walls array minus one
            walls[i].transform.localScale = new Vector3(2.7f, 2.8f, 1) * gameResolution.gameResolutionScale; // Changing the size of all walls, multiplying the original size by game resolution scale
        }
    }

    // Update is called once per frame
    void Update()
    {
        EarthquakeWalls(); // Calling to Earthqueake method to simulate a earthqueake in the walls

        auxCameraPositionY = cameraFollow.cameraPositionY; // Calculating the value of the auxCameraPositionY variable with the current camera position in the axis Y

        // If the camera is recording a wall, then the wall that is on top of the camera will move until below of all walls and change its background randomly
        if ((cameraPositionY - cameraDisplacementDistance) >= auxCameraPositionY)
        {
            cameraPositionY -= cameraDisplacementDistance; // Updating cameraPositionY, substraction the camera displacement to itself
            randomNumber = MyRand(numberOfWalls, frequencyOfWalls, lengthWalls); // Getting a random number between 0 and length of walls array
            randomNumbers[indexRandomNumbers] = randomNumber; // Storing the random number (previously selected) to randomNumbers array

            wallPositionY -= cameraDisplacementDistance; // Substracting the camera displacement distance to wallPositionY vairable

            // If the background does not stop, change random number if it is equal to previous one
            if (!finishLevelController.stopBackground)
            {
                // If it is the first index of random numbers, then compare it with the last random number stored in the randomNumbers array
                if (indexRandomNumbers == 0)
                {
                    // This loop will repeat as long as the current random number is different to the last random number stored in the randomNumbers array
                    while (randomNumber == randomNumbers[randomNumbers.Length - 1])
                        randomNumber = MyRand(numberOfWalls, frequencyOfWalls, lengthWalls); // If it is necessary, generate another random number
                } else { // If it is not the first index of random numbers, then compare it with the previous random number stored in the randomNumbers array
                    while (randomNumber == randomNumbers[indexRandomNumbers - 1]) // This loop will repeat as long as the current random number is different to the previous random number stored in the randomNumbers array
                        randomNumber = MyRand(numberOfWalls, frequencyOfWalls, lengthWalls); // If it is necessary, generate another random number
                }
                randomNumbers[indexRandomNumbers] = randomNumber; // Storing the random number (previously selected and verified) to randomNumbers array 
                (Instantiate(walls[randomNumber], new Vector3(0, wallPositionY, 2), Quaternion.identity) as GameObject).transform.parent = backgroundParent.transform; // Instantiating a random wall below the last wall as child of background parent
            } else { // If the background stop, then it will change all backgrounds on the walls to the first background (sprite by default)
                (Instantiate(walls[0], new Vector3(0, wallPositionY, 2), Quaternion.identity) as GameObject).transform.parent = backgroundParent.transform; // Instantiating the first wall below the last wall as child of background parent
            }

            Destroy(backgroundParent.transform.GetChild(0).gameObject); // Destroying the first child object of background parent

            indexRandomNumbers++; // Adding a index to the index of random numbers count
            if (indexRandomNumbers >= randomNumbers.Length) // If index of random numbers reaches the maximum number of indexes, then index of random numbers will restart to 0
                indexRandomNumbers = 0; // Restarting the index of random numbers to 0
        }
    }

    // Utility function to find ceiling of r in arr[l..h]
    private int FindCeil(int[] arr, int r, int l, int h)
    {
        int mid;
        while (l < h)
        {
            mid = (l + h) / 2;
            if (r > arr[mid])
                l = mid + 1;
            else
                h = mid;
        }
        return (arr[l] >= r) ? l : -1; // If in only a line
    }

    // Choose a random number depending on its frequency
    private int MyRand(int[] arr, int[] freq, int n)
    {
        // Create and fill prefix array
        int[] prefix = new int[n];
        prefix[0] = freq[0];
        for (int i = 1; i < n; ++i)
            prefix[i] = prefix[i - 1] + freq[i]; // prefix[n-1] is sum of all frequencies

        // Generate a random number with value from 1 to this sum
        int r = Random.Range(1, prefix[n - 1] + 1);

        // Find index of ceiling of r in prefix arrat
        int indexc = FindCeil(prefix, r, 0, n - 1);
        return arr[indexc];
    }

    // Simulate a little earthquake in the walls
    private void EarthquakeWalls()
    {
        // If earthquake scale is negative or positive, then it will store this value in an auxiliary earthquake scale and the original earthquake scale will restart to 0
        if (earthquakeScale > 0 || earthquakeScale < 0)
        {
            auxEarthquakeScale = earthquakeScale; // Setting the auxiliar earthquake scale with the current earthquake scale
            earthquakeScale = 0; // Restarting the earthquake scale to 0
        } else { // If earthquake scale is 0, then it will reset with the opposite value of auxiliary earthquake scale
            earthquakeScale = -auxEarthquakeScale; // Setting the earthquake scale with the negative value of auxiliar earth scale
        }

        backgroundParent.transform.position = new Vector3(earthquakeScale, backgroundParent.transform.position.y, 1); // Adding the earthquake scale in the position X and keeping the original position Y on background parent
    }
}