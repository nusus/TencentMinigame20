using UnityEngine;
using System.Collections;

public class ShuxingPanelUIM : MonoBehaviour {
    public UnityEngine.UI.Slider m_HungerSlider;
    public UnityEngine.UI.Text m_HungerText;

    public UnityEngine.UI.Slider m_ThirstSlider;
    public UnityEngine.UI.Text m_ThirstText;

    public UnityEngine.UI.Slider m_EnergySlider;
    public UnityEngine.UI.Text m_EnergyText;

    public UnityEngine.UI.Slider m_HappySlider;
    public UnityEngine.UI.Text m_HappyText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnQuitButtonClicked()
    {
        gameObject.SetActive(false);
    }

    public void OnShow()
    {
        MainUIGD gameDirector = GameObject.Find("GameDirector").GetComponent<MainUIGD>();
        GameDatabase db = GameDatabase.GetInstance();
        int tmp = (gameDirector.m_HungerDefaultValue - db.hunger) / gameDirector.m_HungerDefaultValue * 100;
        m_HungerSlider.value = tmp;
        m_HungerText.text = tmp.ToString() + "%";

        tmp = (gameDirector.m_ThirstDefaultValue - db.thirst) / gameDirector.m_ThirstDefaultValue * 100;
        m_ThirstSlider.value = tmp;
        m_ThirstText.text = tmp.ToString() + "%";

        tmp = (gameDirector.m_EnergyDefaultValue - db.energy) / gameDirector.m_EnergyDefaultValue * 100;
        m_EnergySlider.value = tmp;
        m_EnergyText.text = tmp.ToString() + "%";

        tmp = 50;
        m_HappySlider.value = tmp;
        m_HappyText.text = tmp.ToString() + "%";
    }
}
