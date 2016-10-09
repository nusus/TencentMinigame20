using UnityEngine;
using System.Collections;

public class FeedFoodUIManager : MonoBehaviour {
    public UnityEngine.UI.Text m_HitTimesText;
    public UnityEngine.UI.Text m_LeftTimesText;

    public GameObject m_ResultPanel;
    // Use this for initialization
    void Start () {
        m_HitTimesText = GameObject.Find("hitTimesText").GetComponent<UnityEngine.UI.Text>();
        m_LeftTimesText = GameObject.Find("leftTimesText").GetComponent<UnityEngine.UI.Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeHitTimes(int hitTimes)
    {
        m_HitTimesText.text ="命中次数：" + hitTimes.ToString();
    }

    public void ChangeLeftTimes(int leftTimes)
    {
        m_LeftTimesText.text ="剩余次数：" + leftTimes.ToString();
    }


    public void ShowResultPanel(int hitTimes, int leftTimes)
    {
        m_ResultPanel.SetActive(true);
        UnityEngine.UI.Text hitText = GameObject.Find("hitTimesResultText").GetComponent<UnityEngine.UI.Text>();
        hitText.text = "命中次数：" + hitTimes.ToString();
        UnityEngine.UI.Text leftText = GameObject.Find("leftTimesResultText").GetComponent<UnityEngine.UI.Text>();
        leftText.text = "剩余次数：" + leftTimes.ToString();
    }

}
