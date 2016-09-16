using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class quitweishuiPanelCtrl : MonoBehaviour {

    public GameObject weishuiPanel = null;
    public GameObject weishuijieguoPanel = null;
    public Button yesButton = null;
    public Button noButton = null;
    public Button closeButton = null;

	// Use this for initialization
	void Start () {
        EventTriggerListener.Get(yesButton.gameObject).onClick = OnYesClick;
        EventTriggerListener.Get(noButton.gameObject).onClick = OnCloseClick;
        EventTriggerListener.Get(closeButton.gameObject).onClick = OnCloseClick;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void OnYesClick(GameObject go)
    {
        this.gameObject.SetActive(false);
        weishuijieguoPanel.SetActive(true);
    }

    private void OnCloseClick(GameObject go)
    {
        this.gameObject.SetActive(false);
        weishuiPanel.SetActive(true);
        weishuiPanelCtrl weishuiCtrl = weishuiPanel.GetComponent<weishuiPanelCtrl>();
        if (weishuiCtrl != null)
        {
            weishuiCtrl.ResumeTick();
        }
    }
}
