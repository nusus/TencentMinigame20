using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class weishuijieguoPanelCtrl : MonoBehaviour {

    public GameObject weishuiPanel = null;
    public Button yesButton = null;
    public Button noButton = null;
	public Button closeButton = null;

	// Use this for initialization
	void Start () {
        EventTriggerListener.Get(yesButton.gameObject).onClick = OnYesBtnClick;
		EventTriggerListener.Get(noButton.gameObject).onClick = OnNoBtnClick;
		EventTriggerListener.Get(closeButton.gameObject).onClick = OnNoBtnClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnYesBtnClick(GameObject go)
    {
        this.gameObject.SetActive(false);
        weishuiPanel.SetActive(true);
        weishuiPanelCtrl weishuiCtrl = weishuiPanel.GetComponent<weishuiPanelCtrl>();
        if (weishuiCtrl != null)
        {
            weishuiCtrl.StartTick();
        }
    }

    private void OnNoBtnClick(GameObject go)
    {
		this.gameObject.SetActive(false);
		weishuiPanelCtrl weishuiCtrl = weishuiPanel.GetComponent<weishuiPanelCtrl>();
		if (weishuiCtrl != null)
		{
			weishuiCtrl.StopTick();
		}
    }
}
