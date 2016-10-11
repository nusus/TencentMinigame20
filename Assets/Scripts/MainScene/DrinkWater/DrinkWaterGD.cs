using UnityEngine;
using System.Collections;

public class DrinkWaterGD : MonoBehaviour {
    [Header("喝水倒计时时间")]
    [Tooltip("单位为秒")]
    public float m_CountDownSeconds = 60;

    private float m_CountDownTimeSecondsRoRender = 0.0f;
    //喝到水的总时间
    private float m_DrinkWaterSeconds = 0;

    private DrinkWaterUIManager m_DrinkWaterUIManager;

    public GameObject m_DrinkWaterResultPanel;

    private bool m_GameOver = false;

    private AudioSource m_As;
	// Use this for initialization
	void Start () {
        m_CountDownTimeSecondsRoRender = m_CountDownSeconds;
        m_DrinkWaterUIManager = GameObject.Find("LobbyUI").GetComponent<DrinkWaterUIManager>();
        m_As = GetComponent<AudioSource>();
        m_As.Play();
    }
	
	// Update is called once per frame
	void Update () {
        m_CountDownTimeSecondsRoRender -= Time.deltaTime;
        if (m_CountDownTimeSecondsRoRender <= 0.0f) {
            OnTimeOut();
        }

        m_DrinkWaterUIManager.ChangeCountDownSeconds((int)(m_CountDownTimeSecondsRoRender));
        m_DrinkWaterUIManager.ChangeDrinkWaterSeconds((int)m_DrinkWaterSeconds);
	
	}

    public void QuitDrinkWaterScene()
    {
        m_As.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainui");
        //UnityEngine.SceneManagement.SceneManager.UnloadScene("DrinkWater");
    }

    public void OnQuitButtonClicked() {
        ShowDrinkWaterResult((int)m_DrinkWaterSeconds, (int)(m_CountDownSeconds - m_CountDownTimeSecondsRoRender));
    }

    public void OnTimeOut() {
        if (!m_GameOver) {
            ShowDrinkWaterResult((int)m_DrinkWaterSeconds, (int)m_CountDownSeconds);
            m_GameOver = true;
        }
            
    }

    private void ShowDrinkWaterResult(int drinkWater, int totalTime)
    {
        //GameObject panel = GameObject.Find("drinkResultPanel");
        m_DrinkWaterResultPanel.SetActive(true);

        UnityEngine.UI.Text drinkWaterSecondsResultText = GameObject.Find("drinkWaterSecondsResultText").GetComponent<UnityEngine.UI.Text>();
        drinkWaterSecondsResultText.text = "喝水量：   " + drinkWater.ToString();
        
        UnityEngine.UI.Text drinkTimeText = GameObject.Find("drinkTimeText").GetComponent<UnityEngine.UI.Text>();
        drinkTimeText.text = "总耗时：   " + totalTime.ToString();

        GameDatabase.GetInstance().thirst += (int)this.GetDrinkingWaterPercent();
    }

    public void IncreaseDrinkWaterSeconds(float seconds)
    {
        m_DrinkWaterSeconds += seconds;
    }

    public float GetCountDownTimeSecondsToRender()
    {
        return m_CountDownTimeSecondsRoRender;
    }

    public float GetDrinkWaterSeconds() {
        return m_DrinkWaterSeconds;
    }


    public float GetDrinkingWaterPercent()
    {
        return m_DrinkWaterSeconds / m_CountDownSeconds * 100;
    }
}
