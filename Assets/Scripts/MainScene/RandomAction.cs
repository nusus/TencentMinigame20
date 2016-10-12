/*
 RandomAction.cs
 Using to calculate sleep time and let the pet do random actions
 include shake phone detect. but the number and threshold should be polished.
 control pet action when pet is idle.
 created by hearstzhang at 2016.9.7
 */

using UnityEngine;
using System.Collections;


public class RandomAction : MonoBehaviour
{
    [Header("间隔时间")]
    [Tooltip("用于标记两次动作间隔的大致时间，单位为秒")]
    public float timeBetween;

    [Header("间隔时间变化范围")]
    [Tooltip("用于在一定范围内改变间隔时间，使得间隔时间有更多的随机性，大小请不要超过间隔时间，单位为秒")]
    public float randomTimeAddOrMinus;

    [Header("动作控制器")]
    [Tooltip("请将动作控制器\"IdleRandomControll\"放到这里")]
    //Using code to set this. So hide it in inspector. Remove this in case of need.
    [HideInInspector]
    public Animator IdleRandomControll;

    //[Header("超时时间")]
    //[Tooltip("设备处于空闲状态（没有触摸）超过这个时间，宠物会睡觉，单位为秒")]
    //public float idleEclapseTime;

    //Needless to know, private variables, for performance or other perpose

    //Used to calculate next action take effect time.
    private float nextAction;

    //actions for pet.
    //corresponding to the "Num" variable in the animator "IdleRandomControll"
    private enum actions
    {
        Pinch = 1,
        Ballon,
        ScrollARound,
        Sitdown,
        Standup,
        Ball2Human,
        Human2Ball
    }
    //Used to define action.
    private int actionControll;

    //Last action. use this to prevent playing repeatly action.
    private int lastAction;

    //Temp time range. Set this for debug purpose. needless to know.
    private float tempTimeRange;

    //shake phone detect variable storage area
    // basic parameters.
    private float accelerometerUpdateInterval;
    private float lowPassKernelWidthInSeconds;
    private float shakeDetectionThreshold;

    //for performance consideration.
    private float lowPassFilterFactor = 0f;
    private Vector3 lowPassValue = Vector3.zero;
    private Vector3 acceleration = Vector3.zero;
    private Vector3 deltaAcceleration = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        //Set action as idle.
        actionControll = 0;
        lastAction = 0;
        nextAction = 0.0f;
        IdleRandomControll = this.GetComponent<Animator>();
        tempTimeRange = 0.0f;


        //shake phone detect initialize
        //initialized the phone.
        accelerometerUpdateInterval = 1.0f / 60.0f;
        lowPassKernelWidthInSeconds = 1.0f;
        shakeDetectionThreshold = 2.0f;
        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        lowPassValue = Input.acceleration;

        IdleRandomControll.SetBool("isLowEnergy", false);
        IdleRandomControll.SetBool("isHungry", false);
        IdleRandomControll.SetBool("isThirsty", false);

    }

    // Update is called once per frame
    void Update()
    {

        bool isThirsty, isHungry, isLowEnergy;
        isThirsty = IdleRandomControll.GetBool("isThirsty");
        isHungry = IdleRandomControll.GetBool("isHungry");
        isLowEnergy = IdleRandomControll.GetBool("isThirsty");
        if(isLowEnergy)
        {
            //shake detection
            acceleration = Input.acceleration;
            lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
            deltaAcceleration = acceleration - lowPassValue;

            //the phone is shaking or player touch the phone.
            if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold || Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                //shake phone or touch detected.
                //the pet should wake up.
                IdleRandomControll.SetBool("isLowEnergy", false);
            }
        }

        if (!isThirsty && !isLowEnergy && !isHungry)
        {
            //Init as idle if possible.
            if (nextAction < 0.001f)
            {
                //calculate next action time.
                nextAction = Time.time + Random.Range(-randomTimeAddOrMinus, randomTimeAddOrMinus) + timeBetween;
            }

            if (Time.time > nextAction)
            {
                tempTimeRange = Random.Range(-randomTimeAddOrMinus, randomTimeAddOrMinus);
                nextAction = Time.time + tempTimeRange + timeBetween;

                //make sure don't play one action repeatly.
                if (actionControll == 0)
                {
                    do
                    {
                        actionControll = Random.Range((int)actions.Pinch, (int)actions.Human2Ball);
                    }
                    while (lastAction == actionControll);
                    lastAction = actionControll;
                }
            }
            else
            {
                if (actionControll != 0)
                {
                    actionControll = 0;
                }
            }
            IdleRandomControll.SetInteger("Num", actionControll);
        }
    }
}
