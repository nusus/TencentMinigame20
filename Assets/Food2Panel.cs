using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Food2Panel : MonoBehaviour {
    public GameObject foodPanel = null;
    public Button yesfoodButton = null;
    public Button nofoodButton = null;
    public Button closeButton = null;

    // Use this for initialization
    void Start()
    {
        EventTriggerListener.Get(yesfoodButton.gameObject).onClick = OnYesBtnClick;
        EventTriggerListener.Get(nofoodButton.gameObject).onClick = OnNoBtnClick;
        EventTriggerListener.Get(closeButton.gameObject).onClick = OnNoBtnClick;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnYesBtnClick(GameObject go)
    {
        this.gameObject.SetActive(true);
        UnityEngine.SceneManagement.SceneManager.LoadScene("FeedFood");
    }

    private void OnNoBtnClick(GameObject go)
    {
        /*this.gameObject.SetActive(false);
        weishuiPanelCtrl weishuiCtrl = weishuiPanel.GetComponent<weishuiPanelCtrl>();
        if (weishuiCtrl != null)
        {
            weishuiCtrl.StopTick();
        }
        */
    }
}
