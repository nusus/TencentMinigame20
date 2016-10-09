using UnityEngine;
using System.Collections;

public class MainUIGD : MonoBehaviour {
    [Header("饱渴值流失速度")]
    [Tooltip("每秒饱渴值流失的值，整数")]
    public int m_ThirstLosingSpeed;

    [Header("饱渴标准值")]
    [Tooltip("饱渴标准值，整数")]
    public int m_ThirstStandard;

    [Header("饱渴初始值")]
    [Tooltip("饱渴初始值，整数")]
    public int m_ThirstDefaultValue;


    [Header("饱食值流失速度")]
    [Tooltip("每秒饱食值流失的值，整数")]
    public int m_HungerLosingSpeed;

    [Header("饱食标准值")]
    [Tooltip("饱食标准值，整数")]
    public int m_HungerStandard;

    [Header("饱食初始值")]
    [Tooltip("饱食初始值，整数")]
    public int m_HungerDefaultValue;

    [Header("精力值流失速度")]
    [Tooltip("每秒精力值流失的值，整数")]
    public int m_EnergyLosingSpeed;

    [Header("精力值标准值")]
    [Tooltip("精力值标准值，整数")]
    public int m_EnergyStandard;

    [Header("精力值初始值")]
    [Tooltip("精力值初始值，整数")]
    public int m_EnergyDefaultValue;


    public MainUIManager m_MainUIManager;

    private GameDatabase db;
    private MainUIBaby m_Baby;
    void Awake() {
        Screen.orientation = ScreenOrientation.Portrait;
    }
	// Use this for initialization
	void Start () {
        db = GameDatabase.GetInstance();
        if (db.isFirstStart) {
            db.thirst = m_ThirstDefaultValue;
            db.hunger = m_HungerDefaultValue;
            db.energy = m_HungerDefaultValue;
        }

        m_Baby = GameObject.Find("baby").GetComponent<MainUIBaby>();
	
	}
	
	// Update is called once per frame
	void Update () {
        db.thirst -= m_ThirstLosingSpeed;
        if (db.thirst < m_ThirstStandard) {
            m_Baby.OnThirstLowerStandard();
        }

        db.hunger -= m_HungerLosingSpeed;
        if (db.hunger < m_HungerStandard) {
            m_Baby.OnHungerLowerStandard();
        }

        db.energy -= m_EnergyLosingSpeed;
        if (db.energy < m_EnergyStandard) {
            m_Baby.OnEnergyLowerStandard();
        }
	
	}

    public void PurchaseWater()
    {
        DrinkWater();
    }

    public void DrinkWater()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadScene("mainui");
        UnityEngine.SceneManagement.SceneManager.LoadScene("DrinkWater");
    }

    public void PurchaseFood()
    {
        FeedFood();
    }

    public void FeedFood()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadScene("mainui");
        UnityEngine.SceneManagement.SceneManager.LoadScene("FeedFood");
    }
}
