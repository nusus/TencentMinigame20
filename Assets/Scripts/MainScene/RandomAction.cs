/*
 RandomAction.cs
 control pet action when pet is idle.
 created by hearstzhang at 2016.9.7
 */
using UnityEngine;
using System.Collections;

public class RandomAction : MonoBehaviour
{
    [Header("间隔时间")]
    [Tooltip("用于标记两次动作间隔的大致时间")]
    public float timeBetween;

    [Header("间隔时间变化范围")]
    [Tooltip("用于在一定范围内改变间隔时间，使得间隔时间有更多的随机性，大小请不要超过间隔时间")]
    public float randomTimeAddOrMinus;

    [Header("动作控制器")]
    [Tooltip("请将动作控制器\"IdleRandomControll\"放到这里")]
    //Using code to set this. So hide it in inspector. Remove this in case of need.
    [HideInInspector]
    public Animator IdleRandomControll;

    [Header("宠物状态")]
    [Tooltip("1表示宠物处于闲置状态，0表示宠物处于其他状态。闲置状态的时候会进行自动的走动、闲逛等动作")]
    //Using this if only this script controlls the animator. Otherwise remain this as comment.
    //[HideInInspector]
    public int isIdle;

    //Needless to know, private variables, for performance or other perpose

    //Used to calculate next action take effect time.
    private float nextAction;

    //actions for pet.
    //corresponding to the "Num" variable in the animator "IdleRandomControll"
    private enum actions
    {
        Thirsty = 1,
        Ballon,
        ScrollARound,
        Sitdown,
        Standup,
        Ball2Human,
        Human2Ball
    }
    //Used to define action.
    private int actionControll;

    //Last action.
    private int lastAction;

    //Temp time range. Set this for debug purpose. needless to know.
    private float tempTimeRange;


    // Use this for initialization
    void Start()
    {
        //Set action as idle.
        actionControll = 0;
        lastAction = 0;
        nextAction = 0.0f;
        IdleRandomControll = this.GetComponent<Animator>();
        tempTimeRange = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIdle != 0)
        {
            //Init as idle if possible.
            if(nextAction == 0.0f)
                nextAction = Time.time + Random.Range(-randomTimeAddOrMinus, randomTimeAddOrMinus) + timeBetween;
            if (Time.time > nextAction)
            {
                tempTimeRange = Random.Range(-randomTimeAddOrMinus, randomTimeAddOrMinus);
                nextAction = Time.time + tempTimeRange + timeBetween;
                //Debug.Log(tempTimeRange);
                if (actionControll == 0)
                {
                    do
                    {
                        actionControll = Random.Range((int)actions.Thirsty, (int)actions.Human2Ball);
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
