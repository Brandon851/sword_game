using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions

public class Level1 : MonoBehaviour
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
    private int cameraTakesCount;
    private float totalDistance;
    private float initializationDistanceX;
    private float initializationDistanceY;
    private float currentDistance;
    private float swordPositionY;
    private float distancePerTake;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>(); // Getting all components of type CameraFollow
        swordMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<SwordMovement>(); // Getting all components of type SwordMovement
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        initializationDistanceX = cameraFollow.unitsInCameraX / 2; // Initializing the initializationDistanceX variable which is the size in the axis X of the camera take of middle
        initializationDistanceY = cameraFollow.unitsInCameraY; // Initializing the initializationDistanceY variable which is the size in the axis Y of the camera take, this is the distance for initialization of the obstacles or rewards
        totalDistance = finishLevelController.distanceFinishLevel - initializationDistanceY / 16; // Initializing the final distance between the sword and the end of level
        distancePerTake = totalDistance / 35; // Calculating the distance per take, dividing the distance by the number of takes (in this case are 35 takes)
        prefabsBricks[0].transform.localScale = new Vector3(0.8f, 0.8f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 1, multiplying its original size by game resolution scale
        prefabsBricks[1].transform.localScale = new Vector3(0.68f, 0.8f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 2, multiplying its original size by game resolution scale
        prefabsBricks[2].transform.localScale = new Vector3(0.6f, 0.76f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 3, multiplying its original size by game resolution scale
        prefabsBricks[3].transform.localScale = new Vector3(0.48f, 0.76f, 1) * gameResolution.gameResolutionScale; // Initializing the prefab brick 4, multiplying its original size by game resolution scale
        prefabSpeedCharger.transform.localScale = new Vector3(1f, 1f, 1f) * gameResolution.gameResolutionScale; // Initializing the prefab speed charger, multiplying its original size by game resolution scale 
        cameraTakesCount = 0; // Initializing the camera take count to 0
    }

    // Update is called once per frame
    private void Update()
    {
        currentDistance = finishLevelController.CalculateDistance(); // Calculating the current distance between the sword and the end of level
        swordPositionY = swordMovement.swordPositionY; // Calculating the value of the swordPositionY variable with the current sword position in the axis Y

        if (currentDistance <= totalDistance - distancePerTake && currentDistance > totalDistance - 2 * distancePerTake && cameraTakesCount != 1)
        {
            cameraTakesCount = 1;
            (Instantiate(prefabsBricks[0], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY * 0.75f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 0.61f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 2 * distancePerTake && currentDistance > totalDistance - 3 * distancePerTake && cameraTakesCount != 2) {
            cameraTakesCount = 2;
            (Instantiate(prefabsBricks[1], new Vector3(prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * -1, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            //(Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.33f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 3 * distancePerTake && currentDistance > totalDistance - 4 * distancePerTake && cameraTakesCount != 3) {
            cameraTakesCount = 3;
            (Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -0.61f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.15f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 4 * distancePerTake && currentDistance > totalDistance - 5 * distancePerTake && cameraTakesCount != 4) {
            cameraTakesCount = 4;
            //(Instantiate(prefabsBricks[1], new Vector3(-prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * 1, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabSpeedCharger, new Vector3(-prefabSpeedCharger.transform.localScale.x * 2 + initializationDistanceX * 0.61f, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -0.3f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 5 * distancePerTake && currentDistance > totalDistance - 6 * distancePerTake && cameraTakesCount != 5) {
            cameraTakesCount = 5;
            (Instantiate(prefabsBricks[2], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 6 * distancePerTake && currentDistance > totalDistance - 7 * distancePerTake && cameraTakesCount != 6) {
            cameraTakesCount = 6;
            (Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.92f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -0.92f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 7 * distancePerTake && currentDistance > totalDistance - 8 * distancePerTake && cameraTakesCount != 7) {
            cameraTakesCount = 7;
            //(Instantiate(prefabsBricks[1], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 0.61f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 8 * distancePerTake && currentDistance > totalDistance - 9 * distancePerTake && cameraTakesCount != 8) {
            cameraTakesCount = 8;
            (Instantiate(prefabsBricks[2], new Vector3(prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * -0.84f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[1], new Vector3(-prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * 0.23f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 9 * distancePerTake && currentDistance > totalDistance - 10 * distancePerTake && cameraTakesCount != 9) {
            cameraTakesCount = 9;
            (Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.84f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabSpeedCharger, new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
            //(Instantiate(prefabsBricks[2], new Vector3(prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * -0.53f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 10 * distancePerTake && currentDistance > totalDistance - 11 * distancePerTake && cameraTakesCount != 10) {
            cameraTakesCount = 10;
            (Instantiate(prefabsBricks[1], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -1, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 11 * distancePerTake && currentDistance > totalDistance - 12 * distancePerTake && cameraTakesCount != 11) {
            cameraTakesCount = 11;
            //(Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.46f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[0], new Vector3(prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * -0.46f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 12 * distancePerTake && currentDistance > totalDistance - 13 * distancePerTake && cameraTakesCount != 12) {
            cameraTakesCount = 12;
            (Instantiate(prefabsBricks[1], new Vector3(-prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * 0.23f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * -0.76f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 13 * distancePerTake && currentDistance > totalDistance - 14 * distancePerTake && cameraTakesCount != 13) {
            cameraTakesCount = 13;
            //(Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.92f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabSpeedCharger, new Vector3(prefabSpeedCharger.transform.localScale.x * 2 + initializationDistanceX * -0.77f, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 14 * distancePerTake && currentDistance > totalDistance - 15 * distancePerTake && cameraTakesCount != 14) {
            cameraTakesCount = 14;
            (Instantiate(prefabsBricks[1], new Vector3(prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * -0.61f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 0.61f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 15 * distancePerTake && currentDistance > totalDistance - 16 * distancePerTake && cameraTakesCount != 15) {
            cameraTakesCount = 15;
            (Instantiate(prefabsBricks[0], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY * 0.75f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            //(Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.77f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 16 * distancePerTake && currentDistance > totalDistance - 17 * distancePerTake && cameraTakesCount != 16) {
            cameraTakesCount = 16;
            (Instantiate(prefabsBricks[1], new Vector3(prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * -0.69f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 0.31f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 17 * distancePerTake && currentDistance > totalDistance - 18 * distancePerTake && cameraTakesCount != 17) {
            cameraTakesCount = 17;
            (Instantiate(prefabsBricks[1], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabSpeedCharger, new Vector3(prefabSpeedCharger.transform.localScale.x * 2 + initializationDistanceX * -0.85f, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
            //(Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.61f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 18 * distancePerTake && currentDistance > totalDistance - 19 * distancePerTake && cameraTakesCount != 18) {
            cameraTakesCount = 18;
            (Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -0.84f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.31f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 19 * distancePerTake && currentDistance > totalDistance - 20 * distancePerTake && cameraTakesCount != 19) {
            cameraTakesCount = 19;
            //(Instantiate(prefabsBricks[1], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.92f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 20 * distancePerTake && currentDistance > totalDistance - 21 * distancePerTake && cameraTakesCount != 20) {
            cameraTakesCount = 20;
            (Instantiate(prefabsBricks[0], new Vector3(prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * -0.54f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.38f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 21 * distancePerTake && currentDistance > totalDistance - 22 * distancePerTake && cameraTakesCount != 21) {
            cameraTakesCount = 21;
            (Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -0.77f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            //(Instantiate(prefabsBricks[1], new Vector3(-prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * 0.77f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 22 * distancePerTake && currentDistance > totalDistance - 23 * distancePerTake && cameraTakesCount != 22) {
            cameraTakesCount = 22;
            (Instantiate(prefabsBricks[2], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * -0.61f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 23 * distancePerTake && currentDistance > totalDistance - 24 * distancePerTake && cameraTakesCount != 23) {
            cameraTakesCount = 23;
            //(Instantiate(prefabsBricks[0], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabSpeedCharger, new Vector3(prefabSpeedCharger.transform.localScale.x * 2 + initializationDistanceX * -0.61f, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.61f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 24 * distancePerTake && currentDistance > totalDistance - 25 * distancePerTake && cameraTakesCount != 24) {
            cameraTakesCount = 24;
            (Instantiate(prefabsBricks[1], new Vector3(prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * -0.38f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 0.23f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 25 * distancePerTake && currentDistance > totalDistance - 26 * distancePerTake && cameraTakesCount != 25) {
            cameraTakesCount = 25;
            (Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.15f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            //(Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 0.92f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 26 * distancePerTake && currentDistance > totalDistance - 27 * distancePerTake && cameraTakesCount != 26) {
            cameraTakesCount = 26;
            (Instantiate(prefabsBricks[2], new Vector3(prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * -0.92f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[1], new Vector3(-prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * 0.46f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 27 * distancePerTake && currentDistance > totalDistance - 28 * distancePerTake && cameraTakesCount != 27) {
            cameraTakesCount = 27;
            //(Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.69f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[1], new Vector3(prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * -0.61f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 28 * distancePerTake && currentDistance > totalDistance - 29 * distancePerTake && cameraTakesCount != 28) {
            cameraTakesCount = 28;
            (Instantiate(prefabsBricks[0], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabSpeedCharger, new Vector3(prefabSpeedCharger.transform.localScale.x * 2 + initializationDistanceX * -0.23f, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.92f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 29 * distancePerTake && currentDistance > totalDistance - 30 * distancePerTake && cameraTakesCount != 29) {
            cameraTakesCount = 29;
            (Instantiate(prefabsBricks[1], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            //(Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -1, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 30 * distancePerTake && currentDistance > totalDistance - 31 * distancePerTake && cameraTakesCount != 30) {
            cameraTakesCount = 30;
            (Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.77f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(-prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * 0.38f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 31 * distancePerTake && currentDistance > totalDistance - 32 * distancePerTake && cameraTakesCount != 31) {
            cameraTakesCount = 31;
            (Instantiate(prefabsBricks[1], new Vector3(-prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * 0.77f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            //(Instantiate(prefabsBricks[3], new Vector3(prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * -0.77f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 32 * distancePerTake && currentDistance > totalDistance - 33 * distancePerTake && cameraTakesCount != 32) {
            cameraTakesCount = 32;
            //(Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 1, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabSpeedCharger, new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY * 1.25f, -1), Quaternion.identity) as GameObject).transform.parent = rewardsParent.transform;
            (Instantiate(prefabsBricks[1], new Vector3(prefabsBricks[1].transform.localScale.x * 2 + initializationDistanceX * -0.69f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } /*else if (currentDistance <= totalDistance - 33 * distancePerTake && currentDistance > totalDistance - 34 * distancePerTake && cameraTakesCount != 33) {
            cameraTakesCount = 33;
            (Instantiate(prefabsBricks[2], new Vector3(prefabsBricks[2].transform.localScale.x * 2 + initializationDistanceX * -0.92f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[0], new Vector3(initializationDistanceX * 0, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        }/* else if (currentDistance <= totalDistance - 34 * distancePerTake && currentDistance > totalDistance - 35 * distancePerTake && cameraTakesCount != 34) {
            cameraTakesCount = 34;
            (Instantiate(prefabsBricks[3], new Vector3(-prefabsBricks[3].transform.localScale.x * 2 + initializationDistanceX * 0.38f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[0], new Vector3(prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * -0.84f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        } else if (currentDistance <= totalDistance - 35 * distancePerTake && currentDistance > totalDistance - 36 * distancePerTake && cameraTakesCount != 35) {
            cameraTakesCount = 35;
            (Instantiate(prefabsBricks[0], new Vector3(-prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * 0.84f, swordPositionY - initializationDistanceY, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
            (Instantiate(prefabsBricks[2], new Vector3(prefabsBricks[0].transform.localScale.x * 2 + initializationDistanceX * -0.92f, swordPositionY - initializationDistanceY * 1.5f, -1), Quaternion.identity) as GameObject).transform.parent = obstaclesParent.transform;
        }*/
    }
}
