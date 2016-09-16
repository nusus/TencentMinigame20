using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class weishuijieguoPanelCtrl : MonoBehaviour {

	public GameObject waterPanel = null;
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
        waterPanel.SetActive(true);
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
