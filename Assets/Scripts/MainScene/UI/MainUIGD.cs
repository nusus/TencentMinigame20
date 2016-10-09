using UnityEngine;
using System.Collections;

public class MainUIGD : MonoBehaviour {

    public MainUIManager m_MainUIManager;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
	// Use this for initialization
	void Start () {
        GameDatabase.GetInstance();
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
