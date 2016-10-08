using UnityEngine;
using System.Collections;

public class ShuxingPanelUIM : MonoBehaviour {
    public UnityEngine.UI.Slider m_HungerSlider;
    public UnityEngine.UI.Slider m_ThirstSlider;
    public UnityEngine.UI.Slider m_EnergySlider;
    public UnityEngine.UI.Slider m_HappySlider;
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

    public void OnShow(float hunger, float thirst, float energy, float happy)
    {
        m_HungerSlider.value = hunger;
        m_ThirstSlider.value = thirst;
        m_EnergySlider.value = energy;
        m_HappySlider.value = happy;
    }
}
