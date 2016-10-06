using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;


public class FoodPanelCtrl : MonoBehaviour {

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
        this.gameObject.SetActive(false);
        //waterPanel.SetActive(true); foodPanel.SetActive(true)
        System.IO.FileStream fs = new System.IO.FileStream("d:\\text.txt", System.IO.FileMode.Create);
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fs);
        //开始写入
        sw.Write("Hello World!!!!");
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();

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
