using UnityEngine;
using System.Collections;

public class DrinkWaterUIManager : MonoBehaviour {
    private UnityEngine.UI.Text m_CountDownSecondsText;
    private UnityEngine.UI.Text m_DrinkWaterSecondsText;

	// Use this for initialization
	void Start () {
        m_CountDownSecondsText = GameObject.Find("countDownText").GetComponent<UnityEngine.UI.Text>();
        m_DrinkWaterSecondsText = GameObject.Find("drinkVolumeText").GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {           
	
	}

    public void ChangeCountDownSeconds(int value)
    {
        m_CountDownSecondsText.text = value.ToString();
    }

    public void ChangeDrinkWaterSeconds(int value)
    {
        m_DrinkWaterSecondsText.text = value.ToString();
    }
}
