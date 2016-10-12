using UnityEngine;
using System.Collections;

public class FeedFoodGD : MonoBehaviour {
    public int m_TotalTimes = 3;

    private int m_LeftTimes ;
    private int m_HitTimes;
    public BallController2D m_BallController;

    private FeedFoodUIManager m_FeedFoodUIManager;

    private AudioSource m_As;

    public SphereCollider m_FoodCollider;
    public SphereCollider m_BabyCollider;
    void Awake()
    {
        
    }
	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        m_FeedFoodUIManager = GameObject.Find("UIManager").GetComponent<FeedFoodUIManager>();

        m_LeftTimes = m_TotalTimes;
        m_HitTimes = 0;
        m_FeedFoodUIManager.ChangeHitTimes(m_HitTimes);
        m_FeedFoodUIManager.ChangeLeftTimes(m_LeftTimes);

        m_As = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 distanceVector = m_FoodCollider.transform.position - m_BabyCollider.transform.position;
        if (distanceVector.magnitude < m_FoodCollider.radius + m_BabyCollider.radius) {
            OnFoodBabyCollision();
        }
	
	}

    private void OnFoodBabyCollision()
    {
        FDBabyController fdbc = GameObject.Find("baby").GetComponent<FDBabyController>();
        fdbc.OnFoodBabyCollision();
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
        GameDatabase.GetInstance().hunger += m_HitTimes * 3;
        m_FeedFoodUIManager.ShowResultPanel(m_HitTimes, m_LeftTimes);
    }

    public void QuitFeedFoodScene()
    {
        m_As.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainui");
        //UnityEngine.SceneManagement.SceneManager.UnloadScene("FeedFood2D");
    }
}
