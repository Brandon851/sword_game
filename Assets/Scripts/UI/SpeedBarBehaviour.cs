using System.Collections; // Library called System.Collections, it uses some of classes and functions of C#
using UnityEngine; // Library called UnityEngine, it uses most of Unity's functions
using UnityEngine.UI; // Library called UnityEngine.UI, it is used to all relationated with User Interfaces (UI)

public class SpeedBarBehaviour : MonoBehaviour
{
    // Proporties
    public Slider sliderSpeedBar { get; private set; }
    public float currentSliderValue { get; private set; }
    public float sliderMaxValue { get; private set; }
    public float distanceSwordFinishLevel { get; private set; }
    public bool deactivateSpeedBar { get; private set; }

    // Private variables
    [SerializeField] private RectTransform speedNeedle;
    private FinishLevelController finishLevelController;
    private Animator myAnimator;
    private int degreeRotationSpeedNeedle;
    private int maxValueHashCode;
    private float penalty;
    private float reward;
    private float calculateCurrentDistanceSwordFinishLevel;
    private float rotationSpeedNeedle;
    private float differenceMaxValueCurrentValue;
    private float auxPenalty;
    private float auxDistance;
    private bool unlockPenalty;
    private bool unlockFinish;
    private bool lockSpeedBar;
    private bool unlockMaxValueSpeedBar;

    /** PRIVATE METHODS **/

    // Start is called before the first frame update
    private void Start()
    {
        sliderSpeedBar = GetComponent<Slider>(); // Getting all components of type Slider
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        myAnimator = GetComponent<Animator>(); // Getting all components of type Animator
        maxValueHashCode = Animator.StringToHash("MaxValue"); // Coverting the "MaxValue" word (animation condition) in a hash of type integer
        distanceSwordFinishLevel = finishLevelController.distanceFinishLevel; // Initializing and calculating the distanceSwordFinishLevel variable with the method to calculate distance
        sliderMaxValue = (distanceSwordFinishLevel) - (distanceSwordFinishLevel / 3); // Initializing and calculating the slider maximum value, substraction 1/3 of the distance between the sword and the end of level (it means that will be able to have until a 33% more of speed)
        penalty = distanceSwordFinishLevel / 4; // Initializing and calculating a penalty, dividing the distance between the sword and the end of level divided by 4
        reward = distanceSwordFinishLevel / 10; // Initializing and calculating a reward, dividing the distance between the sword and the end of level divided by 10
        sliderSpeedBar.maxValue = sliderMaxValue; // Initializing the maxValue proporty of slider with the sliderMaxValue variable (previously calculated)
        calculateCurrentDistanceSwordFinishLevel = 0; // Initializing calculateCurrentDistanceSwordFinishLevel variable to 0
        currentSliderValue = 0; // Initializing sliderCurrentValue variable to 0
        rotationSpeedNeedle = 0; // Initializing rotationSpeedNeedleue variable to 0
        differenceMaxValueCurrentValue = 0; // Initializing differenceMaxValueCurrentValue variable to 0
        auxPenalty = 0; // Initializing auxPenalty variable to 0
        auxDistance = 0; // Initializing auxDistance variable to 0
        degreeRotationSpeedNeedle = -235; // Initializing degreeRotationSpeedNeedle variable to -235 degrees
        unlockPenalty = false; // Initializing the unlockPenalty boolean variable as false
        unlockFinish = false; // Initializing the unlockFinish boolean variable as false
        lockSpeedBar = false; // Initializing the lockSpeedBar boolean variable as false
        deactivateSpeedBar = false; // Initializing the deactivateSpeedBar boolean variable as false
        unlockMaxValueSpeedBar = false; // Initializing the maxValueSpeedBar boolean variable as false
    }

