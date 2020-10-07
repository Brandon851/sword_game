using UnityEngine;
using UnityEngine.UI;

public class SpeedBarBehaviour2 : MonoBehaviour
{
    // Private variables
    [SerializeField] private RectTransform speedNeedle;
    private Slider sliderSpeedBar;
    private FinishLevelController finishLevelController;
    private float penalty;
    private float distanceSwordFinishLevel;
    private float calculateCurrentDistanceSwordFinishLevel;
    private float sliderMaxValue;
    private float sliderCurrentValue;
    private float rotationSpeedNeedle;
    private float constantMaxSpeed;
    private float auxPenalty;
    private float auxDistance;
    private float auxSliderCurrentValue;
    private int degreeRotationSpeedNeedle;
    private bool unlockPenalty;
    private bool unlockFinish;
    private bool unlockMaxSpeed;

    // Awake can initialize variables only once per execution and can work inactivated
    void Start()
    {
        sliderSpeedBar = GetComponent<Slider>(); // Getting all components of type Slider
        finishLevelController = GameObject.FindGameObjectWithTag("Finish").GetComponent<FinishLevelController>(); // Getting all components of type FinishLevelController
        distanceSwordFinishLevel = finishLevelController.CalculateDistance() * 1.05f;
        sliderMaxValue = (distanceSwordFinishLevel) - (distanceSwordFinishLevel / 3);
        penalty = distanceSwordFinishLevel * 1.05f / 5;
        sliderSpeedBar.maxValue = sliderMaxValue;
        calculateCurrentDistanceSwordFinishLevel = 0;
        sliderCurrentValue = 0;
        rotationSpeedNeedle = 0;
        constantMaxSpeed = 1;
        auxPenalty = 0;
        auxDistance = 0;
        auxSliderCurrentValue = 0;
        degreeRotationSpeedNeedle = -235;
        unlockPenalty = false;
        unlockFinish = false;
        unlockMaxSpeed = false;
        /*Debug.Log("DistanceSwordFinishlevel: " + (distanceSwordFinishLevel));
        Debug.Log("SliderMaxValue: " + (sliderMaxValue));
        Debug.Log("Penalty: " + (penalty));*/
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Distance: " + distanceSwordFinishLevel);
        Debug.Log("AuxDistance: " + auxDistance);

        if (!unlockPenalty)
        {
            auxDistance = distanceSwordFinishLevel;
        } else if (unlockPenalty) {
            auxDistance = distanceSwordFinishLevel - auxPenalty;
        }


        if (sliderCurrentValue < 0)
        {
            unlockFinish = false;
            sliderCurrentValue = 0;
            rotationSpeedNeedle = 0;
            distanceSwordFinishLevel = finishLevelController.CalculateDistance() + auxPenalty;
        } else if (sliderCurrentValue >= 0 && sliderCurrentValue < sliderMaxValue) {
            unlockFinish = false;
            /*if(!unlockMaxSpeed)
            {
                calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance();
                sliderCurrentValue = auxDistance - calculateCurrentDistanceSwordFinishLevel;
                rotationSpeedNeedle = sliderCurrentValue * degreeRotationSpeedNeedle / sliderMaxValue;
            } else {
                //Debug.Log(finishLevelController.CalculateDistance()*0.55f);
                //float auxSliderCurrentValue = sliderCurrentValue;
                calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance() * (((distanceSwordFinishLevel - finishLevelController.CalculateDistance()) - auxSliderCurrentValue)/ finishLevelController.CalculateDistance());// ((auxSliderCurrentValue - (auxDistance - finishLevelController.CalculateDistance()))/finishLevelController.CalculateDistance());
                Debug.Log((((distanceSwordFinishLevel - finishLevelController.CalculateDistance()) - auxSliderCurrentValue) / finishLevelController.CalculateDistance()));
                
                //Debug.Log(((distanceSwordFinishLevel - finishLevelController.CalculateDistance())) - auxSliderCurrentValue);
                //Debug.Log(((auxSliderCurrentValue - (distanceSwordFinishLevel - finishLevelController.CalculateDistance())) / finishLevelController.CalculateDistance()));
                //Debug.Log("AuxSliCurrVal: "+auxSliderCurrentValue + " - SlideCurrValue" + (auxDistance - calculateCurrentDistanceSwordFinishLevel) + " = " + (auxSliderCurrentValue - (auxDistance - calculateCurrentDistanceSwordFinishLevel)));
                //sliderCurrentValue = auxDistance - (calculateCurrentDistanceSwordFinishLevel - (auxSliderCurrentValue - (auxDistance - calculateCurrentDistanceSwordFinishLevel))); //* ((auxSliderCurrentValue - auxDistance - calculateCurrentDistanceSwordFinishLevel)/calculateCurrentDistanceSwordFinishLevel); //- calculateCurrentDistanceSwordFinishLevel - calculateCurrentDistanceSwordFinishLevel;
                sliderCurrentValue = auxDistance - calculateCurrentDistanceSwordFinishLevel;
                
                //Debug.Log("CalculateCurrentDistance: " + finishLevelController.CalculateDistance());
                //Debug.Log("SliderCurrentValue: " + sliderCurrentValue);
                rotationSpeedNeedle = sliderCurrentValue * degreeRotationSpeedNeedle / sliderMaxValue;
                //unlockMaxSpeed = false;
            }*/

            if (unlockMaxSpeed)
            {
                constantMaxSpeed = ((distanceSwordFinishLevel - finishLevelController.CalculateDistance()) - auxSliderCurrentValue) / finishLevelController.CalculateDistance();
                calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance() * constantMaxSpeed;
                sliderCurrentValue = auxDistance - calculateCurrentDistanceSwordFinishLevel;
                //unlockMaxSpeed = false;
            } else {
                calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance();
                sliderCurrentValue = auxDistance - calculateCurrentDistanceSwordFinishLevel;
            }

            /*calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance() * constantMaxSpeed;
            sliderCurrentValue = auxDistance - calculateCurrentDistanceSwordFinishLevel;*/
            rotationSpeedNeedle = sliderCurrentValue * degreeRotationSpeedNeedle / sliderMaxValue;

        } else if (sliderCurrentValue >= sliderMaxValue){
            sliderCurrentValue = sliderMaxValue;
            rotationSpeedNeedle = degreeRotationSpeedNeedle;
            distanceSwordFinishLevel = sliderMaxValue * 1.5f;
          
            /*Debug.Log("DistanceSwordFinishlevel 1: " + (distanceSwordFinishLevel));
            Debug.Log("SliderCurrentValue 1: " + (sliderCurrentValue));
            Debug.Log("AuxPenalty: " + (auxPenalty));*/
            if(unlockFinish)
            {
                sliderCurrentValue -= penalty;

                //if (distanceSwordFinishLevel == sliderMaxValue)
                    distanceSwordFinishLevel -= penalty;
                /*else
                    distanceSwordFinishLevel -= 2 * penalty;*/
                unlockMaxSpeed = true;
                unlockFinish = false;
            }
            auxSliderCurrentValue = sliderCurrentValue;
            /*Debug.Log("DistanceSwordFinishlevel 2: " + (distanceSwordFinishLevel));
            Debug.Log("SliderCurrentValue 2: " + (sliderCurrentValue));*/
        }

        /*if (sliderCurrentValue >= 0 && sliderCurrentValue < sliderMaxValue)
        {
            calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance();
            sliderCurrentValue = auxDistance - calculateCurrentDistanceSwordFinishLevel;
            rotationSpeedNeedle = sliderCurrentValue * degreeRotationSpeedNeedle / sliderMaxValue;
        }*/

            /*if (sliderCurrentValue < 0)
            {
                sliderCurrentValue = 0;
                rotationSpeedNeedle = 0;
                distanceSwordFinishLevel = finishLevelController.CalculateDistance() + auxPenalty;
            } else if (sliderCurrentValue >= sliderMaxValue) {
                sliderCurrentValue = sliderMaxValue;
                rotationSpeedNeedle = degreeRotationSpeedNeedle;
            }

            calculateCurrentDistanceSwordFinishLevel = finishLevelController.CalculateDistance();
            sliderCurrentValue = auxDistance - calculateCurrentDistanceSwordFinishLevel;
            rotationSpeedNeedle = sliderCurrentValue * degreeRotationSpeedNeedle / sliderMaxValue;*/

            /*if (sliderCurrentValue < 0)
            {
                sliderCurrentValue = 0;
                rotationSpeedNeedle = 0;
                distanceSwordFinishLevel = finishLevelController.CalculateDistance() + auxPenalty;
            }*/

        sliderSpeedBar.value = sliderCurrentValue;
        speedNeedle.eulerAngles = new Vector3(0, 0, rotationSpeedNeedle);
    }

    // This method reduces current value of slider depending a penalty 
    public void ReduceSpeed()
    {
        auxPenalty += penalty;
        unlockPenalty = true;
        unlockFinish = true;
    }
}
