using UnityEngine;
using System.Collections;

public class MainUIManager : MonoBehaviour {
    public Sprite[] m_NumImages;

    public GameObject m_ShuXingPanel;
    public GameObject m_FoodPanel;
    public GameObject m_WaterPanel;
    public GameObject m_UpdatePanel;
    public GameObject m_InfoPanel;

    public UnityEngine.UI.Slider m_ExpSlider;

    public UnityEngine.UI.Image m_LevelNumTenSprite;
    public UnityEngine.UI.Image m_LevelNumUnitSprite;

    public UnityEngine.UI.Image m_HealthNumTenSprite;
    public UnityEngine.UI.Image m_HealthNumUnitSprite;

    public UnityEngine.UI.Image m_CoinNumHundredSprite;
    public UnityEngine.UI.Image m_CoinNumTenSprite;
    public UnityEngine.UI.Image m_CoinNumUnitSprite;

    // Use this for initialization
    void Start () {
        
        OnShow(1);
	
	}

    public void OnShow(int updateFrequence)
    {
        if ((int)Time.realtimeSinceStartup % updateFrequence != 0)
            return;
        GameDatabase db = GameDatabase.GetInstance();
        OnLevelChanged(db.level);
        OnExperienceValueChanged(db.experience);
        OnHealthChanged(db.health);
        OnCoinChanged(db.money);
    }
	
	// Update is called once per frame
	void Update () {
        
        OnShow(10);
	
	}

    public void OnWaterButtonClicked()
    {
        m_WaterPanel.SetActive(true);
    }

    public void OnFoodButtonClicked()
    {
        m_FoodPanel.SetActive(true);
    }

    public void OnExperienceValueChanged(int newExp)
    {
        m_ExpSlider.value = newExp;
    }

    public void OnLevelChanged(int newLevel)
    {
        int ten = newLevel / 10;
        int unit = newLevel - ten * 10;
        this.ChangeNumberSprite(m_LevelNumTenSprite, ten);
        this.ChangeNumberSprite(m_LevelNumUnitSprite, unit);
    }

    public void OnHealthChanged(int newValue)
    {
        int ten = newValue / 10;
        int unit = newValue - ten * 10;
        this.ChangeNumberSprite(m_HealthNumTenSprite, ten);
        this.ChangeNumberSprite(m_HealthNumUnitSprite, unit);
    }
    public void OnCoinChanged(int newValue)
    {
        int hundred = newValue / 100;
        int ten = (newValue - hundred * 100) / 10;
        int unit = newValue - hundred * 100 - ten * 10;
        this.ChangeNumberSprite(m_CoinNumHundredSprite, hundred);
        this.ChangeNumberSprite(m_CoinNumTenSprite, ten);
        this.ChangeNumberSprite(m_CoinNumUnitSprite, unit);
    }

    public void OnUpgrade()
    {
        m_UpdatePanel.SetActive(true);
        UnityEngine.UI.Text updateText = GameObject.Find("updateText").GetComponent<UnityEngine.UI.Text>();
        updateText.text = "恭喜升级到：" + GameDatabase.GetInstance().level;
    }

    public void OnUpgradeConfirm()
    {
        m_UpdatePanel.SetActive(false);
        if (GameDatabase.GetInstance().level % 10 == 0)
        {
            GameDatabase.GetInstance().drama = GameDatabase.GetInstance().level / 10 + 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Chapter0");
        }
        
    }

    private void ChangeNumberSprite(UnityEngine.UI.Image image, int number)
    {
        if (number > 9) number = 9;
        image.sprite = m_NumImages[number];
    }

    public void OnCoinsNotEnough()
    {
        m_InfoPanel.SetActive(true);
    }
}
