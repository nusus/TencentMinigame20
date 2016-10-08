using UnityEngine;
using System.Collections;

public class MainUIManager : MonoBehaviour {
    public Sprite[] m_NumImages;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnWaterButtonClicked()
    {

    }

    public void OnFoodButtonClicked()
    {

    }

    public void OnExperienceValueChanged(int newExp)
    {

    }

    public void OnLevelChanged(int newLevel)
    {

    }

    public void OnHealthChanged(int newValue)
    {

    }
    public void OnCoinChanged(int newValue)
    {

    }

    private void ChangeNumberSprite(UnityEngine.UI.Image image, int number)
    {
        int ten = number / 10;
        int unit = number - ten * 10;
        image.sprite = m_NumImages[ten];
    }
}
