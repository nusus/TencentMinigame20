using UnityEngine;
using System.Collections;

public class DrinkWaterUIManager : MonoBehaviour {
    private UnityEngine.UI.Text m_CountDownSecondsText;
    private UnityEngine.UI.Text m_DrinkWaterSecondsText;
    private UnityEngine.UI.Button m_QuitButton;

    private DrinkWaterGD m_GameDirector;
	// Use this for initialization
	void Start () {
        m_CountDownSecondsText = GameObject.Find("countDownText").GetComponent<UnityEngine.UI.Text>();
        m_DrinkWaterSecondsText = GameObject.Find("drinkVolumeText").GetComponent<UnityEngine.UI.Text>();
        m_QuitButton = GameObject.Find("quitButton").GetComponent<UnityEngine.UI.Button>();

        m_GameDirector = GameObject.Find("GameDirector").GetComponent<DrinkWaterGD>();
	}
	
	// Update is called once per frame
	void Update () {
        m_CountDownSecondsText.text = ((int)m_GameDirector.GetCountDownTimeSecondsToRender()).ToString();
        m_DrinkWaterSecondsText.text = ((int)m_GameDirector.GetDrinkWaterSeconds()).ToString();
	
	}
}
