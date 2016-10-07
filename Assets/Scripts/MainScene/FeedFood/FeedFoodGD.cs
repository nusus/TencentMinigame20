using UnityEngine;
using System.Collections;

public class FeedFoodGD : MonoBehaviour {
    public int m_TotalTimes = 3;

    private int m_LeftTimes ;
    private int m_HitTimes;
    public BallController2D m_BallController;

    private FeedFoodUIManager m_FeedFoodUIManager;
    void Awake()
    {
        //Screen.orientation = ScreenOrientation.Landscape;
    }
	// Use this for initialization
	void Start () {
        m_FeedFoodUIManager = GameObject.Find("UIManager").GetComponent<FeedFoodUIManager>();
        m_LeftTimes = m_TotalTimes;
        m_HitTimes = 0;
        m_FeedFoodUIManager.ChangeHitTimes(m_HitTimes);
        m_FeedFoodUIManager.ChangeLeftTimes(m_LeftTimes);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddHitTimes()
    {
        m_HitTimes++;
        m_FeedFoodUIManager.ChangeHitTimes(m_HitTimes);
    }
    
    public void DecreaseLeftTimes()
    {
        m_LeftTimes--;
        m_FeedFoodUIManager.ChangeLeftTimes(m_LeftTimes);
    }   

    public void ReShootFood()
    {
        this.DecreaseLeftTimes();
        m_BallController.ReShootBall();
    }

    public void TryAgain()
    {
        ReShootFood();
        if(m_LeftTimes < 0)
        {
            OnTimesOut();
        }
    }

    public void OnTimesOut()
    {
        ShowResultPanel();
    }

    public void OnQuitButtonClicked()
    {
        ShowResultPanel();
    }

    public void ShowResultPanel()
    {
        if(m_LeftTimes < 0)
        {
            m_LeftTimes = 0;
        }
        m_FeedFoodUIManager.ShowResultPanel(m_HitTimes, m_LeftTimes);
    }

    public void QuitFeedFoodScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainui");
        UnityEngine.SceneManagement.SceneManager.UnloadScene("FeedFood2D");
    }
}