    // Update is called once per frame
    private void Update()
    {
        myAnimator.SetBool(maxValueHashCode, unlockMaxValueSpeedBar); // Applying the max speed bar animation, depending on a bool parameter

        // If the speed bar is not locked, then the speed bar will work normally
        if (!lockSpeedBar)
        {
            // If the slider value does not reach to the maximum value, then the speed bar will work between values less than the maximum value (including negative values)
            if (!(currentSliderValue >= sliderMaxValue))
            {
                unlockMaxValueSpeedBar = false; // Setting the maxValueSpeedBar boolean variable as false
                unlockFinish = false; // Setting the unlockFinish boolean variable as false
                if (!unlockPenalty) // If the penalty is not unlocked, then it wil set the auxiliary of distance with the value of the distance between the sword and the end of level
                    auxDistance = distanceSwordFinishLevel; // Setting the auxiliary of distance with the value of the distance between the sword and the end of level
                else if (unlockPenalty) // If the penalty is unlocked, then it wil set the auxiliary of distance with the value of the distance between the sword and the end of level minus the auxiliary of penalty
                    auxDistance = distanceSwordFinishLevel - auxPenalty; // Setting the auxiliary of distance with the value of the distance between the sword and the end of level minus the auxiliary of penalty

                // If the current slider value is negative, then it will reset the current slider value and the rotation speed needle to 0, also it will add the auxiliary penalty to current distance between the sword and the end of level to reset distanceSwordFinishLevel variable
                if (currentSliderValue < 0)
                {
                    currentSliderValue = 0; // Resetting the current slider value to 0
                    rotationSpeedNeedle = 0; // Resetting the rotation speed needle to 0
                    distanceSwordFinishLevel = finishLevelController.CalculateDistance() + auxPenalty; // Adding the auxiliary penalty to current distance between the sword and the end of level to reset distanceSwordFinishLevel variable
                } else if (currentSliderValue >= 0 && currentSliderValue < sliderMaxValue) { // If the current slider value is between the range of speed bar, then it will calculate the current slider value and the rotation speed needle value with respect to the current distance between the sword and the end of level
                    calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance(); // Setting the current distance between the sword and the end of level
                    currentSliderValue = auxDistance - calculateCurrentDistanceSwordFinishLevel; // Calculating the current slider value, substraction the current distance between the sword and the end of level to the auxiliary of distance
                    rotationSpeedNeedle = currentSliderValue * degreeRotationSpeedNeedle / sliderMaxValue; // Converting the current slider value (previously calculated) to degrees to set rotation speed needle
                }
            } else { // If the slider value is greater than the maximum value, then current slider value and rotation speed needle value will reset the maximum value 
                unlockMaxValueSpeedBar = true; // Setting the maxValueSpeedBar boolean variable as true
                currentSliderValue = sliderMaxValue; // Resetting  the current slider value to the maximum value of slider speed bar;
                rotationSpeedNeedle = degreeRotationSpeedNeedle; // Resetting the rotation speed needlee to the maximum degree of rotation
                differenceMaxValueCurrentValue = auxDistance - finishLevelController.CalculateDistance() - currentSliderValue; // Calculating the difference the current maximum value when current slider value is greater than the auxiliary distance

                // If the sword hit with an obstacle and the speed bar is in its maximum value, then the current slider value will substract the penalty to itself and also the distance between the sword and the end of level will substract the difference the current maximum value to itself
                if (unlockFinish)
                {
                    currentSliderValue -= penalty; // Substracting the penalty to the current slider value
                    distanceSwordFinishLevel -= differenceMaxValueCurrentValue; // Substracting the difference the current maximum value to the distance between the sword and the end of level
                }
            }
      
            sliderSpeedBar.value = currentSliderValue; // Setting the value property of slider with current slider value
            speedNeedle.eulerAngles = new Vector3(0, 0, rotationSpeedNeedle); // Applying a rotation of Euler angles in the axis Z to the speed needle
        }
    }

    // This method decreases the slider and needle of speed bar (return the changes of slider and needle each frame) with a decrement of 4.5f
    private IEnumerator DecreaseSliderAndNeedleSpeedBar()
    {
        // This loop decreases the slider and needle of speed bar to 0
        for (float currentValue = currentSliderValue; currentValue >= 0; currentValue -= 4.5f)
        {
            sliderSpeedBar.value = currentValue; // Decreasing slider speed bar value
            speedNeedle.eulerAngles = new Vector3(0, 0, currentValue * degreeRotationSpeedNeedle / sliderMaxValue); // Decreasing speed needle angle
            yield return null; // Returning null value each frame
        }

        sliderSpeedBar.value = 0; // Resetting the slider speed bar value to 0
        speedNeedle.eulerAngles = new Vector3(0, 0, 0); // Resetting the Euler angles in all axes to 0
        gameObject.SetActive(false); // Deactivating the speed bar, when the speed bar value is 0 and the sword is broken
        deactivateSpeedBar = true; // Setting the deactivateSpeedBar boolean variable as true
    }

    /** PUBLIC METHODS **/

    // This method decrease current value of slider depending on a penalty 
    public void DecreaseSpeed()
    {
        auxPenalty += penalty; // Adding a penalty to penalty count
        unlockPenalty = true; // Setting the unlockPenalty boolean variable as true
        unlockFinish = true; // Setting the unlockFinish boolean variable as true
    }

    // This method increase current value of slider depending on reward
    public void IncreaseSpeed()
    {
        if(!(unlockMaxValueSpeedBar)) // If is not unlocked max value of speed bar, then it will apply the reward
        {
            auxPenalty -= reward; // Adding a reward to penalty count
            unlockPenalty = true; // Setting the unlockPenalty boolean variable as true
            unlockFinish = true; // Setting the unlockFinish boolean variable as true
        }
    }

    // This method restart the speed bar to 0, it is called SwordBehaviour script
    public void RestartSpeedBar()
    {
        lockSpeedBar = true; // Setting the lockSpeedBar boolean variable as true
        StartCoroutine(DecreaseSliderAndNeedleSpeedBar()); // Calling and starting the coroutine to restart slider and needle of speed bar to 0
    }
}