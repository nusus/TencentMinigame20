﻿using UnityEngine;
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
	// Use this for initialization
	void Start () {
        m_CountDownTimeSecondsRoRender = m_CountDownSeconds;
        m_DrinkWaterUIManager = GameObject.Find("LobbyUI").GetComponent<DrinkWaterUIManager>();

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainui");
        UnityEngine.SceneManagement.SceneManager.UnloadScene("DrinkWater");
    }

    public void OnQuitButtonClicked() {
        ShowDrinkWaterResult((int)m_DrinkWaterSeconds, (int)(m_CountDownSeconds - m_CountDownTimeSecondsRoRender));
    }

    public void OnTimeOut() {
        ShowDrinkWaterResult((int)m_DrinkWaterSeconds, (int)m_CountDownSeconds);
    }

    private void ShowDrinkWaterResult(int drinkWater, int totalTime)
    {
        //GameObject panel = GameObject.Find("drinkResultPanel");
        m_DrinkWaterResultPanel.SetActive(true);

        UnityEngine.UI.Text drinkWaterSecondsResultText = GameObject.Find("drinkWaterSecondsResultText").GetComponent<UnityEngine.UI.Text>();
        drinkWaterSecondsResultText.text = "喝水量：" + drinkWater.ToString();
        
        UnityEngine.UI.Text drinkTimeText = GameObject.Find("drinkTimeText").GetComponent<UnityEngine.UI.Text>();
        drinkTimeText.text = "总耗时：" + totalTime.ToString();
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
}
