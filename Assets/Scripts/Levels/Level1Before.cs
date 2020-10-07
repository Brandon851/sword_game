using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class Level1Before : MonoBehaviour
{
    // Private variables
    [SerializeField] private GameResolution gameResolution;
    [SerializeField] private SpeedBarBehaviour speedBarBehaviour;
    [SerializeField] private GameObject rewardsParent;
    [SerializeField] private GameObject obstaclesParent;
    [SerializeField] private GameObject prefabSpeedCharger;
    [SerializeField] private GameObject[] prefabsBricks;
    private CameraFollow cameraFollow;
    private SwordMovement swordMovement;
    private FinishLevelController finishLevelController;
    private float totalDistance;
    private float initializationDistanceX;
    private float initializationDistanceY;
    private float currentDistance;
    private float swordPositionY;
    private float distancePerTake;
    private int cameraTakesCount;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
        swordMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordMovement>(); // Getting all components of type SwordMovement
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        totalDistance = speedBarBehaviour.distanceSwordFinishLevel; // Initializing the final distance between the sword and the end of level
        initializationDistanceX = cameraFollow.unitsInCameraX / 4; // Initializing the initializationDistanceX variable which is the size in the axis X of the camera take of middle
        initializationDistanceY = cameraFollow.unitsInCameraY; // Initializing the initializationDistanceY variable which is the size in the axis Y of the camera take, this is the distance for initialization of the obstacles or rewards
        Debug.Log(initializationDistanceY);
        Debug.Log(totalDistance);
        cameraTakesCount = 0; // Initializing the camera take count to 0
        prefabsBricks[0].transform.localScale = new Vector3(0.8f, 0.8f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 1, multiplying its original size by game resolution scale
        prefabsBricks[1].transform.localScale = new Vector3(0.68f, 0.8f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 2, multiplying its original size by game resolution scale
        prefabsBricks[2].transform.localScale = new Vector3(0.6f, 0.76f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 3, multiplying its original size by game resolution scale
        prefabsBricks[3].transform.localScale = new Vector3(0.48f, 0.76f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 4, multiplying its original size by game resolution scale
    }

    // Update is called once per frame
    private void Update()
    {
        currentDistance = finishLevelController.CalculateDistance(); // Calculating the current distance between the sword and the end of level
        swordPositionY = swordMovement.swordPositionY; // Calculating the value of the swordPositionY variable with the current sword position in the axis Y
        distancePerTake = initializationDistanceY * cameraTakesCount; // Calculating the distance per take, multiplying initialization distance by the number of takes

        //Debug.Log(currentDistance + " <= " + (totalDistance - distancePerTake));
        //Debug.Log(cameraTakesCount);
        // If 
        if (currentDistance <= (totalDistance - distancePerTake))
        {
            switch(cameraTakesCount) // Choose a take and generate the bricks or speed chargers
            {
                case 0: // Instantiating 2 bricks in the first take
                    (Instantiate(prefabsBricks[0], new Vector3(initializationDistanceX * 0,  swordPositionY - (initializationDistanceY * 0.5f), -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform; 
                    (Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.61f, swordPositionY - initializationDistanceY * 1.1f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 1: // Instantiating 2 bricks in the second take
                    (Instantiate(prefabsBricks[1], new Vector3(prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * -1, swordPositionY - (initializationDistanceY * 0.5f), -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3((initializationDistanceX - prefabsBricks[3].transform.localScale.x * 2) * 0.33f, swordPositionY - (initializationDistanceY * 1.1f), -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 2: // Instantiating 2 bricks in the third take
                    (Instantiate(prefabsBricks[3], new Vector3(-8, swordPositionY - (initializationDistanceY * 0.65f), -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[0], new Vector3(2, swordPositionY - distancePerTake - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 3: // Instantiating 2 bricks and a speed charger in the fourth take
                    (Instantiate(prefabsBricks[1], new Vector3(13, swordPositionY - (initializationDistanceY * 0.65f), -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabSpeedCharger, new Vector3(8, swordPositionY - distancePerTake - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(-4, swordPositionY - distancePerTake - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 4: // Instantiating a brick in the fifth take
                    (Instantiate(prefabsBricks[2], new Vector3(0, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 5: // Instantiating 2 bricks in the sixth take
                    (Instantiate(prefabsBricks[0], new Vector3(12, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(-12, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 6: // Instantiating 2 bricks in the seventh take
                    (Instantiate(prefabsBricks[1], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(8, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 7: // Instantiating 2 bricks in the eighth take
                    (Instantiate(prefabsBricks[2], new Vector3(-11, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[1], new Vector3(3, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 8: // Instantiating 2 bricks and a speed charger in the ninth take
                    (Instantiate(prefabsBricks[0], new Vector3(11, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabSpeedCharger, new Vector3(0, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(-7, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 9: // Instantiating 2 bricks in the tenth take
                    (Instantiate(prefabsBricks[1], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(-13, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 10: // Instantiating 2 bricks in the eleventh take
                    (Instantiate(prefabsBricks[2], new Vector3(6, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[0], new Vector3(-6, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 11: // Instantiating 2 bricks in the twelfth take
                    (Instantiate(prefabsBricks[1], new Vector3(3, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(-10, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 12: // Instantiating 2 bricks and a speed charger in the thirteenth take
                    (Instantiate(prefabsBricks[0], new Vector3(12, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabSpeedCharger, new Vector3(-10, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(0, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 13: // Instantiating 2 bricks in the fourteenth take
                    (Instantiate(prefabsBricks[1], new Vector3(-8, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(8, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 14: // Instantiating 2 bricks in the fifteenth take
                    (Instantiate(prefabsBricks[0], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(10, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 15: // Instantiating 2 bricks in the sixteenth take
                    (Instantiate(prefabsBricks[1], new Vector3(-9, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(4, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 16: // Instantiating 2 bricks and a speed charger in the seventeenth take
                    (Instantiate(prefabsBricks[1], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabSpeedCharger, new Vector3(-11, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(8, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 17: // Instantiating 2 bricks in the eighteenth take
                    (Instantiate(prefabsBricks[3], new Vector3(-11, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[0], new Vector3(4, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 18: // Instantiating 2 bricks in the nineteenth take
                    (Instantiate(prefabsBricks[1], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(12, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 19: // Instantiating 2 bricks in the twentieth take
                    (Instantiate(prefabsBricks[0], new Vector3(-7, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(5, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 20: // Instantiating 2 bricks in the twenty first take
                    (Instantiate(prefabsBricks[3], new Vector3(-10, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[1], new Vector3(10, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 21: // Instantiating 2 bricks in the twenty second take
                    (Instantiate(prefabsBricks[2], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(-8, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 22: // Instantiating 2 bricks and a speed charger in the twenty third take
                    (Instantiate(prefabsBricks[0], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabSpeedCharger, new Vector3(-8, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(8, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 23: // Instantiating 2 bricks in the twenty fourth take
                    (Instantiate(prefabsBricks[1], new Vector3(-5, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(3, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 24: // Instantiating 2 bricks in the twenty fifth take
                    (Instantiate(prefabsBricks[0], new Vector3(2, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(12, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 25: // Instantiating 2 bricks in the twenty sixth take
                    (Instantiate(prefabsBricks[2], new Vector3(-12, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[1], new Vector3(6, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 26: // Instantiating 2 bricks in the twenty seventh take
                    (Instantiate(prefabsBricks[0], new Vector3(9, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[1], new Vector3(-8, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 27: // Instantiating 2 bricks and a speed charger in the twenty eighth take
                    (Instantiate(prefabsBricks[0], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabSpeedCharger, new Vector3(-3, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(12, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 28: // Instantiating 2 bricks in the twenty ninth take
                    (Instantiate(prefabsBricks[1], new Vector3(0, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(-13, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 29: // Instantiating 2 bricks in the thirtieth take
                    (Instantiate(prefabsBricks[0], new Vector3(10, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(5, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 30: // Instantiating 2 bricks in the thirty first take
                    (Instantiate(prefabsBricks[1], new Vector3(10, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[3], new Vector3(-10, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 31: // Instantiating 2 bricks in the thirty second take
                    (Instantiate(prefabsBricks[2], new Vector3(-12, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[0], new Vector3(0, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 32: // Instantiating 2 bricks and a speed charger in the thirty third take
                    (Instantiate(prefabsBricks[3], new Vector3(13, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabSpeedCharger, new Vector3(0, swordPositionY - initializationDistanceY - 4, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
                    (Instantiate(prefabsBricks[1], new Vector3(-9, swordPositionY - initializationDistanceY - 9, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 33: // Instantiating 2 bricks in the thirty fourth take
                    (Instantiate(prefabsBricks[3], new Vector3(5, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[0], new Vector3(-11, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;

                case 34: // Instantiating 2 bricks in the thirty fifth take
                    (Instantiate(prefabsBricks[0], new Vector3(11, swordPositionY - initializationDistanceY, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    (Instantiate(prefabsBricks[2], new Vector3(-12, swordPositionY - initializationDistanceY - 6, -1) * 1.2f, Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
                    break;
            }
            cameraTakesCount++; // Adding a take to the camera takes count
        }
    }
}
